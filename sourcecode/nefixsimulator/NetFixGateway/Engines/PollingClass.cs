using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AR.NetFixGateway.Engines
{
    public abstract class PollingClass : IDisposable
    {
        private Timer _timer;
        protected int _refreshInterval = System.Threading.Timeout.Infinite;
        private bool updating = false;
        private readonly object _lockObject = new object();

        protected delegate void DoWork();
        protected DoWork doWorkFunc = null;

        public virtual void Start()
        {
            SetTimoutValue();
            _timer = new Timer(TimerTick);
            RefreshInterval = _refreshInterval;
        }
       
        protected abstract void SetTimoutValue();

        public int RefreshInterval
        {
            get { return _refreshInterval; }
            set
            {
                _refreshInterval = value;
                _timer.Change(0, _refreshInterval);
            }
        }

        private void TimerTick(object state)
        {
            lock (_lockObject)
            {
                if (updating)
                    return;
            }
            updating = true;
            try
            {
                if(doWorkFunc != null)
                    doWorkFunc();
            }
            finally
            {
                updating = false;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_timer != null)
                _timer.Dispose();
            _timer = null;
        }


        ~PollingClass()
        {
            Dispose(false);
        }
    }
}

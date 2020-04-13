using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AR.NETFixSimulator.Model;

namespace AR.NETFixSimulator.ViewModel
{
    using AR.NetFixGateway;

    using Ninject;
    using Ninject.Modules;

    public class ConnectionGroupViewModel : GroupData
    {
        private ISession _session;
       
        [Inject]
        public ConnectionGroupViewModel(ISession session) : base("Connection")
        {
            Initializer();
            _session = session;
        }

        private void Initializer()
        {
            ControlDataCollection.Add(
                new ButtonData("Start", "Start", this)
                    {
                        SmallImage = new Uri("/NetFixSimulator;component/Resources/Start_16x16.png", UriKind.Relative),
                        LargeImage = new Uri("/NetFixSimulator;component/Resources/Start_32x32.png", UriKind.Relative),
                        ToolTipTitle = "Start Session"
                    });

            ControlDataCollection.Add(
                new ButtonData("Stop", "Stop", this)
                {
                    SmallImage = new Uri("/NetFixSimulator;component/Resources/Stop_16x16.png", UriKind.Relative),
                    LargeImage = new Uri("/NetFixSimulator;component/Resources/Stop_32x32.png", UriKind.Relative),
                    ToolTipTitle = "Stop Session"
                });

        }

        public void Start()
        {
            _session.Start("NetFixSimulator.cfg");
        }

        public bool CanStart()
        {
            return !_session.IsConnected;
        }

        public bool CanStop()
        {
            return _session.IsConnected;
        }

        public void Stop()
        {

        }

    }
}

using System;
using AR.NetFixGateway;
using AR.NETFixSimulator.Models;
using Caliburn.Micro;
using Ninject;
using System.ComponentModel;

namespace AR.NETFixSimulator.ViewModels
{
    

    public class ConnectionGroupViewModel : PropertyChangedBase        
    {
        private ISession _session;

        public GroupData groupData { get; set; }

        [Inject]
        public ConnectionGroupViewModel(ISession session) : base()
        {
            Initializer();            
            _session = session;
            _session.StateChangeEvent += state =>
            {
                NotifyOfPropertyChange(() => CanStart);
                NotifyOfPropertyChange(() => CanStop);
                NotifyOfPropertyChange(() => CanPause);
            };
        }

        private void Initializer()
        {
            groupData = new GroupData("Connection");
            groupData.ControlDataCollection.Add(
                new ButtonData("Start", "Start", this)
                    {
                        SmallImage = new Uri("/NetFixSimulator;component/Resources/Start.png", UriKind.Relative),
                        LargeImage = new Uri("/NetFixSimulator;component/Resources/Start.png", UriKind.Relative),
                        ToolTipTitle = "Start Session"
                    });

            groupData.ControlDataCollection.Add(
                new ButtonData("Stop", "Stop", this)
                {
                    SmallImage = new Uri("/NetFixSimulator;component/Resources/Stop.png", UriKind.Relative),
                    LargeImage = new Uri("/NetFixSimulator;component/Resources/Stop.png", UriKind.Relative),
                    ToolTipTitle = "Stop Session"
                });

            //TODO: 
            groupData.ControlDataCollection.Add(
                new TextBoxData("Delay", this)
                {                    
                    Label = "Delay",                                        
                    ToolTipTitle = "UI Refresh delay in seconds",
                    ToolTipDescription = "The longer delay equals better performance. This only effects the rate that UI is refereshed and not FIX message processing."
                });

            _delay = UserSettings.Default.Delay;
        }

        public void Start()
        {
            _session.Start();
           
        }

        public bool CanStart
        {
            get
            {
                return !_session.IsStarted;
            }
        }

        public bool CanStop
        {
            get
            {
                return _session.IsStarted;
            }
        }

        public void Stop()
        {
            _session.Stop();           
        }

        public bool CanPause
        {
            get
            {
                return _session.IsStarted;
            }
        }

        public string Delay
        {
            get { return _delay.ToString(); }
            set 
            {
                float tryValue;
                if (float.TryParse(value, out tryValue))
                {
                    _delay = tryValue;
                    UserSettings.Default.Delay = _delay;
                    UserSettings.Default.Save();
                    NotifyOfPropertyChange("Text");
                }          
            }
        }
        float _delay;
      
    }
}

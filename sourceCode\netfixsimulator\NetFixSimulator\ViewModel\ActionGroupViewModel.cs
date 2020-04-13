using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AR.NETFixSimulator.Model;

namespace AR.NETFixSimulator
{
    
    public class ActionGroupViewModel : GroupData
    {
        public ActionGroupViewModel(string header = "Actions") : base(header)
        {
            Initializer();
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

        }

        public bool CanStop()
        {
            return false;
        }

        public void Stop()
        {

        }

    }
}

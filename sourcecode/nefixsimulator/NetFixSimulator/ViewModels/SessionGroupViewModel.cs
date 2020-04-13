using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AR.NETFixSimulator.Model;
using AR.NetFixGateway;
using Caliburn.Micro;
using Ninject;

namespace AR.NETFixSimulator.ViewModels
{
    class SessionGroupViewModel : Conductor<object>
    {
        private readonly ISession _session;
        private IWindowManager _windowManager;

        public GroupData groupData { get; set; }

        [Inject]
        public SessionGroupViewModel(ISession session, IWindowManager windowManager)
            : base()
        {
            Initializer();
            _windowManager = windowManager;
            _session = session;
            _session.StateChangeEvent += state =>
                                           {
                                               NotifyOfPropertyChange(() => CanReset);
                                               NotifyOfPropertyChange(() => CanManageSequenceNumber);                                               
                                           };
        }

        private void Initializer()
        {
            groupData = new GroupData("Session");
            groupData.ControlDataCollection.Add(
                new ButtonData("Reset", "Reset", this)
                {
                    SmallImage = new Uri("/NetFixSimulator;component/Resources/Reset.png", UriKind.Relative),
                    ToolTipTitle = "Reset Sequence Numbers"
                });


            groupData.ControlDataCollection.Add(
               new ButtonData("SqNum", "ManageSequenceNumber", this)
               {
                   SmallImage = new Uri("/NetFixSimulator;component/Resources/Size Horz.png", UriKind.Relative),
                   ToolTipTitle = "Reset Sequence Numbers"
               });

            //groupData.ControlDataCollection.Add(
            //    new ButtonData("Stop", "Stop", this)
            //    {
            //        SmallImage = new Uri("/NetFixSimulator;component/Resources/Stop_16x16.png", UriKind.Relative),
            //        LargeImage = new Uri("/NetFixSimulator;component/Resources/Stop_32x32.png", UriKind.Relative),
            //        ToolTipTitle = "Stop Session"
            //    });

        }

        public void Reset()
        {
            _session.IncomingSequenceNumber = 1;
            _session.OutgoingSequenceNumber = 1;
        }

        public bool CanReset
        {
            get { return _session.SessionId != null; }
        }

        public void ManageSequenceNumber()
        {
            _windowManager.ShowDialog(new SequenceNumberViewModel(_session));

        }

        public bool CanManageSequenceNumber
        {
            get { return _session.SessionId != null; }
        }

    }
}

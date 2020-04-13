using System;
using AR.NetFixGateway;
using AR.NETFixSimulator.Models;
using Caliburn.Micro;
using Ninject;

namespace AR.NETFixSimulator.ViewModels
{
    class SqNumGroupViewModel : Conductor<object>
    {
        private readonly ISession _session;
        private IWindowManager _windowManager;

        public GroupData groupData { get; set; }

        [Inject]
        public SqNumGroupViewModel(ISession session, IWindowManager windowManager)
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
            groupData = new GroupData("Sequence Numbers");
            groupData.ControlDataCollection.Add(
                new ButtonData("Reset", "Reset", this)
                {
                    SmallImage = new Uri("/NetFixSimulator;component/Resources/Reset.png", UriKind.Relative),
                    ToolTipTitle = "Reset Incoming and Outgoing Sequence Numbers to 1"
                });


            groupData.ControlDataCollection.Add(
               new ButtonData("Set", "ManageSequenceNumber", this)
               {
                   SmallImage = new Uri("/NetFixSimulator;component/Resources/Size Horz.png", UriKind.Relative),
                   ToolTipTitle = "Set Incoming and Outgoing Sequence Numbers."
               });

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

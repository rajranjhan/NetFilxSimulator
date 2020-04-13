using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AR.NetFixGateway;
using Caliburn.Micro;
using System.Windows;
using Ninject;

namespace AR.NETFixSimulator.ViewModels
{
    public class SequenceNumberViewModel : Screen
    {
        [Inject]
        public  SequenceNumberViewModel(ISession  session)
        {
            _session = session;
        }

        public int Incoming
        {
            get { return _session.OutgoingSequenceNumber; }
            set { _incomingSqNum = value; }
        }

        public int Outgoing
        {
            get { return _session.IncomingSequenceNumber; }
            set { _outgoingSqNum = value; }
        }


        public void Ok()
        {
            _session.OutgoingSequenceNumber = _outgoingSqNum;
            _session.IncomingSequenceNumber = _incomingSqNum;
           TryClose(true);
        }

        private int _incomingSqNum;
        private  int _outgoingSqNum;
        private ISession _session;
    }
}

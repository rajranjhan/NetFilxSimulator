using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AR.NetFixGateway.Common;
using Ninject;
using QuickFix;

namespace AR.NetFixGateway.Engines
{
    public class IncomingMessageHandler42 : MessageCracker, IMessageHandler
    {
        [Inject]
        public IncomingMessageHandler42(IExecutionEngine executionEngine, ISession session)
        {
            _executionEngine = executionEngine;
            _session = session;
        }

        private IExecutionEngine _executionEngine;
        private ISession _session;

        public void CrackMessage(MessageHolder messageHolder)
        {
           Crack(messageHolder.Message, _session.SessionId);
        }

        public void OnMessage(QuickFix.FIX42.Logon n, SessionID s)
        {
           //No - op   
        }

        public void OnMessage(QuickFix.FIX42.Heartbeat n, SessionID s)
        {
            //No - op   
        }

        public void OnMessage(QuickFix.FIX42.NewOrderSingle n, SessionID s)
        {
            if(_executionEngine != null)
                _executionEngine.HandleNewOrder(n);
        }
      

    }
}

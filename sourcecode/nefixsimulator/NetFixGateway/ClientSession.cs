using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using QuickFix;

namespace AR.NetFixGateway
{

    /// <summary>
    /// Todo:  Place holder for future development
    /// </summary>
    public class ClientSession : ISession
    {
        private IInitiator _initiator = null;
        public IInitiator Initiator
        {
            set
            {
                if (_initiator != null)
                    throw new Exception("You already set the initiator");
                _initiator = value;
            }
            get
            {
                if (_initiator == null)
                    throw new Exception("You didn't provide an initiator");
                return _initiator;
            }
        }


        public void Start(string configFile)
        {
            Trace.WriteLine("QFApp::Start() called");
            if(Initiator.IsStopped)
                Initiator.Start();
            else
                Trace.WriteLine("(already started)");
            
        }
    
        public void Send(Message m)
        {         
        }
      
        public event Action<Message> MessageEvent;
       

        public bool IsConnected
        {
            get { throw new NotImplementedException(); }
        }


        public void Stop()
        {
            throw new NotImplementedException();
        }


        public event Action<bool> ConnectonEvent;
    }
}

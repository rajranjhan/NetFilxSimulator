using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using AR.NetFixGateway.Common;
using AR.NetFixGateway.Engines;
using Ninject;
using QuickFix;
//using Ninject.Moupdules;

namespace AR.NetFixGateway
{
    /// <summary>
    /// Server session.  
    /// </summary>
    public class ServerSession : ISession
    {  
        [Inject]
        public ServerSession(IApplication application, string configFile)
        {
             _application = application;     
            var CurrentSessionSettings = new SessionSettings(configFile);
            MessageStoreFactory storeFactory = new FileStoreFactory(CurrentSessionSettings);
            LogFactory logFactory = new FileLogFactory(CurrentSessionSettings);
            this.Acceptor = new ThreadedSocketAcceptor(_application, storeFactory, CurrentSessionSettings, logFactory);

            _sessionId = CurrentSessionSettings.GetSessions().FirstOrDefault();
            SessionState = SessionState.Idle;

            _application.MessageEvent += (message) =>
            {
                if (MessageEvent != null) MessageEvent( message);
            };
            _application.LogonEvent += (s) =>
            {
                _sessionId = s;
                SessionState |= SessionState.Connected;                               
            };
            _application.LogoutEvent += (s) =>
            {                
                SessionState = SessionState.LoggedOut;
            };
        }
        public event Action<MessageHolder> MessageEvent;
        private IApplication _application;

        public void Start()
        {
            this.Acceptor.Start();
            this.SessionState = SessionState.Started;         
        }

        public void Stop()
        {
           this.Acceptor.Stop();
           this.SessionState = SessionState.LoggingOut;
           
        }
     
        /// <summary>
        /// Reset the next Incoming and Outgoing sequence numbers to 1
        /// </summary>
        public void ResetSequenceNumbers()
        {
            Contract.Requires(_sessionId != null);
            Session.LookupSession(_sessionId).NextSenderMsgSeqNum = 1;
            Session.LookupSession(_sessionId).NextTargetMsgSeqNum = 1;
        }

        public void Send(Message message)
        {
            Contract.Requires(_sessionId != null);
            Session.SendToTarget(message, _sessionId);
        }
    
        public bool IsConnected
        {
            get
            {
                return ((_sessionState & SessionState.Connected) != 0);
            }
        }

        public bool IsStarted
        {
            get
            {
                return ((_sessionState & SessionState.Started) != 0);
            }
        }


        public int IncomingSequenceNumber
        {
            get
            {
                if (_sessionId != null)
                    return Session.LookupSession(_sessionId).NextSenderMsgSeqNum;
                else
                    return 0;
            }
            set
            {
                Contract.Requires(_sessionId != null);
                Session.LookupSession(_sessionId).NextSenderMsgSeqNum = value;                
            }
        }

        public int OutgoingSequenceNumber
        {
            get
            {
                if (_sessionId != null)
                    return Session.LookupSession(_sessionId).NextTargetMsgSeqNum;
                else
                    return 0;
            }
            set
            {
                Contract.Requires(_sessionId != null);
                Session.LookupSession(_sessionId).NextTargetMsgSeqNum = value;                
            }
        }

        public event Action<SessionState> StateChangeEvent;

        public SessionState SessionState
        {
            get
            {
                return _sessionState;
            }
            set
            {
                _sessionState = value;
                if (StateChangeEvent != null) StateChangeEvent(_sessionState);
            }
        }

        public ThreadedSocketAcceptor Acceptor
        {
            private set
            {
                if (_acceptor != null)
                    throw new Exception("You already set the acceptor");
                _acceptor = value;
            }
            get
            {
                if (_acceptor == null)
                    throw new Exception("You didn't provide an acceptor");
                return _acceptor;
            }
        }
        private ThreadedSocketAcceptor _acceptor = null;
        public SessionID SessionId
        {
            get
            {
                return _sessionId;
            }
        }
        
        private SessionID _sessionId = null;
        private SessionState _sessionState;
    }
}
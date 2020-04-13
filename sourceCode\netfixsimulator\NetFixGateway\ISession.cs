using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AR.NetFixGateway.Common;
using QuickFix;

namespace AR.NetFixGateway
{    
    [Flags]
    public  enum SessionState
    {
        Idle = 0,
        Started = 1,
        Connected = 2,
        LoggingOut = 4,
        LoggedOut = 8
    }

    public interface ISession
    {        
        event Action<MessageHolder> MessageEvent;
        event Action<SessionState> StateChangeEvent;
                
        SessionID SessionId { get;  }
        bool IsConnected { get; }
        bool IsStarted { get; }
        int IncomingSequenceNumber { get; set; }
        int OutgoingSequenceNumber { get; set; }
        
        void Send(Message message);        
        void Start();
        void Stop();

    }   
}

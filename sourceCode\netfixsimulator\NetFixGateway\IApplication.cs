using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AR.NetFixGateway.Common;

using QuickFix;

namespace AR.NetFixGateway
{
    public interface IApplication : Application
    {

        /// <summary>
        /// Triggered on any message sent or received (arg1: isIncoming)
        /// </summary>
        event Action<MessageHolder> MessageEvent;
        event Action<SessionID> LogonEvent;
        event Action<SessionID> LogoutEvent;



    }
}

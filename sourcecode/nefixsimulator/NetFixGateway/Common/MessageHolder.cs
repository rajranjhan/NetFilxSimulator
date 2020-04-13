using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickFix;

namespace AR.NetFixGateway.Common
{
    public struct MessageHolder
    {        
        public MessageHolder(Message msg, bool isIncoming, bool isAdmin = false) : this()
        {
            Message = msg;
            IsIncoming = isIncoming;
            IsAdmin = isAdmin;
        }

        public QuickFix.Message Message { get;  private set; }
        public bool IsIncoming { get;  private set; }
        public bool IsAdmin { get; private set; }
    }
}

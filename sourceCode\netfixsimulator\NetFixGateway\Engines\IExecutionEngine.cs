using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AR.NetFixGateway.Engines
{
    using QuickFix;

    public  enum RTPriceLevel
    {
        NEVER,
        WHENAVAILABLE,
        ALWAYS
    }

    public interface IExecutionEngine
    {
        void HandleNewOrder(Message newOrder);       
        RTPriceLevel RTPriceLevel { get; set; }
                       
    }
}

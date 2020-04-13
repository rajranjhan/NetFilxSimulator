using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickFix.FIX42;
using QuickFix.Fields;

namespace AR.NetFixGateway.Engines
{
    internal static class EngineHelper
    {
        internal static  ExecutionReport CopyFromOrder(this ExecutionReport executionReport, NewOrderSingle newOrderSingle )
        {
            executionReport.Set(newOrderSingle.Symbol);
            executionReport.Set(newOrderSingle.Side);
            executionReport.Set(newOrderSingle.OrdType);
            executionReport.Set(newOrderSingle.ClOrdID);
            executionReport.Set(newOrderSingle.OrderQty);
            if (newOrderSingle.IsSetAccount())
                executionReport.SetField(newOrderSingle.Account);

            return executionReport;
        }

    }
}

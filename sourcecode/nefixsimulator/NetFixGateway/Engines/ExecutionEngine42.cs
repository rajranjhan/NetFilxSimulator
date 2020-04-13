using QuickFix;
using System.Diagnostics.Contracts;
using Message = QuickFix.Message;
using NewOrderSingle = QuickFix.FIX42.NewOrderSingle;
using QuickFix.Fields;
using System;
using Ninject;
using System.Threading.Tasks;
using System.Threading;

namespace AR.NetFixGateway.Engines
{
    public class ExecutionEngine42 : ExecutionEngine   
    {
      
        [Inject]
        public ExecutionEngine42(ISession session, IExecutionEngineSettings executionEngineSettings)
            : base(session)
        {
            _executionEngineSettings = executionEngineSettings;
        }

        private IExecutionEngineSettings _executionEngineSettings;
       
        //TODO:  Move to background thread and pulse execreports
        public override void HandleNewOrder(Message newOrder)
        {
            Contract.Requires(_session != null);
            Contract.Requires(newOrder != null);
            Contract.Requires(newOrder is NewOrderSingle);
            NewOrderSingle newOrderSingle = (NewOrderSingle)newOrder;

            if (_executionEngineSettings.SendAck)
                SendAck(newOrderSingle);

            //TODO:  Add canceling mechanism
            if (_executionEngineSettings.SendExecs)
                Task.Factory.StartNew( () => SendExecReport(newOrderSingle, _executionEngineSettings));
        }

        private void SendAck(NewOrderSingle newOrderSingle)
        {
            var exReport = new QuickFix.FIX42.ExecutionReport();
            OrderQty orderQty = newOrderSingle.OrderQty;
            
            exReport.CopyFromOrder(newOrderSingle);
            exReport.Set(new OrderID(GenOrderID()));
            exReport.Set(new ExecID(GenExecID()));
            exReport.Set(new ExecTransType(ExecTransType.NEW));
            exReport.Set(new ExecType(ExecType.NEW));
            exReport.Set(new OrdStatus(OrdStatus.NEW));
            exReport.Set(new LeavesQty(orderQty.getValue()));
            exReport.Set(new CumQty(0));
            exReport.Set(new AvgPx(0));

            SendExecReport(exReport);
        }

        private void SendExecReport(QuickFix.FIX42.ExecutionReport exReport)
        {
            try
            {
                _session.Send(exReport);
            }
            catch (SessionNotFound ex)
            {
                Console.WriteLine("==session not found exception!==");
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void SendExecReport(NewOrderSingle newOrderSingle, IExecutionEngineSettings settings)
        {

            OrdType ordType = newOrderSingle.OrdType;
            Price price = new Price(10);

            switch (ordType.getValue())
            {
                case OrdType.LIMIT:
                    price = newOrderSingle.Price;
                    if (price.Obj == 0)
                        throw new IncorrectTagValue(price.Tag);
                    break;
                case OrdType.MARKET:
                    break;
                default:
                    throw new IncorrectTagValue(ordType.Tag);
            }

            OrderQty orderQty = newOrderSingle.OrderQty;
            var exReport = new QuickFix.FIX42.ExecutionReport();

            exReport.CopyFromOrder(newOrderSingle);
            exReport.Set(new ExecTransType(ExecTransType.NEW));
            exReport.Set(new ExecType(ExecType.FILL));
            exReport.Set(new LastPx(price.getValue()));
            exReport.Set(new AvgPx(price.getValue()));

            decimal qtyPerSlice = orderQty.getValue() * 
                ((settings.EachFillPercentange ?? 100) / 100m);

            decimal leaves = orderQty.getValue();
            decimal cumQty = 0;

            while (leaves != 0)
            {
                if (leaves <= qtyPerSlice)
                {
                    leaves = 0;
                    qtyPerSlice = leaves;
                    exReport.Set(new OrdStatus(OrdStatus.FILLED));
                }
                else
                {
                    leaves -= qtyPerSlice;               
                    exReport.Set(new OrdStatus(OrdStatus.PARTIALLY_FILLED));
                }
                
                cumQty += qtyPerSlice;
                exReport.Set(new OrderID(GenOrderID()));
                exReport.Set(new ExecID(GenExecID()));

                exReport.Set(new LeavesQty(leaves));
                exReport.Set(new CumQty(cumQty));                

                exReport.Set(new LastShares(qtyPerSlice));              
                SendExecReport(exReport);
                Thread.Sleep(Convert.ToInt32(settings.SecondsBetweenFills * 1000));
            }
        }
    }
}

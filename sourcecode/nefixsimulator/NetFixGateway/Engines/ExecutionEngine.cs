using Ninject;
using Ninject.Modules;
using Message = QuickFix.Message;
using System.Threading;


namespace AR.NetFixGateway.Engines
{
    public abstract class ExecutionEngine : IExecutionEngine
    {

        [Inject]
        public ExecutionEngine(ISession session)
        {
            _session = session;
        }

        protected ISession _session;
        
        protected static string GenOrderID()
        {
            int newValue = Interlocked.Increment(ref _orderID);
            return _orderID.ToString();
        }

        protected static string GenExecID()
        {
            int newValue = Interlocked.Increment(ref _execID);
            return _execID.ToString();
        }
        private static int _orderID = 1;
        private static int _execID = 1;

        public abstract void HandleNewOrder(Message newOrder);

        public RTPriceLevel RTPriceLevel
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
                throw new System.NotImplementedException();
            }
        }
    }
}


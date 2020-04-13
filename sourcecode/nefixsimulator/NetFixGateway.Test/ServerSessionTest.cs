using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AR.NetFixGateway;


namespace NetFixGateway.Test
{
    [TestFixture]
    public class ServerSessionTest
    {
        
        private ISession _session;
        
        [SetUp]
        public void Init()
        {
            IApplication application = new QuickFixApplication();
            _session = new ServerSession(application, "NetFixSimulatorTest.cfg");
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AR.NetFixGateway.Engines;
using Ninject.Modules;

namespace AR.NETFixSimulator.Infrastructure
{
    //TODO:  Pick by the configuration
    public class FixModules42 : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IExecutionEngine>().To<ExecutionEngine42>();
        }
    }
}

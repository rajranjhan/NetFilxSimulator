// -----------------------------------------------------------------------
// <copyright file="BootStrapper.cs" >
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AR.NETFixSimulator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using AR.NetFixGateway;

    using Caliburn.Micro;

    using Ninject;
    using System.Reflection;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
    //    "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable",
    //    Justification = "Entry class is never Disposed, cleanup handled already by OnExit")]
    public sealed class BootStrapper : Bootstrapper<IShellViewModel>
    {
        #region Fields
        private IKernel _kernel;
        #endregion

        //Override logger for Caliburn with Log4net
        static BootStrapper()
        {
            LogManager.GetLog = t => new Log4netLogger(t);
        }

        #region Base functionality overrides
        protected override void Configure()
        {
            _kernel = new StandardKernel();

            _kernel.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            _kernel.Bind<IEventAggregator>().To<EventAggregator>().InSingletonScope();

            _kernel.Bind<IShellViewModel>().To<ShellViewModel>().InSingletonScope();
            _kernel.Bind<ISession>().To<ServerSession>();
            _kernel.Load(Assembly.GetExecutingAssembly());
            _kernel.Load("*.dll");
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            _kernel.Dispose();
            base.OnExit(sender, e);
        }
        #endregion

        #region IoC method overrides
        protected override object GetInstance(Type serviceType, string key)
        {
            if (serviceType != null)
            {
                return _kernel.Get(serviceType);
            }

            throw new ArgumentNullException("serviceType");
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        protected override void BuildUp(object instance)
        {
            _kernel.Inject(instance);
        }
        #endregion
    }
}

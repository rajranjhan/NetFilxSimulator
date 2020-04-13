// -----------------------------------------------------------------------
// <copyright file="BootStrapper.cs" >
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

using AR.NETFixSimulator.Models;
using AR.NETFixSimulator.ViewModels;
using AR.NetFixGateway.Common;
using AR.NetFixGateway.Engines;
using Caliburn.Micro;
using Ninject;

namespace AR.NETFixSimulator.Infrastructure
{
    using System.Windows;

    using AR.NetFixGateway;

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
            _kernel.Bind<ILoginViewModel>().To<LoginViewModel>().InSingletonScope();
            _kernel.Bind<ISession>().To<ServerSession>().InSingletonScope().WithConstructorArgument("configFile",
                                                                                                    "NetFixSimulator.cfg");
            _kernel.Bind<IConfigurationStore>().To<IniStore>().WithConstructorArgument("path", Directory.GetCurrentDirectory()+ "\\NetFixSimulator.cfg");
            _kernel.Bind<IMessageHandler>().To<IncomingMessageHandler42>();
            _kernel.Bind<IExecutionEngineSettings>().To<ExecutionEngineSettings>();  
                              
            _kernel.Load(Assembly.GetExecutingAssembly());
            _kernel.Load("*.dll");
            
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            StartLogin();
            StartMainApplication();
           
            Application.Shutdown();
        }

        bool StartLogin()
        {
            var login = _kernel.Get<LoginViewModel>();
            var windowManager = _kernel.Get<IWindowManager>();
            windowManager.ShowDialog(login);
            return login.IsLoginValid;
        }

        void StartMainApplication()
        {
            var shell = _kernel.Get<IShellViewModel>();
            var windowManager = _kernel.Get<IWindowManager>();
            windowManager.ShowDialog(shell);
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

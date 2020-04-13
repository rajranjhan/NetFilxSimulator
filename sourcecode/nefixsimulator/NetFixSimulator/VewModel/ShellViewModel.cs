// -----------------------------------------------------------------------
// <copyright file="ShellViewModel.cs" company="MNA">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------


using AR.NetFixGateway;
using Caliburn.Micro;
using Ninject;
using Message = QuickFix.Message;

namespace AR.NETFixSimulator
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>    
    public class ShellViewModel : PropertyChangedBase, IShellViewModel
    {
        private ISession _session;

        [Inject]
        public void SetSession(ISession session)
        {
            _session = session;
            session.MessageEvent += message => { messages.Add(message); };
        }

        public ShellViewModel(RibbonViewModel ribbonViewModel)
        {
            RibbonVM = ribbonViewModel;
        }

        public RibbonViewModel RibbonVM { get; private set; }

        ObservableCollection<Message> messages  = new ObservableCollection<Message>();
        
    }
}

// -----------------------------------------------------------------------
// <copyright file="RibbonViewModel.cs" company="MNA">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.ObjectModel;
using AR.NetFixGateway;
using AR.NETFixSimulator.Models;
using Caliburn.Micro;
using Ninject;

namespace AR.NETFixSimulator.ViewModels

{   
    /// <summary>
    /// TODO: Update summary.
    /// </summary>    
    public class RibbonViewModel : PropertyChangedBase
    {


        [Inject]
        public ExecutionTabViewModel ExecutionTabViewModel
        {
            set;
            get;
        }

        [Inject]
        public RibbonViewModel(ISession session, IWindowManager windowManager)
        {
            _session = session;
            _windowManager = windowManager;
        }

        private ISession _session;    

        public ObservableCollection<TabData> TabDataCollection
        {
            get
            {
                if (_tabDataCollection == null)
                {
                    _tabDataCollection = new ObservableCollection<TabData>();
                    _tabDataCollection.Insert(0, GetHomeTab());
                    _tabDataCollection.Insert(1, ExecutionTabViewModel);
                }
                return _tabDataCollection;
            }
        }
        private ObservableCollection<TabData> _tabDataCollection;

         
        public ObservableCollection<ContextualTabGroupData> ContextualTabGroupDataCollection 
        { 
            get
            {
                if(_contextualTabGroupDataCollection == null)
                {
                    _contextualTabGroupDataCollection = new ObservableCollection<ContextualTabGroupData>();
                }
                return _contextualTabGroupDataCollection;
            }             
        }

        private ObservableCollection<ContextualTabGroupData> _contextualTabGroupDataCollection;
        private IWindowManager _windowManager;

        private TabData GetHomeTab()
        {
            TabData tabData = new TabData("Home");

            tabData.GroupDataCollection.Add(new ConnectionGroupViewModel(_session).groupData);
            tabData.GroupDataCollection.Add(new SqNumGroupViewModel(_session, _windowManager).groupData);           
            return tabData;
        }       
    }
}

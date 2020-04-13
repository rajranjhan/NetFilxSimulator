// -----------------------------------------------------------------------
// <copyright file="RibbonViewModel.cs" company="MNA">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
  
using AR.NetFixGateway;
using Caliburn.Micro;
using System.Collections.ObjectModel;
using AR.NETFixSimulator.Model;
using Ninject;
using AR.NETFixSimulator.ViewModel;

namespace AR.NETFixSimulator
{   
    /// <summary>
    /// TODO: Update summary.
    /// </summary>    
    public class RibbonViewModel : PropertyChangedBase
    {        

        [Inject]
        public RibbonViewModel(ISession session)
        {
            _session = session;
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
                    _tabDataCollection.Insert(1, new TabData("Configuration"));
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

        private TabData GetHomeTab()
        {
            TabData tabData = new TabData("Home");

            tabData.GroupDataCollection.Add(new ConnectionGroupViewModel(_session));
            tabData.GroupDataCollection.Add(new GroupData("Session"));

            return tabData;
        }

       
    }
}

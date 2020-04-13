// -----------------------------------------------------------------------
// <copyright file="RibbonData.cs" company="MNA">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using AR.NETFixSimulator.Infrastructure;
using AR.NETFixSimulator.ViewModel;
using AR.NetFixGateway;
using Ninject;

namespace AR.NETFixSimulator.Model
{
   
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RibbonData : INotifyPropertyChanged
    {
        private ISession _session;

        [Inject]
        public RibbonData(ISession session)
        {
            _session = session;
        }

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

        private TabData GetHomeTab()
        {
            TabData tabData = new TabData("Home"); 
            
            //TODO Replace this with the Factory
            IKernel kernel = new StandardKernel(new ConnectionGroupModule());
            var connectionModel = kernel.Get<ConnectionGroupViewModel>();

            tabData.GroupDataCollection.Add(connectionModel);
            tabData.GroupDataCollection.Add(new GroupData("Session"));

            return tabData;
        }
       
        private ObservableCollection<TabData> _tabDataCollection;        

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        #endregion
    }
}

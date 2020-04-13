// -----------------------------------------------------------------------
// <copyright file="RibbonData.cs" company="MNA">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AR.NETFixSimulator.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Collections.ObjectModel;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RibbonData : INotifyPropertyChanged
    {

        public ObservableCollection<TabData> TabDataCollection
        {
            get
            {
                if (_tabDataCollection == null)
                {
                    _tabDataCollection = new ObservableCollection<TabData>();                    
                    _tabDataCollection.Insert(0, GetHomeTab());
                    _tabDataCollection.Insert(1, new TabData("Actions"));                   
                }
                return _tabDataCollection;
            }
        }

        private TabData GetHomeTab()
        {
            TabData tabData = new TabData("Home"); 
            
            tabData.GroupDataCollection.Add(new ControlGroupViewModel());
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

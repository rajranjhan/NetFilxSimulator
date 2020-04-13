// -----------------------------------------------------------------------
// <copyright file="Tabdata.cs" company="MNA">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace AR.NETFixSimulator.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.Remoting.Messaging;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TabData : INotifyPropertyChanged
    {
        public TabData()
            : this(null)
        {
        }

        public TabData(string header)
        {
            Header = header;
        }

        public string Header
        {
            get
            {
                return _header;
            }

            set
            {
                if (_header != value)
                {
                    _header = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("Header"));
                }
            }
        }
        private string _header;        
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }

            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged(new PropertyChangedEventArgs("IsSelected"));
                }
            }
        }
        private bool _isSelected;

        public ObservableCollection<GroupData> GroupDataCollection
        {
            get
            {
                if (_groupDataCollection == null)
                {
                    _groupDataCollection = new ObservableCollection<GroupData>();                    
                }
                return _groupDataCollection;
            }
        }
        private ObservableCollection<GroupData> _groupDataCollection;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        #endregion
    }
}

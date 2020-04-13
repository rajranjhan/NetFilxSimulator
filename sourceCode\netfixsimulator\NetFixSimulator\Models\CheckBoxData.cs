using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using Caliburn.Micro;

namespace AR.NETFixSimulator.Models
{
    public class CheckBoxData : BoundedControlData<bool>
    {

        public CheckBoxData(string actionName, object target) :
            base(actionName, target)
        {
            
        }

        public bool IsChecked
        {
            get
            {
                return _isChecked = valueGetter.Invoke();
            }

            set
            {
                if (_isChecked != value)
                {
                    valueSetter.Invoke(value);
                    OnPropertyChanged(new PropertyChangedEventArgs("IsChecked"));
                }
            }
        }
        private bool _isChecked;
      
    }
}

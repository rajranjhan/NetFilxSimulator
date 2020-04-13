using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace AR.NETFixSimulator.Models
{
    public class TextBoxData : BoundedControlData<String>
    {
      
        public TextBoxData(string actionName, object target) : base(actionName, target)
        {      

        }            
            
        public string Text
        {
            get
            {
                return _text = valueGetter.Invoke();
            }

            set
            {
                if (_text != value)
                {
                    valueSetter.Invoke(value);
                    OnPropertyChanged(new PropertyChangedEventArgs("Text"));
                }
            }
        }
        private string _text;                 
     
    }
}

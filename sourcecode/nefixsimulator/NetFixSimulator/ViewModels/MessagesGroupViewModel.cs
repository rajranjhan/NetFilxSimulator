using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AR.NETFixSimulator.Models;
using Caliburn.Micro;
using Ninject;
using AR.NetFixGateway.Engines;

namespace AR.NETFixSimulator.ViewModels
{
    public class MessagesGroupViewModel : PropertyChangedBase
    {
        public GroupData groupData { get; set; }
                
        public MessagesGroupViewModel()
        {
            groupData = new GroupData("Behavior");            

            groupData.ControlDataCollection.Add(
               new CheckBoxData("AutoExec", this)
               {
                   Label = "Auto Execute"
               });

            groupData.ControlDataCollection.Add(
              new CheckBoxData("AutoAck", this)
              {
                  Label = "Send Ack"
              });

            groupData.ControlDataCollection.Add(
                 new TextBoxData("Percentage", this)
                 {
                     Label = "Fill Percentage",
                     ToolTipTitle = "Each fill Percentage",
                     ToolTipDescription = "Percentage of the total qty of each fill."
                 });

            _percentage = UserSettings.Default.EachFillPercentange;
            

            groupData.ControlDataCollection.Add(
                 new TextBoxData("Delay", this)
                 {
                     Label = "Fill Delay",
                     ToolTipTitle = "Seconds delay between each fill",
                     ToolTipDescription = "For partially filled orders this is the delay between each execution."
                 });

            _delay = UserSettings.Default.EachFillDelay;
            
        }        

        public bool AutoExec
        {
            get 
            {
                return UserSettings.Default.AutoExec;
            }

            set
            {                
                UserSettings.Default.AutoExec = value;
                UserSettings.Default.Save();                                
            }
        }

        public bool AutoAck
        {
            get
            {
                return UserSettings.Default.AutoAck;
            }

            set
            {             
                UserSettings.Default.AutoAck = value;
                UserSettings.Default.Save();
            }
        }

        public string Delay
        {
            get { return _delay.ToString(); }
            set
            {
                float tryValue;
                if (float.TryParse(value, out tryValue))
                {                    
                    UserSettings.Default.EachFillDelay = _delay = tryValue;
                    UserSettings.Default.Save();
                }
            }
        }
        float _delay;

        public string Percentage
        {
            get { return _percentage.ToString(); }
            set
            {
                Int32 tryValue;
                if (Int32.TryParse(value, out tryValue))
                {
                    UserSettings.Default.EachFillPercentange =  _percentage = tryValue;                     
                    UserSettings.Default.Save();
                }
            }
        }
        int _percentage;
    }
}

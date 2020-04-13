using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AR.NetFixGateway.Engines;

namespace AR.NETFixSimulator.Models
{    
    public class ExecutionEngineSettings : IExecutionEngineSettings
    {
        public bool SendExecs
        {
            get
            {
                return UserSettings.Default.AutoExec;
            }
            set
            {
                //No-op
            }
        }

        public bool SendAck
        {
            get
            {
                return UserSettings.Default.AutoAck;
            }
            set
            {
                //No-op
            }
        }

        public int? EachFillPercentange
        {
            get
            {
                return
                    (UserSettings.Default.EachFillPercentange > 0) ? 
                    UserSettings.Default.EachFillPercentange : new Nullable<int>();
            }
            set
            {
                //No-op
            }
        }

        public float SecondsBetweenFills
        {
            get
            {
                return UserSettings.Default.EachFillDelay;
            }
            set
            {
                //No-op
            }
        }
    }
}

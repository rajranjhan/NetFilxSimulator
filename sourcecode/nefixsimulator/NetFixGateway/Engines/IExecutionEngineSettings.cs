using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AR.NetFixGateway.Engines
{
    public  interface IExecutionEngineSettings
    {
        bool SendExecs { get; set; }
        bool SendAck { get; set; }
        int? EachFillPercentange { get; set; }
        float SecondsBetweenFills { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AR.NetFixGateway.Common;

namespace AR.NetFixGateway.Engines
{
    public interface IMessageHandler
    {
       void CrackMessage(MessageHolder messageHolder);
    }
}

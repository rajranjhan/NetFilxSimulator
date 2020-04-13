using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AR.NetFixGateway.Engines
{
    public interface IRealTimePriceEngine
    {
        decimal GetPrice(string tickerSymbol);
        long GetVolume(string tickerSymbol);
        bool Subscribe(string tickerSymbol);
        void UnSubscribe(string tickerSymbol);
    }
}

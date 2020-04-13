using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AR.NetFixGateway.Engines
{
    public interface IMarketFeedService
    {
        StockPriceList GetPrices(string[] tickers);
    }

    public class StockPriceList : Dictionary<string, decimal> 
    {

    }
}

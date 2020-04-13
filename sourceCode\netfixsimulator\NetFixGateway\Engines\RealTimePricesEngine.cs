using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace AR.NetFixGateway.Engines
{
    public class RealTimePricesEngine : PollingClass, IRealTimePriceEngine, IDisposable
    {
        //private IEventAggregator EventAggregator { get; set; }
        private readonly object _lockObject = new object();

        private readonly Dictionary<string, SubscriptionInfo> _priceList = new Dictionary<string, SubscriptionInfo>();
        private readonly IMarketFeedService marketFeed = new YahooMarketService();

        //public RealTimePricesEngine(IEventAggregator eventAggregator)
        //{
        //    EventAggregator = eventAggregator;
        //    doWorkFunc = UpdatePrices;
        //    base.Start();
        //}

        protected override void SetTimoutValue()
        {
            _refreshInterval = 20000;
        }

        
        protected void UpdatePrices()
        {   
            StockPriceList stockPrices = null;
            lock (_lockObject)
            {
               stockPrices =  marketFeed.GetPrices(_priceList.Keys.ToArray());
            }

            foreach (var stockPrice in stockPrices)
            {
                _priceList[stockPrice.Key].price = stockPrice.Value;
            }

            OnMarketPricesUpdated();
        }

        private void OnMarketPricesUpdated()
        {
            Dictionary<string, decimal> clonedPriceList = new Dictionary<string, decimal>();
            lock (_lockObject)
            {
                foreach (var item in _priceList)
                    clonedPriceList.Add(item.Key, item.Value.price);
            }
            //EventAggregator.GetEvent<MarketPricesUpdatedEvent>().Publish(clonedPriceList);
        }

        public decimal GetPrice(string tickerSymbol)
        {
            if (_priceList.ContainsKey(tickerSymbol))
                return _priceList[tickerSymbol].price;

            return Decimal.Zero;
        }

        public long GetVolume(string tickerSymbol)
        {
            throw new NotImplementedException();
        }

        public bool Subscribe(string tickerSymbol)
        {
            SubscriptionInfo info = null;
           
            try
            {
                //Check with google if this is a valid symbol
                if ( _priceList.TryGetValue(tickerSymbol, out info) == false)
                    _priceList[tickerSymbol] = new SubscriptionInfo();
                else
                    info.numberOfSubscriptions = Interlocked.Increment(ref _priceList[tickerSymbol].numberOfSubscriptions);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public void UnSubscribe(string tickerSymbol)
        {
            SubscriptionInfo info = null;

            try
            {
                //Check with google if this is a valid symbol
                if (_priceList.TryGetValue(tickerSymbol, out info) == true)
                {
                    info.numberOfSubscriptions = Interlocked.Decrement(ref _priceList[tickerSymbol].numberOfSubscriptions);
                    if (info.numberOfSubscriptions < 1)
                        _priceList.Remove(tickerSymbol);
                }
            }
            catch
            {
            }
        }

        private class SubscriptionInfo
        {
            public decimal price;
            public long numberOfSubscriptions;
        }
    }

}

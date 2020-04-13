using System;
using System.IO;
using System.Net;
using System.Text;

namespace AR.NetFixGateway.Engines
{
    public class YahooMarketService : IMarketFeedService
    {
        #region IMarketFeedService Members

        public StockPriceList GetPrices(string[] tickerList)
        {
            var priceList = new StockPriceList();

            HttpWebRequest req;
            HttpWebResponse res;
            StreamReader sr;
            string strResult;
            string fullpath;
            var tickers = new StringBuilder();

            foreach (string item in tickerList)
                tickers.Append(item + '+');

            if (tickers.Length > 1)
                tickers.Remove(tickers.Length - 1, 1);
            else
                return priceList;

            fullpath = @"http://quote.yahoo.com/d/quotes.csv?s=" + tickers + "&f=sl1&e=.csv";
            try
            {
                req = (HttpWebRequest) WebRequest.Create(fullpath);
                res = (HttpWebResponse) req.GetResponse();
                using (sr = new StreamReader(res.GetResponseStream(), Encoding.ASCII))
                {
                    strResult = sr.ReadLine();
                    while (string.IsNullOrEmpty(strResult) == false)
                    {
                        string[] words = strResult.Split(',');
                        decimal price;

                        if (Decimal.TryParse(words[1], out price) && price > 0M)
                        {
                            string ticker = words[0].Replace("\"", "");
                            priceList[ticker] = price;
                        }
                        strResult = sr.ReadLine();
                    }
                }
            }
            catch (Exception)
            {
            }
            return priceList;
        }

        #endregion
    }
}
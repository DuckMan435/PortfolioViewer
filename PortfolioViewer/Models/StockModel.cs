using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace PortfolioViewer.Models
{
    public class StockModel : SecurityModel
    {
        #region Properties

        private string name;
        public override string Name
        {
            get
            {
                return Quote.Name;
            }
            set
            {
                name = value;
            }
        }

        public string Symbol { get; set; }

        public int Quantity { get; set; }

        public double PurchasePrice { get; set; }

        public double Cost
        {
            get
            {
                return PurchasePrice * Quantity;
            }
        }

        #region Quote Properties

        private QuotesModel quote;
        public QuotesModel Quote
        {
            get
            {
                // Lazy load the Stock Quote
                if (quote == null)
                    GetQuote();

                return quote;
            }
        }

        public double CurrentPrice
        {
            get
            {
                return double.Parse(Quote.LastTradePriceOnly);
            }
        }

        public double MarketValue
        {
            get
            {
                return CurrentPrice * Quantity;
            }
        }

        private double dividend;
        public virtual double Dividend
        {
            get
            {
                if (Quote.DividendYield != null)
                    dividend = double.Parse(Quote.DividendYield.ToString());

                return dividend;
            }
            set
            {
                dividend = value;
            }
        }

        public virtual double CurrentValue
        {
            get
            {
                return (Quantity * CurrentPrice) + ((Dividend / CurrentPrice) * Quantity);
            }
        }

        public virtual double Gain
        {
            get
            {
                return CurrentValue - Cost;
            }
        }

        public double PercentageGain
        {
            get
            {
                return Gain / Cost;
            }
        }

        #endregion

        #region Override Properties

        public override string SecurityType
        {
            get
            {
                return "Stock";
            }
        }

        #endregion

        #endregion

        #region Methods

        private void GetQuote()
        {
            string requestUri = $"http://query.yahooapis.com/v1/public/yql?q=select * from yahoo.finance.quotes where symbol in ('{Symbol}')&env=store://datatables.org/alltableswithkeys&format=json";

            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetAsync(requestUri).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    string responseString = responseContent.ReadAsStringAsync().Result;

                    JObject json = JObject.Parse(responseString);
                    JObject quoteJson = JObject.Parse(json["query"]["results"]["quote"].ToString());

                    quote = quoteJson.ToObject<QuotesModel>();
                }
            }
        }

        #endregion
    }
}
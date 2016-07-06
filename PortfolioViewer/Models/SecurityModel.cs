using System;

namespace PortfolioViewer.Models
{
    public abstract class SecurityModel
    {
        #region Properties

        public int Id { get; set; }

        public virtual string Name { get; set; }

        public abstract string SecurityType { get; }

        public DateTime? PurchaseDate { get; set; }

        #endregion
    }

    /// <summary>
    /// Created from Quote JSON retrieved from Yahoo Finance API
    /// </summary>
    public class QuotesModel
    {
        #region Properties

        public string symbol { get; set; }
        public object Ask { get; set; }
        public object AverageDailyVolume { get; set; }
        public object Bid { get; set; }
        public object AskRealtime { get; set; }
        public object BidRealtime { get; set; }
        public string BookValue { get; set; }
        public string Change_PercentChange { get; set; }
        public string Change { get; set; }
        public object Commission { get; set; }
        public string Currency { get; set; }
        public object ChangeRealtime { get; set; }
        public object AfterHoursChangeRealtime { get; set; }
        public object DividendShare { get; set; }
        public string LastTradeDate { get; set; }
        public object TradeDate { get; set; }
        public object EarningsShare { get; set; }
        public object ErrorIndicationreturnedforsymbolchangedinvalid { get; set; }
        public object EPSEstimateCurrentYear { get; set; }
        public object EPSEstimateNextYear { get; set; }
        public string EPSEstimateNextQuarter { get; set; }
        public object DaysLow { get; set; }
        public object DaysHigh { get; set; }
        public object YearLow { get; set; }
        public object YearHigh { get; set; }
        public object HoldingsGainPercent { get; set; }
        public object AnnualizedGain { get; set; }
        public object HoldingsGain { get; set; }
        public object HoldingsGainPercentRealtime { get; set; }
        public object HoldingsGainRealtime { get; set; }
        public object MoreInfo { get; set; }
        public object OrderBookRealtime { get; set; }
        public object MarketCapitalization { get; set; }
        public object MarketCapRealtime { get; set; }
        public object EBITDA { get; set; }
        public object ChangeFromYearLow { get; set; }
        public object PercentChangeFromYearLow { get; set; }
        public object LastTradeRealtimeWithTime { get; set; }
        public object ChangePercentRealtime { get; set; }
        public object ChangeFromYearHigh { get; set; }
        public object PercebtChangeFromYearHigh { get; set; }
        public string LastTradeWithTime { get; set; }
        public string LastTradePriceOnly { get; set; }
        public object HighLimit { get; set; }
        public object LowLimit { get; set; }
        public object DaysRange { get; set; }
        public object DaysRangeRealtime { get; set; }
        public object FiftydayMovingAverage { get; set; }
        public object TwoHundreddayMovingAverage { get; set; }
        public object ChangeFromTwoHundreddayMovingAverage { get; set; }
        public object PercentChangeFromTwoHundreddayMovingAverage { get; set; }
        public object ChangeFromFiftydayMovingAverage { get; set; }
        public object PercentChangeFromFiftydayMovingAverage { get; set; }
        public string Name { get; set; }
        public object Notes { get; set; }
        public object Open { get; set; }
        public string PreviousClose { get; set; }
        public object PricePaid { get; set; }
        public string ChangeinPercent { get; set; }
        public object PriceSales { get; set; }
        public object PriceBook { get; set; }
        public object ExDividendDate { get; set; }
        public object PERatio { get; set; }
        public object DividendPayDate { get; set; }
        public object PERatioRealtime { get; set; }
        public string PEGRatio { get; set; }
        public object PriceEPSEstimateCurrentYear { get; set; }
        public object PriceEPSEstimateNextYear { get; set; }
        //public string Symbol { get; set; }
        public object SharesOwned { get; set; }
        public object ShortRatio { get; set; }
        public string LastTradeTime { get; set; }
        public object TickerTrend { get; set; }
        public object OneyrTargetPrice { get; set; }
        public string Volume { get; set; }
        public object HoldingsValue { get; set; }
        public object HoldingsValueRealtime { get; set; }
        public object YearRange { get; set; }
        public object DaysValueChange { get; set; }
        public object DaysValueChangeRealtime { get; set; }
        public string StockExchange { get; set; }
        public object DividendYield { get; set; }
        public string PercentChange { get; set; }

        #endregion
    }
}
using System;
using LinqToYql.Attributes;

namespace YQLDataProvider.Models
{
    public class YqlQuote
    {
        [YqlIgnore]
        public Guid Id { get; set; }
        public YqlCompany Company { get; set; }
        public float? Ask { get; set; }
        public long? AverageDailyVolume { get; set; }
        public float? Bid { get; set; }
        public float? AskRealtime { get; set; }
        public float? BidRealtime { get; set; }
        public float? BookValue { get; set; }
        public float? Change { get; set; }
        public string Commission { get; set; }
        public float? ChangeRealtime { get; set; }
        public string AfterHoursChangeRealtime { get; set; }
        public float? DividendShare { get; set; }
        public DateTime? LastTradeDate { get; set; }
        public DateTime? TradeDate { get; set; }
        public float? EarningsShare { get; set; }
        public string ErrorIndicationReturnedForSymbolChangedInvalid { get; set; }
        public float? EPSEstimateCurrentYear { get; set; }
        public float? EPSEstimateNextYear { get; set; }
        public float? EPSEstimateNextQuarter { get; set; }
        public float? DaysLow { get; set; }
        public float? DaysHigh { get; set; }
        public float? YearLow { get; set; }
        public float? YearHigh { get; set; }
        public string HoldingsGainPercent { get; set; }
        public string AnnualizedGain { get; set; }
        public string HoldingsGain { get; set; }
        public string HoldingsGainPercentRealtime { get; set; }
        public string HoldingsGainRealtime { get; set; }
        public string MoreInfo { get; set; }
        public string OrderBookRealtime { get; set; }
        public string MarketCapitalization { get; set; }
        public string MarketCapRealtime { get; set; }
        public string EBITDA { get; set; }
        public float? ChangeFromYearLow { get; set; }
        public string PercentChangeFromYearLow { get; set; }
        public string LastTradeRealtimeWithTime { get; set; }
        public string ChangePercentRealtime { get; set; }
        public float? ChangeFromYearHigh { get; set; }
        public string PercebtChangeFromYearHigh { get; set; }
        public string LastTradeWithTime { get; set; }
        public float? LastTradePriceOnly { get; set; }
        public float? HighLimit { get; set; }
        public float? LowLimit { get; set; }
        public string DaysRange { get; set; }
        public string DaysRangeRealtime { get; set; }
        public float? FiftydayMovingAverage { get; set; }
        public float? TwoHundreddayMovingAverage { get; set; }
        public float? ChangeFromTwoHundreddayMovingAverage { get; set; }
        public string PercentChangeFromTwoHundreddayMovingAverage { get; set; }
        public float? ChangeFromFiftydayMovingAverage { get; set; }
        public string PercentChangeFromFiftydayMovingAverage { get; set; }
        public string Name { get; set; }
        public string Notes { get; set; }
        public float? Open { get; set; }
        public float? PreviousClose { get; set; }
        public float? PricePaid { get; set; }
        public string ChangeinPercent { get; set; }
        public float? PriceSales { get; set; }
        public float? PriceBook { get; set; }
        public string ExDividendDate { get; set; }
        public float? PERatio { get; set; }
        public DateTime? DividendPayDate { get; set; }
        public string PERatioRealtime { get; set; }
        public float? PEGRatio { get; set; }
        public float? PriceEPSEstimateCurrentYear { get; set; }
        public float? PriceEPSEstimateNextYear { get; set; }
        [YqlKey("symbol")]
        public string Symbol { get; set; }
        public string SharesOwned { get; set; }
        public float? ShortRatio { get; set; }
        public string LastTradeTime { get; set; }
        public string TickerTrend { get; set; }
        public float? OneyrTargetPrice { get; set; }
        public int? Volume { get; set; }
        public string HoldingsValue { get; set; }
        public string HoldingsValueRealtime { get; set; }
        public string YearRange { get; set; }
        public string DaysValueChange { get; set; }
        public string DaysValueChangeRealtime { get; set; }
        public string StockExchange { get; set; }
        public float? DividendYield { get; set; }
        public string PercentChange { get; set; }
    }
}

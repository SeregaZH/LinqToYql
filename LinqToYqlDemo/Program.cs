using LinqToYql;
using LinqToYql.Interfaces;
using System.Xml.Linq;
using System.Linq;
using YQLDataProvider;
using YQLDataProvider.Models;
using YQLDataProvider.YqlDataLoaders;
using System;

namespace LinqToYqlDemo
{
    class Program
    {
        private const string SectorsConfigKey = "sectors";
        private const string IndustryConfigKey = "industries";
        private const string QuoteConfigKey = "quotes";
        private const string XchangeConfigKey = "xchangeRates";

        static void Main(string[] args)
        {
            const int industryKey = 112;
            ISettingsFactory factory = new SettingsFactory();
  
            IQuerySettings industrySettings = factory.CreateSettings(IndustryConfigKey);
            var industryQuery = new QuerableYqlData<YqlIndustry, XDocument, YqlIndustry>(industrySettings, new XmlIndustryDataLoader());
            var resIndustry = from industry in industryQuery where industry.Key == industryKey select industry;
            var fIndustry = resIndustry.FirstOrDefault();
            Console.WriteLine("Industry id:{0} name:{1} sector:{2}", fIndustry.Key, fIndustry.Name, fIndustry.Sector);

            IQuerySettings quoteSettings = factory.CreateSettings(QuoteConfigKey);
            var twoCompaniesSymbols = fIndustry.Companies.Take(2).Select(x => x.Symbol).ToList();
            var quoteQuery = new QuerableYqlData<YqlQuote, XDocument, YqlQuote>(quoteSettings, new XmlQuoteDataLoader());
            var quoteis = from quote in quoteQuery
                     where twoCompaniesSymbols.Contains(quote.Symbol)
                     select new YqlQuote
                     {
                         Id = quote.Id,
                         Name = quote.Name,
                         Symbol = quote.Symbol,
                         Ask = quote.Ask,
                         Bid = quote.Bid,
                         TradeDate = quote.TradeDate,
                         DaysLow = quote.DaysLow,
                         DaysHigh = quote.DaysHigh,
                         Volume = quote.Volume
                     };
            foreach (var quote in quoteis)
            {
                Console.WriteLine("Sym: {0}, Bid {1}, Ask {2}", quote.Symbol, quote.Bid, quote.Ask);
            }
            Console.ReadKey();
        }
    }
}

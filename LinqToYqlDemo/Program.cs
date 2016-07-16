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
            ISettingsFactory factory = new SettingsFactory();
            IQuerySettings sectorSettings = factory.CreateSettings(SectorsConfigKey);
            var query = new QuerableYqlData<YqlIndustrySector, XDocument, YqlIndustrySector>(sectorSettings, new XmlSectorsDataLoader());
            var allSectors = from sectors in query select sectors;
            var firstSector = allSectors.FirstOrDefault();
            Console.WriteLine("Sector id:{0} name:{1}", firstSector.Id, firstSector.Name);

            var firstIndustry = firstSector.Industries.FirstOrDefault();
            IQuerySettings industrySettings = factory.CreateSettings(IndustryConfigKey);
            var industryQuery = new QuerableYqlData<YqlIndustry, XDocument, YqlIndustry>(industrySettings, new XmlIndustryDataLoader());
            var resIndustry = from industry in industryQuery where firstIndustry.Key == industry.Key select industry;
            var fIndustry = resIndustry.FirstOrDefault();
            Console.WriteLine("Industry id:{0} name:{1} sector:{2}", fIndustry.Key, fIndustry.Name, fIndustry.Sector);

            var twoCompaniesSymbols = fIndustry.Companies.Take(2).Select(x => x.Symbol).ToList();
            var quoteQuery = new QuerableYqlData<YqlQuote, XDocument, YqlQuote>(sectorSettings, new XmlQuoteDataLoader());
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
        }
    }
}

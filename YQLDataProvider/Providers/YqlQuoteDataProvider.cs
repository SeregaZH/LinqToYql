//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Xml.Linq;
//using HelperTools;
//using Interfaces.Criteria;
//using Interfaces.Filters;
//using Interfaces.Models;
//using Interfaces.Providers;
//using LinqToYql;
//using YQLDataProvider.Models;
//using YQLDataProvider.YqlDataLoaders;

//namespace YQLDataProvider.Providers
//{
//    public class YqlQuoteDataProvider : BaseEconomicDataProvider<XmlQuoteDataLoader>, IEconomicDataProvider<IEnumerable<IQuote>,IQuoteisFilter,IKeySupportedCriteria>
//    {
//        public YqlQuoteDataProvider() : base(new XmlQuoteDataLoader()){}

//        public IEnumerable<IQuote> GetData(IKeySupportedCriteria criteria, IQuoteisFilter filter)
//        {
//            var querySettings = SettingsFactory.CreateSettings(QuoteKey);
//            var query = new QuerableYQLData<YqlQuote, XDocument, YqlQuote>(querySettings, DataLoader);
//            IEnumerable<YqlQuote> result;
//            if (criteria.IsLazyLoading)
//            {
//                result = from quote in query
//                         where criteria.Identificators.Contains(quote.Symbol)
//                         select new YqlQuote
//                             {
//                                 Id = quote.Id,
//                                 Name = quote.Name,
//                                 Symbol = quote.Symbol,
//                                 Ask = quote.Ask,
//                                 Bid = quote.Bid,
//                                 TradeDate = quote.TradeDate,
//                                 DaysLow = quote.DaysLow,
//                                 DaysHigh = quote.DaysHigh,
//                                 Volume = quote.Volume
//                             };
//            }
//            else
//                result = from quote in query where criteria.Identificators.Contains(quote.Symbol) select quote;
//            return filter.Returns(x => x.Filtrate(result), result) as IEnumerable<IQuote>;
//        }
//    }
//}

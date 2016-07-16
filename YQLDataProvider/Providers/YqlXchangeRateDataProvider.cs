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
//    public class YqlXchangeRateDataProvider : BaseEconomicDataProvider<XmlXchangeDataLoader>, IEconomicDataProvider<IEnumerable<IXchangeRate>,IXchangeRateFilter,IKeySupportedCriteria>
//    {
//        public YqlXchangeRateDataProvider() : base(new XmlXchangeDataLoader()){}

//        public IEnumerable<IXchangeRate> GetData(IKeySupportedCriteria criteria, IXchangeRateFilter filter)
//        {
//            var query =
//                new QuerableYQLData<YqlXchangeRate, XDocument, YqlXchangeRate>(
//                    SettingsFactory.CreateSettings(XchangeKey), DataLoader);
//            IEnumerable<IXchangeRate> result;
//            if (criteria.IsLazyLoading)
//            {
//                result = from rate in query
//                         where criteria.Identificators.Contains(rate.Pair.PairId)
//                         select new YqlXchangeRate
//                             {
//                                 Id = rate.Id,
//                                 Ask = rate.Ask,
//                                 Bid = rate.Bid,
//                                 TradeDate = rate.TradeDate,
//                                 Rate = rate.Rate
//                             };
//            }
//            else
//                result = from rate in query where criteria.Identificators.Contains(rate.Pair.PairId) select rate;
//            return filter.Returns(x => x.Filtrate(result), result) as IEnumerable<IXchangeRate>;
//        }
//    }
//}

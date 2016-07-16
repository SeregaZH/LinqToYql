//using System.Collections.Generic;
//using System.Linq;
//using System.Xml.Linq;
//using AutoMapper;
//using Framework.Dependency;
//using HelperTools;
//using Interfaces.Criteria;
//using Interfaces.Filters;
//using Interfaces.Models;
//using Interfaces.Providers;
//using LinqToYql;
//using Microsoft.Practices.Unity;
//using YQLDataProvider.Models;
//using YQLDataProvider.YqlDataLoaders;

//namespace YQLDataProvider.Providers
//{
//    public class YqlIndustrySectorsDataProvider : BaseEconomicDataProvider<XmlSectorsDataLoader>, IEconomicDataProvider<IEnumerable<IIndustrySector>,ISectorsFilter,IBaseCriteria>
//    {
//        public YqlIndustrySectorsDataProvider() : base(new XmlSectorsDataLoader()){}

//        public IEnumerable<IIndustrySector> GetData(IBaseCriteria criteria, ISectorsFilter filter)
//        {
//            var querySettings = SettingsFactory.CreateSettings(SectorsKey);
//            var query = new QuerableYQLData<YqlIndustrySector, XDocument, YqlIndustrySector>(querySettings, DataLoader);
//            IEnumerable<IIndustrySector> result = null;
//            if (criteria.IsLazyLoading)
//                result = from sectors in query select new YqlIndustrySector {Id = sectors.Id, Name = sectors.Name};
//            else
//            {
//                if (criteria.IsFullLoading)
//                {
//                    var sectors = from sector in query select sector;
//                    var industryProvider =
//                        DependencyManager.Container
//                                         .Resolve
//                            <IEconomicDataProvider<IEnumerable<IIndustry>, IIndustryFilter, IKeySupportedCriteria>>();
//                    var keysCriteria = Mapper.DynamicMap<IKeySupportedCriteria>(criteria);
                    
//                    foreach (var industrySector in sectors)
//                    {
//                        keysCriteria.Identificators = from indKey in industrySector.Industries select indKey.Key as object;
//                        var industriesWithCompany = industryProvider.GetData(keysCriteria, filter.With(x=>x.IndustryFilter));
//                        industrySector.Industries = industriesWithCompany.ToList();
//                    }
//                }
//                else
//                    result = from sectors in query select sectors;
//            }
//            return filter.Returns(x => x.Filtrate(result), result) as IEnumerable<IIndustrySector>;
//        }
//    }
//}

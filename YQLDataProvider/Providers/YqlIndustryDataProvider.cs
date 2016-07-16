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
//    public class YqlIndustryDataProvider : BaseEconomicDataProvider<XmlIndustryDataLoader>, IEconomicDataProvider<IEnumerable<IIndustry>, IIndustryFilter, IKeySupportedCriteria>
//    {
//        public YqlIndustryDataProvider() : base(new XmlIndustryDataLoader()){}

//        public IEnumerable<IIndustry> GetData(IKeySupportedCriteria criteria, IIndustryFilter filter)
//        {
//            var querySettings = SettingsFactory.CreateSettings(IndustryKey);
//            var query = new QuerableYQLData<YqlIndustry, XDocument, YqlIndustry>(querySettings, DataLoader);
//            IEnumerable<IIndustry> result  = null;
//            if(criteria.IsLazyLoading)
//            {
//                result = from industry in query
//                             where criteria.Identificators.Contains(industry.Key)
//                             select new YqlIndustry {Id = industry.Id, Key = industry.Key, Name = industry.Name};
//            }
//            else
//            {
//                if (criteria.IsFullLoading)
//                {
//                    result = from industry in query where criteria.Identificators.Contains(industry.Key) select industry;
//                    var quoteCriteria = Mapper.DynamicMap<IKeySupportedCriteria>(criteria);
//                    var quoteProvider =
//                        DependencyManager.Container.Resolve
//                            <IEconomicDataProvider<IEnumerable<IQuote>, IQuoteisFilter, IKeySupportedCriteria>>();
//                    foreach (var industry in result)
//                    {
//                        var filteredCompanies = filter.With(y=>y.CompanyFilter).Returns(x=>x.Filtrate(industry.Companies), new List<ICompany>()) as IEnumerable<ICompany>;
//                        quoteCriteria.Identificators = from key in filteredCompanies select key.Symbol;
//                        var quoteis = quoteProvider.GetData(quoteCriteria, filter.With(x=>x.CompanyFilter).With(y=>y.QuoteisFilter));
//                        foreach (var quote in quoteis)
//                        {
//                            var company = industry.Companies.FirstOrDefault(comp => comp.Symbol == quote.Symbol);
//                            if(company != null)
//                            {
//                                company.Quoteis.Add(quote);
//                                quote.Company = company;
//                            }
//                        }
//                    }
//                }
//                else
//                    result = from industry in query where criteria.Identificators.Contains(industry.Key) select industry;
//            }
//            return filter.Returns(x=>x.Filtrate(result),result) as IEnumerable<IIndustry>;
//        }
//    }
//}

using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using LinqToYql.Interfaces;
using YQLDataProvider.Mappings;
using YQLDataProvider.Models;
using HelperTools;
using AutoMapper;

namespace YQLDataProvider.YqlDataLoaders
{
    public class XmlIndustryDataLoader : IDataLoader
    {
        public IEnumerable<object> LoadData(object source)
        {
            var document = source as XDocument;
            if(document == null)
                return new List<YqlIndustry>();
            var element = document.Root.With(x => x.Element("results")).With(y => y.Elements("industry"));
            XDocumentToYqlDataMappings.CreateIndustryMap();
            var industries = Mapper.Map<IEnumerable<XElement>, IEnumerable<YqlIndustry>>(element).ToList();

            foreach (var industry in industries)
                foreach (var company in industry.With(x=>x.Companies))
                    company.Industry = industry;

            return industries;
        }
    }
}

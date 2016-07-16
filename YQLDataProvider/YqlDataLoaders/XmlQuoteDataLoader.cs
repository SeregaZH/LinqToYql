using System.Collections.Generic;
using System.Xml.Linq;
using LinqToYql.Interfaces;
using YQLDataProvider.Mappings;
using YQLDataProvider.Models;
using HelperTools;
using AutoMapper;

namespace YQLDataProvider.YqlDataLoaders
{
    public class XmlQuoteDataLoader : IDataLoader
    {
        public IEnumerable<object> LoadData(object source)
        {
            var document = source as XDocument;
            if (document == null) return new List<YqlQuote>();
            var sRes = document.Root.With(x => x.Element("results")).With(y => y.Elements("quote"));

            XDocumentToYqlDataMappings.CreateQuoteMap();
            var result = Mapper.Map<IEnumerable<XElement>, IEnumerable<YqlQuote>>(sRes);
            return result;
        }
    }
}

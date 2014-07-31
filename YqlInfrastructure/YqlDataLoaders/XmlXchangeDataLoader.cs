using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using LinqToYql.Interfaces;
using YQLDataProvider.Mappings;
using YQLDataProvider.Models;

namespace YQLDataProvider.YqlDataLoaders
{
  public class XmlXchangeDataLoader : IDataLoader
  {
    public IEnumerable<object> LoadData(object source)
    {
      var document = source as XDocument;
      if (document == null)
        return new LinkedList<YqlXchangeRate>();
      XDocumentToYqlDataMappings.CreateXChangeRateMap();
      var xchangeCollection = document.Root.With(x => x.Element("results")).With(y => y.Elements("rate"));
      var rates = AutoMapper.Mapper.Map<IEnumerable<XElement>, IEnumerable<YqlXchangeRate>>(xchangeCollection).ToList();
      foreach (var xchangeRate in rates)
        xchangeRate.Pair.Rateses.Add(xchangeRate);
      return rates;
    }


  }
}

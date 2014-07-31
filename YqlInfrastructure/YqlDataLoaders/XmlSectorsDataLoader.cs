using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using LinqToYql.Interfaces;
using YQLDataProvider.Mappings;
using YQLDataProvider.Models;

namespace YQLDataProvider.YqlDataLoaders
{
  public class XmlSectorsDataLoader : IDataLoader
  {
    public IEnumerable<object> LoadData(object source)
    {
      var document = source as XDocument;
      var result = new List<YqlIndustrySector>();

      if (document != null)
      {
        XDocumentToYqlDataMappings.CreateSectorsMap();
        var sectorNodes = document.Root.With(x => x.Element("results")).With(y => y.Elements("sector"));
        var sectors = AutoMapper.Mapper.Map<IEnumerable<XElement>, IEnumerable<YqlIndustrySector>>(sectorNodes).ToList();

        foreach (var industrySector in sectors)
          foreach (var industry in industrySector.Industries)
            industry.Sector = industrySector;
        result = sectors;
      }
      return result;
    }
  }
}

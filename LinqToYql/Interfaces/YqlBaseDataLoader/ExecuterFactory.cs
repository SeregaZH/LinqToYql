using System;
using System.Xml.Linq;

namespace LinqToYql.Interfaces.YqlBaseDataLoader
{
  internal static class ExecuterFactory
  {
    public static IQueryExecuter CreateQueryExecuter(Type resultType, string query)
    {
      if (resultType == typeof (XDocument))
        return new XmlQueryExecuter(query);

      return null;
    }
  }
}

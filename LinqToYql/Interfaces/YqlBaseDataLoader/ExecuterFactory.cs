using System;
using System.Xml.Linq;
using LinqToYql.Interfaces;

namespace LinqToYql.Interfaces.YqlBaseDataLoader
{
    internal static class ExecuterFactory 
    {
        public static IQueryExecuter CreateQueryExecuter(Type resultType, string query)
        {
            if (resultType.Equals(typeof (XDocument)))
                return new XmlQueryExecuter(query);

            return null;
        }
    }
}

using System;
using System.Threading;
using System.Xml.Linq;
using LinqToYql.Interfaces;

namespace LinqToYql
{
    internal class XmlQueryExecuter : IQueryExecuter
    {
        private readonly string _query;

        public XmlQueryExecuter(string query)
        {
            _query = query;
        }

        public object Execute()
        {
            return LoadData();
        }

        public object LoadData()
        {
            return XDocument.Load(_query);
        }
    }
}

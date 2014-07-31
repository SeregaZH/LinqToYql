using System.Text;

namespace LinqToYql
{
    internal class YqlQueryBuilder
    {
        private readonly StringBuilder _query;
        private readonly string _tableName;
        private readonly string _postfixExtension;
        private readonly bool _includeExtensionTable;
        private readonly string _basicUrl;

        public YqlQueryBuilder(string basicUrl, bool includeExtensionTable, string tableName, string postfixExtension)
        {
            _basicUrl = basicUrl;
            _includeExtensionTable = includeExtensionTable;
            _tableName = tableName;
            _postfixExtension = postfixExtension;
            _query = new StringBuilder();
        }

        public string Build(string selectQuery, string whereQuery)
        {
            var query = _query.Append(_basicUrl)
                              .Append("?q=")
                              .Append(ReplaceSimbols(selectQuery))
                              .Append(string.Format("from {0}", _tableName))
                              .Append(ReplaceSimbols(whereQuery));
            if (_includeExtensionTable)
                query.Append(string.Format("&{0}", _postfixExtension));
            return query.ToString();

        }
       
        private static string ReplaceSimbols(string source)
        {
            return source.Replace(" ", "%20").Replace("\"", "%22").Replace("=", "%3D");
        }
    }
}

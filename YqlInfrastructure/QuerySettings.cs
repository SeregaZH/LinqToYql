using LinqToYql.Interfaces;

namespace YQLDataProvider
{
  public class QuerySettings : IQuerySettings
  {
    private const string DefaultBaseUri = "http://query.yahooapis.com/v1/public/yql";
    private const string DefaultPostfix = "env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

    public QuerySettings(string tableName, string baseUri = DefaultBaseUri, string postfix = DefaultPostfix,
                         bool isExtensionsTableIncluded = true)
    {
      IsExtensionsTableIncluded = isExtensionsTableIncluded;
      Postfix = postfix;
      TableName = tableName;
      BaseUri = baseUri;
    }

    public QuerySettings(string tableName)
    {
      IsExtensionsTableIncluded = true;
      BaseUri = DefaultBaseUri;
      Postfix = DefaultPostfix;
      TableName = tableName;
    }

    public string BaseUri { get; private set; }
    public bool IsExtensionsTableIncluded { get; private set; }
    public string TableName { get; private set; }
    public string Postfix { get; private set; }
  }
}

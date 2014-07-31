using LinqToYql.Interfaces;

namespace YQLDataProvider
{
  public interface ISettingsFactory
  {
    IQuerySettings CreateSettings(string tableName);
  }
}

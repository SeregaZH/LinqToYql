using System.Collections.Generic;

namespace LinqToYql.Interfaces
{
  public interface IDataLoader
  {
    IEnumerable<object> LoadData(object source);
  }
}

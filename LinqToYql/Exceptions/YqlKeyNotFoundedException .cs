using System;

namespace LinqToYql.Exceptions
{
  public class YqlKeyNotFoundedException : Exception
  {
    public YqlKeyNotFoundedException(string message)
      : base(message)
    {
    }
  }
}

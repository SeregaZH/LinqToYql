using System;

namespace LinqToYql.Attributes
{
  public class YqlNameAttribute : Attribute
  {
    public YqlNameAttribute(string name)
    {
      Name = name;
    }

    public string Name { get; private set; }
  }
}

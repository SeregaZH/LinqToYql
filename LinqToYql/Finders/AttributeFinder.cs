using System;
using System.Linq;
using LinqToYql.Attributes;

namespace LinqToYql.Finders
{
  internal static class NameFinder
  {
    public static string GetPropertyName(string propertyName, Type type)
    {
      var prop = type.GetProperty(propertyName);
      var attributes = prop.GetCustomAttributes(typeof (YqlNameAttribute), false);
      if (attributes.Any())
      {
        var nameAttribute = attributes.FirstOrDefault(x => x is YqlNameAttribute) as YqlNameAttribute;
        return nameAttribute != null ? nameAttribute.Name : propertyName;
      }

      return propertyName;
    }

    public static bool IsIgnored(string propName, Type type)
    {
      var prop = type.GetProperty(propName);
      var attributes = prop.GetCustomAttributes(typeof (YqlIgnore), false);
      return attributes.Any();
    }
  }
}

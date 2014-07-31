using System;
using System.Linq;
using LinqToYql.Attributes;

namespace LinqToYql.Helpers
{
  internal static class KeyFinder
  {
    private const string Id = "ID";

    public static string[] GetKeysForType(Type type)
    {
      foreach (var prop in type.GetProperties())
      {
        var keysAttr = prop.GetCustomAttributes(typeof (YqlKeyAttribute), false);
        return keysAttr.Select(x => typeof (YqlKeyAttribute).GetProperty("KeyName").GetValue(x) as string).ToArray();
      }
      return new string[] {};
    }

    public static bool PropertyIsKey(string propName, Type type)
    {
      var prop = type.GetProperty(propName);
      return prop != null && prop.GetCustomAttributes(typeof (YqlKeyAttribute), false).Any();
    }

    public static string GetKeyNameForProperty(string propName, Type type)
    {
      var prop = type.GetProperty(propName);
      var attributes = prop.GetCustomAttributes(typeof (YqlKeyAttribute), false);
      if (attributes.Any())
      {
        var keyAttribute = attributes.FirstOrDefault(x => x is YqlKeyAttribute) as YqlKeyAttribute;
        return keyAttribute != null ? keyAttribute.KeyName : null;
      }
      return null;
    }
  }
}
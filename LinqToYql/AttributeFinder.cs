using System;
using System.Linq;
using LinqToYql.Attributes;

namespace LinqToYql
{
    internal static class NameFinder
    {
        public static string GetPropertyName(string propertyName, Type type)
        {
            var prop = type.GetProperty(propertyName);
            var attributes = prop.GetCustomAttributes(typeof (YqlNameAttribute), false);
            return attributes.Any() ? attributes.Select(x => (x as YqlNameAttribute).Name).FirstOrDefault() : propertyName;
        }

        public static bool IsIgnored(string propName, Type type)
        {
            var prop = type.GetProperty(propName);
            var attributes = prop.GetCustomAttributes(typeof(YqlIgnore), false);
            return attributes.Any();
        }
    }
}

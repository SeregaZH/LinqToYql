using System;
using System.Linq;
using LinqToYql.Attributes;

namespace LinqToYql
{
    internal static class KeyFinder
    {
        private const string ID = "ID";

        public static string[] GetKeysForType(Type type)
        {
            foreach (var prop in type.GetProperties())
            {
                var keysAttr = prop.GetCustomAttributes(typeof(YqlKeyAttribute),false);
                return keysAttr.Select(x=>typeof(YqlKeyAttribute).GetProperty("KeyName").GetValue(x) as string).ToArray();
            }
            return new string[]{};
        }

        public static bool PropertyIsKey(string propName, Type type)
        {
            var prop = type.GetProperty(propName);
            return prop != null && prop.GetCustomAttributes(typeof (YqlKeyAttribute), false).Any();
        }

        public static string GetKeyNameForProperty(string propName, Type type)
        {
            var prop = type.GetProperty(propName);
            return prop.GetCustomAttributes(typeof(YqlKeyAttribute), false).Select(x=>(x as YqlKeyAttribute).KeyName).FirstOrDefault();
        }

    }
}

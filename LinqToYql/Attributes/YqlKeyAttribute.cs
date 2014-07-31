using System;

namespace LinqToYql.Attributes
{
    public class YqlKeyAttribute : Attribute
    {
        public YqlKeyAttribute(string keyName)
        {
            KeyName = keyName;
        }

        public string KeyName { get; private set; }
    }
}

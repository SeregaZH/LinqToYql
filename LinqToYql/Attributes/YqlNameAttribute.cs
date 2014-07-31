using System;

namespace LinqToYql.Attributes
{
    public class YqlNameAttribute : Attribute
    {
        public string Name { get;private set; }

        public YqlNameAttribute(string name)
        {
            Name = name;
        }
    }
}

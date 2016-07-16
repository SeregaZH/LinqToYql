using System.Configuration;

namespace YQLDataProvider.Configuration
{
    public class TableNameElement : ConfigurationElement
    {
        [ConfigurationProperty("key", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string Key
        {
            get { return ((string)(base["key"])); }
            set { base["key"] = value; }
        }

        [ConfigurationProperty("name", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string Name
        {
            get { return ((string)(base["name"])); }
            set { base["name"] = value; }
        }

    }
}

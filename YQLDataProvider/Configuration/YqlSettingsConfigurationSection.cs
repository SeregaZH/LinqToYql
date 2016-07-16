using System.Configuration;

namespace YQLDataProvider.Configuration
{
    public class YqlSettingsConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("baseUri", DefaultValue = "http://query.yahooapis.com/v1/public/yql", IsKey = false)]
        public string BaseUri
        {
            get { return ((string)(base["baseUri"])); }
            set { base["baseUri"] = value; }
        }

        [ConfigurationProperty("urlPostfix", DefaultValue = "env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys", IsKey = false)]
        public string UrlPostfix
        {
            get { return ((string)(base["urlPostfix"])); }
            set { base["urlPostfix"] = value; }
        }

        [ConfigurationProperty("isExtendedTableRequired",DefaultValue = false, IsKey = false)]
        public bool IsExtendedTableRequired
        {
            get { return ((bool)(base["isExtendedTableRequired"])); }
            set { base["isExtendedTableRequired"] = value; }
        }

        [ConfigurationProperty("tableNames")]
        public TablesNameCollection TableNames
        {
            get { return base["tableNames"] as TablesNameCollection; }
        }
    }
}

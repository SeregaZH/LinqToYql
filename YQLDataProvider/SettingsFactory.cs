using System.Configuration;
using LinqToYql.Interfaces;
using YQLDataProvider.Configuration;
using YQLDataProvider.Exceptions;

namespace YQLDataProvider
{
    public class SettingsFactory : ISettingsFactory
    {
        private const string DefaultSettingsSectionName = "yqlSettings";

        public IQuerySettings CreateSettings(string tableName)
        {
            var yqlSettingsConfigurationSection =
                ConfigurationManager.GetSection(DefaultSettingsSectionName) as YqlSettingsConfigurationSection;
            
            if (yqlSettingsConfigurationSection == null)
                throw new InvalidConfigurationException(
                    string.Format("Invalid configuration file. {0} section doesn't exist", DefaultSettingsSectionName));

            var yqlTableName = yqlSettingsConfigurationSection.TableNames[tableName];
            var yqlBaseUri = yqlSettingsConfigurationSection.BaseUri;
            var yqlPostfix = yqlSettingsConfigurationSection.UrlPostfix;
            var isExtendedTablesIncluded = yqlSettingsConfigurationSection.IsExtendedTableRequired;

            if (yqlTableName == null)
                throw new InvalidConfigurationException(
                    string.Format("Invalid configuration file. {0} section doesn't exist table name with key: {1}",
                                  DefaultSettingsSectionName, tableName));

            return new QuerySettings(yqlTableName.Name, yqlBaseUri, yqlPostfix, isExtendedTablesIncluded);
        }
    }
}

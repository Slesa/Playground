using System.Configuration;

namespace Infrastructure.Configuration
{
    public class AppSettingsConfigurationReader : IConfigurationReader
    {
        public string ValueOf(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
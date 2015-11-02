using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Web.Configuration;
using Training.Common.Configuration;

namespace Training.Web.ConfigurationManagers
{
    [ExcludeFromCodeCoverage]
    public class WebConfigConfigurationManager : IConfigurationManager
    {
        public string GetAppSetting(string key)
        {
            return WebConfigurationManager.AppSettings[key];
        }

        public ConnectionStringSettings GetConnectionString(string key)
        {
            return WebConfigurationManager.ConnectionStrings[key];
        }
    }
}

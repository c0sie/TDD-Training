using System.Configuration;

namespace Training.Common.Configuration
{
    public interface IConfigurationManager
    {
        string GetAppSetting(string key);

        ConnectionStringSettings GetConnectionString(string key);
    }
}

using StructureMap.Configuration.DSL;
using Training.Common.Configuration;
using Training.Web.ConfigurationManagers;

namespace Training.Web.DependencyResolution.Registries
{
    public class CommonRegistry : Registry
    {
        public CommonRegistry()
        {
            Scan(c =>
                    {
                        c.Assembly("Training.Common");
                        c.WithDefaultConventions();
                    }
                );
            For<IConfigurationManager>().Use<WebConfigConfigurationManager>();
        }
    }
}
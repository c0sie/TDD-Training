using StructureMap.Configuration.DSL;

namespace Training.Web.DependencyResolution.Registries
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry()
        {
            Scan(c =>
                    {
                        c.Assembly("Training.Service");
                        c.WithDefaultConventions();
                    }
                );
        }
    }
}

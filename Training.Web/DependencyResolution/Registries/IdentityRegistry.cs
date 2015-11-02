using System.Web;
using Microsoft.Owin;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;

namespace Training.Web.DependencyResolution.Registries
{
    public class IdentityRegistry : Registry
    {
        public IdentityRegistry()
        {
            For<IOwinContext>().Use(() => HttpContext.Current.GetOwinContext()).LifecycleIs<TransientLifecycle>();
        }
    }
}

using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;
using Training.Entities.Models;

namespace Training.Web.DependencyResolution.Registries
{
    public class IdentityRegistry : Registry
    {
        public IdentityRegistry()
        {
            For<IOwinContext>().Use(() => HttpContext.Current.GetOwinContext()).LifecycleIs<TransientLifecycle>();

            // IUSerStore and IAuthenticionManaager For<> code below resolves issues with calling these interfaces.
            For<IUserStore<User, int>>().Use<UserStore<User, Role, int, UserLogin, UserRole, UserClaim>>();
            For<IAuthenticationManager>().Use(() => HttpContext.Current.GetOwinContext().Authentication);
        }
    }
}

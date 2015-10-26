using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Training.Web.Startup))]
namespace Training.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

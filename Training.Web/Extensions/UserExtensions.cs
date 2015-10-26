using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace Training.Web.Extensions
{
    public static class UserExtensions
    {
        public static int GetUserId(this IPrincipal user)
        {
            return user.Identity.GetUserId<int>();
        }
    }
}

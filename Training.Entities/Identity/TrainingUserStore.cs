using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Training.Entities.Models;

namespace Training.Entities.Identity
{
    public class TrainingUserStore : UserStore<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public TrainingUserStore(DbContext context)
            : base(context)
        {
        }
    }
}

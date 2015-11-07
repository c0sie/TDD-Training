using Microsoft.AspNet.Identity;
using Training.Entities.Models;

namespace Training.Entities.Identity
{
    public class TrainingUserManager : UserManager<User, int>
    {
        public TrainingUserManager(IUserStore<User, int> store)
            : base(store)
        {
            Context = (TrainingDbContext)((TrainingUserStore)Store).Context;
        }

        public TrainingDbContext Context { get; private set; }
    }
}

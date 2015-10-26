using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Training.Entities.Models;

namespace Training.Entities
{
    public class TrainingDbContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        public TrainingDbContext() : base("DefaultConnection")
        {
        }

        #region DbSets

        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<League> Leagues { get; set; }
        public virtual DbSet<Match> Matches { get; set; }

        #endregion

        public static TrainingDbContext Create()
        {
            return new TrainingDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            MapIdentityTables(modelBuilder);
        }

        private static void MapIdentityTables(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<UserClaim>().ToTable("UserClaims");
            modelBuilder.Entity<UserLogin>().ToTable("IdentityUserLogins");
        }
    }
}

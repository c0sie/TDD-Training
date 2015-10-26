namespace Training.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class IdentityRefactor : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UserLogins", newName: "IdentityUserLogins");
        }

        public override void Down()
        {
            RenameTable(name: "dbo.IdentityUserLogins", newName: "UserLogins");
        }
    }
}

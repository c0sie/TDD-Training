namespace Training.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teamupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Teams", "Name", c => c.String());
            AddColumn("dbo.Matches", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Matches", "IsDeleted");
            DropColumn("dbo.Teams", "Name");
        }
    }
}

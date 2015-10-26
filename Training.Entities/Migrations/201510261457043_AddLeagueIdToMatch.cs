namespace Training.Entities.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddLeagueIdToMatch : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Leagues",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    IsDeleted = c.Boolean(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Teams",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    LeagueId = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Leagues", t => t.LeagueId)
                .Index(t => t.LeagueId);

            CreateTable(
                "dbo.Matches",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    HomeTeamId = c.Int(nullable: false),
                    AwayTeamId = c.Int(nullable: false),
                    HomeScore = c.Int(nullable: false),
                    AwayScore = c.Int(nullable: false),
                    LeagueId = c.Int(nullable: false),
                    MatchDateTime = c.DateTime(nullable: false),
                    Team_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.AwayTeamId)
                .ForeignKey("dbo.Teams", t => t.HomeTeamId)
                .ForeignKey("dbo.Leagues", t => t.LeagueId)
                .ForeignKey("dbo.Teams", t => t.Team_Id)
                .Index(t => t.HomeTeamId)
                .Index(t => t.AwayTeamId)
                .Index(t => t.LeagueId)
                .Index(t => t.Team_Id);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Matches", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.Matches", "LeagueId", "dbo.Leagues");
            DropForeignKey("dbo.Matches", "HomeTeamId", "dbo.Teams");
            DropForeignKey("dbo.Matches", "AwayTeamId", "dbo.Teams");
            DropForeignKey("dbo.Teams", "LeagueId", "dbo.Leagues");
            DropIndex("dbo.Matches", new[] { "Team_Id" });
            DropIndex("dbo.Matches", new[] { "LeagueId" });
            DropIndex("dbo.Matches", new[] { "AwayTeamId" });
            DropIndex("dbo.Matches", new[] { "HomeTeamId" });
            DropIndex("dbo.Teams", new[] { "LeagueId" });
            DropTable("dbo.Matches");
            DropTable("dbo.Teams");
            DropTable("dbo.Leagues");
        }
    }
}

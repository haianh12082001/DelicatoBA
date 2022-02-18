namespace DelicatoBA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTeam : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Image = c.String(maxLength: 500),
                        Office = c.String(),
                        Body = c.String(),
                        Sort = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.OurTeams");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OurTeams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Office = c.String(),
                        Body = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Teams");
        }
    }
}

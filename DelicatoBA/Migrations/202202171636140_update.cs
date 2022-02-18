namespace DelicatoBA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArticleCategories", "ShowFooter", c => c.Boolean(nullable: false));
            DropColumn("dbo.ArticleCategories", "ShowHome");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ArticleCategories", "ShowHome", c => c.Boolean(nullable: false));
            DropColumn("dbo.ArticleCategories", "ShowFooter");
        }
    }
}

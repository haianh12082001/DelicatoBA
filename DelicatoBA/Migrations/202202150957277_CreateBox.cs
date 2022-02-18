namespace DelicatoBA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateBox : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Box", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "Box");
        }
    }
}

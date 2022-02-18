namespace DelicatoBA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Banners", "Number", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Banners", "Number");
        }
    }
}

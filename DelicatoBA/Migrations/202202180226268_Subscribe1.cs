
namespace DelicatoBA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Subscribe1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subscribes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(maxLength: 50),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Subscribes");
        }
    }
}

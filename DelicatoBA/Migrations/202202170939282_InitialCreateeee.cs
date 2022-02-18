namespace DelicatoBA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreateeee : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Contacts", "Mobile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contacts", "Mobile", c => c.String(nullable: false));
        }
    }
}

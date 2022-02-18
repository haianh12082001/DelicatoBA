namespace DelicatoBA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatelinkgooglemap : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConfigSites", "LinkGoogleMap", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConfigSites", "LinkGoogleMap");
        }
    }
}

namespace FamiDesk.Mobile.App.MobileAppService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "FamilyInformations", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.People", "FamilyInformations");
        }
    }
}

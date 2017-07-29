namespace FamiDesk.Mobile.App.MobileAppService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "Avatar", c => c.String());
            AddColumn("dbo.People", "Address", c => c.String());
            AddColumn("dbo.Users", "Login", c => c.String());
            AddColumn("dbo.Users", "Profession", c => c.String());
            AddColumn("dbo.Users", "Avatar", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Avatar");
            DropColumn("dbo.Users", "Profession");
            DropColumn("dbo.Users", "Login");
            DropColumn("dbo.People", "Address");
            DropColumn("dbo.People", "Avatar");
        }
    }
}

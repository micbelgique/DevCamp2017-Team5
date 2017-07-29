namespace FamiDesk.Mobile.App.MobileAppService.Migrations
{
	using FamiDesk.Mobile.App.MobileAppService.DataObjects;
	using FamiDesk.Mobile.App.MobileAppService.Models;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<FamiDesk.Mobile.App.MobileAppService.Models.MasterDetailContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
			ContextKey = "FamiDesk.Mobile.App.MobileAppService.Models.MasterDetailContext";
		}

		protected override void Seed(FamiDesk.Mobile.App.MobileAppService.Models.MasterDetailContext context)
		{
			Seeder.Seed(context);
		}		
	}
}

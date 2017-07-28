namespace FamiDesk.Mobile.App.MobileAppService.Migrations
{
	using FamiDesk.Mobile.App.MobileAppService.DataObjects;
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


			var kine = new User
			{
				Id = Guid.NewGuid().ToString()
			};

			var doctor = new User
			{
				Id = Guid.NewGuid().ToString()
			};

			var ginette = new Person
			{
				Id = Guid.NewGuid().ToString(),
				FirstName = "Ginette",
				LastName = "Nova"
			};

			var albert = new Person
			{
				Id = Guid.NewGuid().ToString(),
				FirstName = "Albert",
				LastName = "Tremblais"
			};

			context.Users.AddOrUpdate(
				p => p.Id,
				kine,
				doctor
			);

			context.Persons.AddOrUpdate(
				p => p.Id,
				ginette,
				albert
			);

			context.EventInfos.AddOrUpdate(
				p => p.Id,
				new EventInfo
				{
					Id = Guid.NewGuid().ToString(),
					Comment = "ras",
					Date = DateTime.UtcNow.AddDays(-1),
					PersonId = ginette.Id,
					Type = EEventType.CheckIn,
					UserId = kine.Id
				}
			);
		}
	}
}

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
				Id = "76321492-16BA-42DF-9470-059885843CEC"
		};

			var doctor = new User
			{
				Id = "C1ED6A6D-F804-43CE-9975-25D093F76422"
			};

			var ginette = new Person
			{
				Id = "FFBC9B72-5166-4C06-A547-7626386FFE9B",
				FirstName = "Ginette",
				LastName = "Nova"
			};

			var albert = new Person
			{
				Id = "EED7114E-CC42-4598-8FA6-AAC67A81C9B3",
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
					Id = "568AEAEB-8E31-40BA-8E8C-5648C1691D87",
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

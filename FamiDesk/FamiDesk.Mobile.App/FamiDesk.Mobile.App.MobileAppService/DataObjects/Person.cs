using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamiDesk.Mobile.App.MobileAppService.DataObjects
{
	public class Person : EntityData
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string BeaconId { get; set; }
		public string Avatar { get; set; }
		public string Address { get; set; }

	}
}
using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamiDesk.Mobile.App.MobileAppService.DataObjects
{
	public class User : EntityData
	{
		public string Login { get; set; }
		public string Profession { get; set; }
		public string Avatar { get; set; }

	}
}
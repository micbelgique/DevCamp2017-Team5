using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamiDesk.Mobile.App.MobileAppService.DataObjects
{
	public class Event : EntityData
	{
		public EEventType Type { get; set; }
		public DateTime Date { get; set; }
		public DateTime Comment { get; set; }

	}
}
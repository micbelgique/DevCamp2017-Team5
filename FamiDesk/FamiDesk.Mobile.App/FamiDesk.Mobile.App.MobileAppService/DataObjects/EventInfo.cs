using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FamiDesk.Mobile.App.MobileAppService.DataObjects
{
	public class EventInfo : EntityData
	{
		public EEventType Type { get; set; }
		public DateTime Date { get; set; }
		public string Comment { get; set; }
		public string PersonId { get; set; }
		public string UserId { get; set; }
	}
}
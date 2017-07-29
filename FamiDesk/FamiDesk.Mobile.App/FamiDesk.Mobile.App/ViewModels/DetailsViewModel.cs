using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamiDesk.Mobile.App.Helpers;
using FamiDesk.Mobile.App.Models;
using FamiDesk.Mobile.App.Views;
using Xamarin.Forms;

namespace FamiDesk.Mobile.App.ViewModels
{
	public class DetailsViewModel : BaseViewModel
	{		
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string Avatar { get; set; }
		public string Comment { get; set; }

		public DetailsViewModel()
		{
			Title = "Details";
		}

		public void Load(Person person)
		{
			FirstName = person.FirstName;
			LastName = person.LastName;
			Address = person.Address;
			Avatar = person.Avatar;
		}
	}
}

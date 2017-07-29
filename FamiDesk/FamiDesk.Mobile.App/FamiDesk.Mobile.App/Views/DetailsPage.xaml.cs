using FamiDesk.Mobile.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FamiDesk.Mobile.App.ViewModels;

namespace FamiDesk.Mobile.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
		private readonly DetailsViewModel viewModel;
		private readonly string personId;
		public DetailsPage(Person person)
        {			
			InitializeComponent();
			viewModel = new DetailsViewModel();
			viewModel.Load(person);
			BindingContext = viewModel;
			personId = person.Id;
		}

		private async void CheckIn_Clicked(object sender, EventArgs e)
		{
			await viewModel.EventDataStore.AddItemAsync(new EventInfo {
				Id = Guid.NewGuid().ToString(),
				Comment = viewModel.Comment,
				Date = DateTime.UtcNow,
				PersonId = personId,
				UserId = "C1ED6A6D-F804-43CE-9975-25D093F76422",
				Type = "CheckIn"
			});
			//viewModel.EventDataStore.SyncAsync
		}

		private async void CheckOut_Clicked(object sender, EventArgs e)
		{
			await viewModel.EventDataStore.AddItemAsync(new EventInfo
			{
				Id = Guid.NewGuid().ToString(),
				Comment = viewModel.Comment,
				Date = DateTime.UtcNow,
				PersonId = personId,
				UserId = "C1ED6A6D-F804-43CE-9975-25D093F76422",
				Type = "CheckOut"
			});
			//viewModel.EventDataStore.SyncAsync
		}
	}
}
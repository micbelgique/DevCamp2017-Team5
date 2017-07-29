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
		public DetailsPage(Person person)
        {			
			InitializeComponent();
			viewModel = new DetailsViewModel();
			viewModel.Load(person);
			BindingContext = viewModel;
		}
	}
}
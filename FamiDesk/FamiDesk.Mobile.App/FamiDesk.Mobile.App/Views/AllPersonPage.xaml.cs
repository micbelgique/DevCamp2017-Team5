using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamiDesk.Mobile.App.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FamiDesk.Mobile.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AllPersonPage : ContentPage
    {
        private readonly AllPersonViewModel viewModel;
        public AllPersonPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new AllPersonViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!viewModel.Persons.Any())
                viewModel.LoadPersonsCommand.Execute(null);
        }
    }
}
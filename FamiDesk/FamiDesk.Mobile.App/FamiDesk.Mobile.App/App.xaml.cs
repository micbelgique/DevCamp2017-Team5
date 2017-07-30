using System.Collections.Generic;
using FamiDesk.Mobile.App.Helpers;
using FamiDesk.Mobile.App.Services;
using FamiDesk.Mobile.App.ViewModels;
using FamiDesk.Mobile.App.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace FamiDesk.Mobile.App
{
    public partial class App : Application
    {
        //MUST use HTTPS, neglecting to do so will result in runtime errors on iOS
        public static bool OfflineMode => false;

        public static string CurrentUserId => "C1ED6A6D-F804-43CE-9975-25D093F76422";

        public static string AzureMobileAppUrl = "https://FamiDeskMobileApp.azurewebsites.net";
        public static IDictionary<string, string> LoginParameters => null;

        public App()
        {
            InitializeComponent();

            if (OfflineMode)
            {
                DependencyService.Register<PersonMockDataStore>();
                DependencyService.Register<EventInfoMockDataStore>();
                DependencyService.Register<BluetoothLEService>();
            }
            else
            {
                DependencyService.Register<PersonAzureDataStore>();
                DependencyService.Register<EventInfoAzureDataStore>();
                DependencyService.Register<BluetoothLEService>();
            }

            DependencyService.Register<AllPersonViewModel>();

            SetMainPage();
        }

        public static void SetMainPage()
        {
            //if (OfflineMode && !Settings.IsLoggedIn)
            //{
            //Current.MainPage = new NavigationPage(new BeaconsPage());
            Current.MainPage = new NavigationPage(new AllPersonPage());
            //Current.MainPage = new NavigationPage(new LoginPage())
            //{
            //    BarBackgroundColor = (Color) Current.Resources["Primary"],
            //    BarTextColor = Color.White
            //};
            //}
            //else
            //{
            //    GoToMainPage();
            //}
        }

        public static void GoToMainPage()
        {
            Current.MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new ItemsPage())
                    {
                        Title = "Browse",
                        Icon = Device.OnPlatform("tab_feed.png", null, null)
                    },
                    new NavigationPage(new AboutPage())
                    {
                        Title = "About",
                        Icon = Device.OnPlatform("tab_about.png", null, null)
                    },
                }
            };
        }
    }
}
using System.Collections.Generic;
using FamiDesk.Mobile.App.Helpers;
using FamiDesk.Mobile.App.Services;
using FamiDesk.Mobile.App.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace FamiDesk.Mobile.App
{
    public partial class App : Application
    {
        //MUST use HTTPS, neglecting to do so will result in runtime errors on iOS
        public static bool OfflineMode => true;

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
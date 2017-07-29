using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamiDesk.Mobile.App.Helpers;
using FamiDesk.Mobile.App.Messages;
using FamiDesk.Mobile.App.Models;
using FamiDesk.Mobile.App.Views;
using Xamarin.Forms;

namespace FamiDesk.Mobile.App.ViewModels
{
    public class AllPersonViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Person> Persons { get; set; }
        public Person PersonSelected { get; set; }
        public Command LoadPersonsCommand { get; }
        public Command OpenDetailCommand { get; }
        public Command DisplayingCommand { get; }
        private INavigation Navigation { get; }
        private Person openPersonDetailDelayed = null;

        public AllPersonViewModel() : this(null)
        { }
        public AllPersonViewModel(INavigation navigation)
        {
            Title = "Persons";
            Persons = new ObservableRangeCollection<Person>();
            LoadPersonsCommand = new Command(async () => await ExecuteLoadPersonsCommand());
            OpenDetailCommand = new Command(async () => await ExecuteOpenDetailCommand());
            DisplayingCommand = new Command(async () => await ExecuteDisplayingCommand());
            Navigation = navigation;

            MessagingCenter.Subscribe<App, NotificationClickedMessage>(this, "NotificationClicked", async (sender, notif) =>
             {
                 openPersonDetailDelayed = PersonSelected = Persons.SingleOrDefault(p => p.Id == notif.Id);
                 await ExecuteOpenDetailCommand();
             });

        }

        private async Task ExecuteDisplayingCommand()
        {
            if (openPersonDetailDelayed != null)
            {
                await ExecuteOpenDetailCommand();
                openPersonDetailDelayed = null;
            }
        }

        private async Task ExecuteOpenDetailCommand()
        {
            if (IsBusy)
            {
                openPersonDetailDelayed = PersonSelected;
                return;
            }
            IsBusy = true;
            try
            {
                // Navigate to our edit page
                await Navigation.PushAsync(new DetailsPage(PersonSelected));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ExecuteLoadPersonsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Persons.Clear();
                var persons = await PersonDataStore.GetItemsAsync(true);
                Persons.ReplaceRange(persons);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

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
        public Command LoadPersonsCommand { get; set; }

        public AllPersonViewModel()
        {
            Title = "Persons";
            Persons = new ObservableRangeCollection<Person>();
            LoadPersonsCommand = new Command(async () => await ExecuteLoadPersonsCommand());

            MessagingCenter.Subscribe<App>(this, "NotificationClicked", personId =>
            {
                //TODO: load person detail or sugjest person    
            });
            
        }

        private async Task ExecuteLoadPersonsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Persons.Clear();
                var persons =  await PersonDataStore.GetItemsAsync(true);
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

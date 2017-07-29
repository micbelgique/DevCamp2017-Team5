using FamiDesk.Mobile.App.Helpers;
using FamiDesk.Mobile.App.Models;
using FamiDesk.Mobile.App.Services;

using Xamarin.Forms;

namespace FamiDesk.Mobile.App.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        /// <summary>
        /// Get the azure service instance
        /// </summary>
        public IDataStore<Person> PersonDataStore => DependencyService.Get<IDataStore<Person>>();
        public IDataStore<EventInfo> EventDataStore => DependencyService.Get<IDataStore<EventInfo>>();

        bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }
        /// <summary>
        /// Private backing field to hold the title
        /// </summary>
        string title = string.Empty;
        /// <summary>
        /// Public property to set and get the title of the item
        /// </summary>
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }
    }
}


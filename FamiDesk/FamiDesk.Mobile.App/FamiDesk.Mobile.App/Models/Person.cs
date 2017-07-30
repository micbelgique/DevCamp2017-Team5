using System.Collections.Generic;
using System.Linq;
using FamiDesk.Mobile.App.Services;
using Xamarin.Forms;

namespace FamiDesk.Mobile.App.Models
{
    public class Person : BaseDataObject
    {
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _beaconId = string.Empty;
        private string _avatar = string.Empty;
        private string _address = string.Empty;
        public List<string> UserIn { get; } = new List<string>();
        private string _familyInformations = string.Empty;

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public string BeaconId
        {
            get => _beaconId;
            set => SetProperty(ref _beaconId, value);
        }

        public string Avatar
        {
            get => _avatar;
            set => SetProperty(ref _avatar, value);
        }

		public string Address
		{
			get => _address;
			set => SetProperty(ref _address, value);
		}

		public string FamilyInformations
		{
			get => _familyInformations;
			set => SetProperty(ref _familyInformations, value);
		}

        public bool ImHere => UserIn?.Any(u => u == App.CurrentUserId) == true;

    }
}
namespace FamiDesk.Mobile.App.Models
{
    public class Person : BaseDataObject
    {
        private string _firstName;
        private string _lastName;
        private string _beaconId;

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
    }
}
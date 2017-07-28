using System;

namespace FamiDesk.Mobile.App.Models
{
    public class Event : BaseDataObject
    {
        private string _type;
        private DateTime _date;
        private string _comment;
        private Person _person;

//        private User _user;
        public string Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public string Comment
        {
            get => _comment;
            set => SetProperty(ref _comment, value);
        }

        public Person Person1
        {
            get => _person;
            set => SetProperty(ref _person, value);
        }
    }
}
using System;

namespace FamiDesk.Mobile.App.Models
{
    public class EventInfo : BaseDataObject
    {
        private string _type;
        private DateTime _date;
        private string _comment;
        private string _personId;
        private string _userId;

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

        public string PersonId
		{
            get => _personId;
            set => SetProperty(ref _personId, value);
        }

		public string UserId
		{
			get => _userId;
			set => SetProperty(ref _userId, value);
		}
	}
}
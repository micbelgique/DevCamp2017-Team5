namespace FamiDesk.Mobile.App.Models
{
    public class User : BaseDataObject
    {
        private string _login = string.Empty;
        private string _profession = string.Empty;
        private string _avatar = string.Empty;
        
		public string FirstName
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        public string Profession
		{
            get => _profession;
            set => SetProperty(ref _profession, value);
        }
        
		public string Avatar
		{
			get => _avatar;
			set => SetProperty(ref _avatar, value);
		}
	}
}
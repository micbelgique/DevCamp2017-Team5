namespace FamiDesk.Mobile.App.Messages
{
    public class NotificationMessage
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public NotificationMessage(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }
}
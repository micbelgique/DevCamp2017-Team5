using System.Collections.Generic;

namespace FamiDesk.Mobile.App.Messages
{
    public class NotificationMessage
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public KeyValuePair<string,string> Extra { get; set; }

        public NotificationMessage(string title, string content, KeyValuePair<string, string> extra)
        {
            Title = title;
            Content = content;
            Extra = extra;
        }
    }
}
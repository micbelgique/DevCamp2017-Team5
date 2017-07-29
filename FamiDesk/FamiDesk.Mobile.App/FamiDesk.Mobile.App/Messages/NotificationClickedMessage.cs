using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamiDesk.Mobile.App.Messages
{
    public class NotificationClickedMessage
    {
        private string _id;

        public string Id
        {
            get => _id;
            set => _id = value;
        }

        public NotificationClickedMessage(string id)
        {
            _id = id;
        }
    }
}

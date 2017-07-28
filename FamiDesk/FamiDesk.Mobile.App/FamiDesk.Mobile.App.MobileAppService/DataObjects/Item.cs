using Microsoft.Azure.Mobile.Server;

namespace FamiDesk.Mobile.App.MobileAppService.DataObjects
{
    public class Item : EntityData
    {
        public string Text { get; set; }
        public string Description { get; set; }
    }
}
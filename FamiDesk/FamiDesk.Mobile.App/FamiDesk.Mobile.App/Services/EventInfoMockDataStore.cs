using System.Threading.Tasks;
using FamiDesk.Mobile.App.Models;

namespace FamiDesk.Mobile.App.Services
{
    public class EventInfoMockDataStore : MockDataStore<EventInfo>
    {
        public override async Task InitializeAsync()
        {
            isInitialized = true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FamiDesk.Mobile.App.Models;
using Xamarin.Forms;

namespace FamiDesk.Mobile.App.Services
{
    public class PersonAzureDataStore : AzureDataStore<Person>
    {
        private IDataStore<EventInfo> EventDataStore => DependencyService.Get<IDataStore<EventInfo>>();

        public override async Task<IEnumerable<Person>> GetItemsAsync(bool forceRefresh = false)
        {
            var persons = (await base.GetItemsAsync(forceRefresh)).ToList();
            IEnumerable<EventInfo> eventInfos = await EventDataStore.GetItemsAsync(true);
            foreach (Person person in persons)
            {
                var personEvents = eventInfos.Where(e => e.PersonId == person.Id).ToList();
                var userIn = personEvents
                    .GroupBy(e => e.UserId)
                    .Select(e => e.OrderByDescending(ev => ev.Date)
                        .First())
                    .Where(e => e.Type.ToUpper() == "CHECKIN")
                    .Select(p => p.UserId).ToList();

                person.UserIn.AddRange(userIn);
            }

            return persons;
        }
    }
}

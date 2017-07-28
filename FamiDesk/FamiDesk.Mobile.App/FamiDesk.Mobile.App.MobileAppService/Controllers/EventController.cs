using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;

using FamiDesk.Mobile.App.MobileAppService.DataObjects;
using FamiDesk.Mobile.App.MobileAppService.Models;
using System;

namespace FamiDesk.Mobile.App.MobileAppService.Controllers
{
    // TODO: Uncomment [Authorize] attribute to require user be authenticated to access Item(s).
    // [Authorize]
    public class EventController : TableController<Event>
    {
		MasterDetailContext context;
		protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            context = new MasterDetailContext();
            DomainManager = new EntityDomainManager<Event>(context, Request);
        }

        // GET tables/Item
        public IQueryable<Event> GetAllItems()
        {
            return Query();
        }

        // GET tables/Item/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Event> GetItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Item/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Event> PatchItem(string id, Delta<Event> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/Item
        public async Task<IHttpActionResult> PostItem(Event item)
        {
			Event current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Item/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteItem(string id)
        {
            return DeleteAsync(id);
        }

		public async Task CheckIn(string personId, string comment)
		{
			var person = await context.Persons.FindAsync(personId);
			if (person == null)
				NotFound();

			var item = new Event {
				Id = Guid.NewGuid().ToString(),
				Date = DateTime.UtcNow,
				Type = EEventType.CheckIn,
				Comment = comment,
				Person = person
			};
		}

		public async Task CheckOut(string personId, string comment)
		{			
			var person = await context.Persons.FindAsync(personId);
			if (person == null)
				NotFound();

			var item = new Event
			{
				Id = Guid.NewGuid().ToString(),
				Date = DateTime.UtcNow,
				Type = EEventType.CheckIn,
				Comment = comment,
				Person = person
			};
		}
	}
}
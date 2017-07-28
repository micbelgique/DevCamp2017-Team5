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
    // TODO: Uncomment [Authorize] attribute to require user be authenticated to access Event(s).
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

        // GET tables/Event
        public IQueryable<Event> GetAllEvents()
        {
            return Query();
        }

        // GET tables/Event/48D68C86-6EA6-4C25-AA33-223FC9A27959
//        public SingleResult<Event> GetEvent(string id)
//        {
//            return Lookup(id);
//        }

        // PATCH tables/Event/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Event> PatchEvent(string id, Delta<Event> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/Event
        public async Task<IHttpActionResult> PostEvent(Event item)
        {
            Event current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new {id = current.Id}, current);
        }

        // DELETE tables/Event/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteEvent(string id)
        {
            return DeleteAsync(id);
        }
    }
}
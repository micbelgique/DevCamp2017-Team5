using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;

using FamiDesk.Mobile.App.MobileAppService.DataObjects;
using FamiDesk.Mobile.App.MobileAppService.Models;

namespace FamiDesk.Mobile.App.MobileAppService.Controllers
{
    // TODO: Uncomment [Authorize] attribute to require user be authenticated to access Person(s).
    // [Authorize]
    public class PersonController : TableController<Person>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MasterDetailContext context = new MasterDetailContext();
            DomainManager = new EntityDomainManager<Person>(context, Request);
		}

        // GET tables/Person
        public IQueryable<Person> GetAllPersons()
        {
            return Query();
        }

        // GET tables/Person/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<Person> GetPerson(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/Person/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Person> PatchPerson(string id, Delta<Person> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/Person
        public async Task<IHttpActionResult> PostPerson(Person person)
        {
			Person current = await InsertAsync(person);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/Person/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeletePerson(string id)
        {
            return DeleteAsync(id);
        }
    }
}
using System.Linq;
using System.Web.Http;
using Breeze.WebApi;
using Newtonsoft.Json.Linq;
using WorkOrders.Web.Models;

namespace WorkOrders.Web.Controllers
{
    [BreezeController]
    public class UsersController : ApiController
    {
        readonly EFContextProvider<UsersContext> _contextProvider =
            new EFContextProvider<UsersContext>();

        [HttpGet]
        public string Metadata()
        {
            return _contextProvider.Metadata();
        }

        [HttpPost]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            return _contextProvider.SaveChanges(saveBundle);
        }

        [HttpGet]
        public IQueryable<UserProfile> Users()
        {
            return _contextProvider.Context.UserProfiles;
        }



    }
}

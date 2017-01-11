using System.Web.Http;

namespace AspNetWebApi.Controllers
{
    /*
     * This controller shares the route prefix with the SameRoutePrefix1Controller.
     * This will work but Swashbuckle splits up the documentation by controllers so
     * it will look ugly. Swashbuckle can be configured to treat them as the same
     * controller though, see Startup.cs.
     */
    [RoutePrefix("sameRoutePrefix")]
    public class SameRoutePrefix1Controller : ApiController
    {
        [HttpGet, Route("")]
        public IHttpActionResult Get()
        {
            return Ok("Works 1!");
        }
    }
}

using System.Web.Http;

namespace AspNetWebApi.Controllers
{
    /*
     * This controller shares the route prefix with the SameRoutePrefix2Controller.
     * This will work but Swashbuckle splits up the documentation by controllers so
     * it will look ugly. Swashbuckle might be configured to treat them as the same
     * controller, will need to investigate. TODO!
     */
    [RoutePrefix("sameRoutePrefix")]
    public class SameRoutePrefix2Controller : ApiController
    {
        [HttpGet, Route("anotherRoute")]
        public IHttpActionResult Get()
        {
            return Ok("Works 2!");
        }
    }
}

using System.Web.Http;

namespace AspNetWebApi.Controllers.PrefixTest
{
    /*
     * This controller doesn't have a route prefix, but it is configurable in Web.config
     */
    public class Prefix2Controller : PrefixBaseApiController
    {
        [HttpGet, Route("prefix2")]
        public IHttpActionResult Get()
        {
            return Ok("Configurable base route prefix test!");
        }
    }
}

using System.Web.Http;

namespace AspNetWebApi.Controllers.PrefixTest
{
    /*
     * This controller doesn't have a route prefix, but it is configurable in Web.config
     */
    public class Prefix1Controller : ApiController
    {
        [HttpGet, Route("prefix1")]
        public IHttpActionResult Get()
        {
            return Ok("Configurable route prefix test!");
        }
    }
}

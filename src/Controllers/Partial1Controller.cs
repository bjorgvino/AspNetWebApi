using System.Web.Http;

namespace AspNetWebApi.Controllers
{
    /* 
     * This partial class can serve as a base class that can be extended by
     * other classes, but only within the same assembly.
     */
    [RoutePrefix("partial")]
    public partial class PartialController : ApiController
    {
        [HttpGet, Route("")]
        public IHttpActionResult Get1()
        {
            return Ok("Partial 1!");
        }
    }
}

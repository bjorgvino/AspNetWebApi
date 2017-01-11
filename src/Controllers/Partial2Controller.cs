using System.Web.Http;

namespace AspNetWebApi.Controllers
{
    /*
     * This class extends the partial from Partial1Controller and the routes added here will
     * be grouped with the routes from that controller.
     * 
     * Swashbuckle will group these routes like they're coming from the same controller (obviously
     * since that's the way partials work in C#).
     * 
     * But this will only work within the same assembly.
     */
    public partial class PartialController
    {
        [HttpGet, Route("anotherRoute")]
        public IHttpActionResult Get2()
        {
            return Ok("Partial 2!");
        }
    }
}

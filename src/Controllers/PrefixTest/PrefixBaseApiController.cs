using System.Web.Http;

namespace AspNetWebApi.Controllers.PrefixTest
{
    /*
     * Base controller that will be configured with a specific route prefix
     * so all controllers that derive from this base controller will have the
     * same route prefix (unless of course the derived controller is configured
     * with a specific route prefix).
     */
    public class PrefixBaseApiController : ApiController
    {
    }
}

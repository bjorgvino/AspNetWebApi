using System.Web.Http;

namespace AspNetWebApi.Controllers.NamespaceTest.NamespaceTwo
{
    /*
     * This class will conflict with AspNetWebApi.Controllers.NamespaceTest.NamespaceOne.TestClass and 
     * both controllers will be disabled, even though the controller namespaces and route prefixes are different.
     *
     * LESSON: Controller class names must be globally unique!
     */
    [RoutePrefix("anotherTest")]
    public class TestController : ApiController
    {
        [HttpGet, Route("")]
        public IHttpActionResult Get()
        {
            return Ok("This controller will not work because the class name isn't unique accross the api!");
        }
    }
}

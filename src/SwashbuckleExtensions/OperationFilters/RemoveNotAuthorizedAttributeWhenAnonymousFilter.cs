using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace AspNetWebApi.SwashbuckleExtensions.OperationFilters
{
    /// <summary>
    /// Operation filter that removes 401 response documentation for methods that allow anonymous access
    /// </summary>
    public class RemoveNotAuthorizedAttributeWhenAnonymousFilter : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiDescription"></param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var allowAnonymous = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any() ||
                                 apiDescription.ActionDescriptor.ControllerDescriptor
                                     .GetCustomAttributes<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
            {
                var responseKey = ((int) HttpStatusCode.Unauthorized).ToString();
                if (operation.responses.ContainsKey(responseKey))
                {
                    operation.responses.Remove(responseKey);
                }
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Description;
using AspNetWebApi.SwashbuckleExtensions.Annotations;
using Swashbuckle.Swagger;

namespace AspNetWebApi.SwashbuckleExtensions.OperationFilters
{
    /// <summary>
    /// Operation Filter that reads header information from <see cref="SwaggerResponseHeaderAttribute"/> 
    /// and adds it to the operations' documentation.
    /// </summary>
    public class ApplySwaggerResponseHeaderAttributes : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiDescription"></param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            var responseAttributes = apiDescription.GetControllerAndActionAttributes<SwaggerResponseHeaderAttribute>()
                .OrderBy(attr => attr.StatusCode);

            foreach (var attr in responseAttributes)
            {
                var statusCode = attr.StatusCode.ToString();
                if (!operation.responses.ContainsKey(statusCode))
                {
                    operation.responses[statusCode] = new Response();
                }
                if (operation.responses[statusCode].headers == null)
                {
                    operation.responses[statusCode].headers = new Dictionary<string, Header>();
                }
                if (!operation.responses[statusCode].headers.ContainsKey(attr.Header))
                {
                    operation.responses[statusCode].headers[attr.Header] = new Header();
                }
                operation.responses[statusCode].headers[attr.Header].description = attr.Description;
                operation.responses[statusCode].headers[attr.Header].type = attr.Type;
            }
        }
    }
}

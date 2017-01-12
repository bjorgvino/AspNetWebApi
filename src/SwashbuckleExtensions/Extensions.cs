using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using AspNetWebApi.WebApiExtensions.Providers;
using Swashbuckle.Swagger;

namespace AspNetWebApi.SwashbuckleExtensions
{
    public class Extensions
    {
        public static string GetRoutePrefix(ApiDescription apiDesc)
        {
            var routePrefixAttribute = apiDesc.GetControllerAndActionAttributes<RoutePrefixAttribute>().FirstOrDefault();
            if (!string.IsNullOrEmpty(routePrefixAttribute?.Prefix))
            {
                return char.ToUpper(routePrefixAttribute.Prefix[0]) + routePrefixAttribute.Prefix.Substring(1);
            }
            return apiDesc.ActionDescriptor.ControllerDescriptor.ControllerName;
        }

        public static string GetConfiguredRoutePrefix(ApiDescription apiDesc, ControllerPrefixProvider controllerPrefixProvider)
        {
            var configuredPrefix = controllerPrefixProvider.GetConfiguredRoutePrefix(apiDesc.ActionDescriptor.ControllerDescriptor);
            if (!string.IsNullOrEmpty(configuredPrefix))
            {
                return char.ToUpper(configuredPrefix[0]) + configuredPrefix.Substring(1);
            }
            return apiDesc.ActionDescriptor.ControllerDescriptor.ControllerName;
        }
    }
}

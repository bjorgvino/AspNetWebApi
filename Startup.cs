using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using AspNetWebApi;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;
using Swashbuckle.Swagger;

[assembly: OwinStartup(typeof(Startup))]
namespace AspNetWebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            var jsonFormatter = config.Formatters.JsonFormatter;

            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());
            config.Formatters.Clear();
            config.Formatters.Add(jsonFormatter);
            config.Formatters.JsonFormatter.MediaTypeMappings.Add(new QueryStringMapping("type", "json", new MediaTypeHeaderValue("application/json")));

            config.MapHttpAttributeRoutes();

            config
                .EnableSwagger("docs/{apiVersion}", c =>
                {
                    c.SingleApiVersion("v1", "WebApi");
                    /* 
                     * This is how we would make Swashbuckle group actions by the route prefixes of controllers...
                     * TODO: Check how this will work with runtime configurable prefixes...
                     */
                    c.GroupActionsBy(apiDesc =>
                    {
                        var routePrefixAttribute = apiDesc.GetControllerAndActionAttributes<RoutePrefixAttribute>().FirstOrDefault();
                        if (!string.IsNullOrEmpty(routePrefixAttribute?.Prefix))
                        {
                            return char.ToUpper(routePrefixAttribute.Prefix[0]) + routePrefixAttribute.Prefix.Substring(1);
                        }
                        return apiDesc.ActionDescriptor.ControllerDescriptor.ControllerName;
                    });
                })
                .EnableSwaggerUi("docs/ui/{*assetPath}", c =>
                {
                    // Removes the Swagger Docs validator (the validator works only for public APIs so therefore we disable it)
                    c.DisableValidator();

                    // Enables the dropdown to select different API versions
                    c.EnableDiscoveryUrlSelector();

                    // Swagger UI expanded by default
                    //c.DocExpansion(DocExpansion.Full);
                });

            app.UseFileServer(new FileServerOptions
            {
                RequestPath = new PathString(string.Empty),
                FileSystem = new PhysicalFileSystem("./Public"),
                EnableDirectoryBrowsing = true,
            });

            app.UseStageMarker(PipelineStage.MapHandler);

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}

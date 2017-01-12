using System.Configuration;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using AspNetWebApi;
using AspNetWebApi.SwashbuckleExtensions;
using AspNetWebApi.WebApiExtensions.Configuration;
using AspNetWebApi.WebApiExtensions.Providers;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Extensions;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;

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

            // Setup API routes using a custom controller prefix provider that enables route prefixes to be configurable by controller types
            var controllerPrefixConfiguration = ConfigurationManager.GetSection("controllerPrefixConfiguration") as ControllerPrefixConfiguration;
            var controllerPrefixProvider = new ControllerPrefixProvider(controllerPrefixConfiguration);
            config.MapHttpAttributeRoutes(controllerPrefixProvider);

            config
                .EnableSwagger("docs/{apiVersion}", c =>
                {
                    // Single API version
                    c.SingleApiVersion("v1", "WebApi");

                    // Multiple API versions
                    // TODO!
                    
                    // Group actions by their route prefixes
                    // c.GroupActionsBy(Extensions.GetRoutePrefix);
                    
                    // Group actions by their configurable route prefixes
                    c.GroupActionsBy(apiDesc => Extensions.GetConfiguredRoutePrefix(apiDesc, controllerPrefixProvider));
                })
                .EnableSwaggerUi("docs/ui/{*assetPath}", c =>
                {
                    // Removes the Swagger Docs validator (for example if your API is not publicly accessible)
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

            //app.UseStageMarker(PipelineStage.MapHandler);
            //app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}

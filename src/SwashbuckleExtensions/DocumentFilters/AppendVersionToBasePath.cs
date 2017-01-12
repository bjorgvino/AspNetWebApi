using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http.Description;
using Swashbuckle.Swagger;

namespace AspNetWebApi.SwashbuckleExtensions.DocumentFilters
{
    /// <summary>
    /// Appends the version to the Swagger Doc basePath taking into account controller prefixes as well
    /// </summary>
    public class AppendVersionToBasePath : IDocumentFilter
    {
        private const string VersionRegex = @"v([\d]+)";

        /// <summary>
        /// Applies the document filter.
        /// </summary>
        /// <param name="swaggerDoc">SwaggerDocument</param>
        /// <param name="schemaRegistry">SchemaRegistry</param>
        /// <param name="apiExplorer">ApiExplorer</param>
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            swaggerDoc.basePath = "/" + swaggerDoc.info.version.Replace("-", "/");

            swaggerDoc.paths = swaggerDoc.paths.ToDictionary(
                entry => entry.Key.Replace(swaggerDoc.basePath, ""),
                entry => RemoveVersionParamsFrom(entry.Value));
        }

        private static PathItem RemoveVersionParamsFrom(PathItem pathItem)
        {
            RemoveVersionParamFrom(pathItem.get);
            RemoveVersionParamFrom(pathItem.put);
            RemoveVersionParamFrom(pathItem.post);
            RemoveVersionParamFrom(pathItem.delete);
            RemoveVersionParamFrom(pathItem.options);
            RemoveVersionParamFrom(pathItem.head);
            RemoveVersionParamFrom(pathItem.patch);
            return pathItem;
        }

        private static void RemoveVersionParamFrom(Operation operation)
        {
            if (operation != null)
            {
                var versionParam = operation.parameters?.SingleOrDefault(param => param.name == "apiVersion");
                if (versionParam != null)
                {
                    operation.parameters.Remove(versionParam);
                }

                if (operation.tags != null)
                {
                    for (var i = 0; i < operation.tags.Count; i++)
                    {
                        // This will remove version postfixes from controller names in the Swagger Docs
                        operation.tags[i] = Regex.Replace(operation.tags[i], VersionRegex, string.Empty, RegexOptions.IgnoreCase); ;
                    }
                }
            }
        }
    }
}

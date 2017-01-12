using System.Collections.Concurrent;
using Swashbuckle.Swagger;

namespace AspNetWebApi.SwashbuckleExtensions.SwaggerProviders
{
    /// <summary>
    /// A wrapper that caches a provided Swagger Provider.
    /// </summary>
    public class CachedSwaggerProvider : ISwaggerProvider
    {
        private static readonly ConcurrentDictionary<string, SwaggerDocument> Cache = new ConcurrentDictionary<string, SwaggerDocument>();

        private readonly ISwaggerProvider _swaggerProvider;

        /// <summary>
        /// Cache a provided Swagger Provider
        /// </summary>
        /// <param name="swaggerProvider">Swagger Provider to cache</param>
        public CachedSwaggerProvider(ISwaggerProvider swaggerProvider)
        {
            _swaggerProvider = swaggerProvider;
        }

        /// <summary>
        /// Get SwaggerDocument
        /// </summary>
        /// <param name="rootUrl">Root URL</param>
        /// <param name="apiVersion">API version</param>
        /// <returns>SwaggerDocument</returns>
        public SwaggerDocument GetSwagger(string rootUrl, string apiVersion)
        {
            var cacheKey = $"{rootUrl}_{apiVersion}";
            return Cache.GetOrAdd(cacheKey, key => _swaggerProvider.GetSwagger(rootUrl, apiVersion));
        }
    }
}

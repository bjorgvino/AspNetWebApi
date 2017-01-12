using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using AspNetWebApi.WebApiExtensions.Configuration;

namespace AspNetWebApi.WebApiExtensions.Providers
{
    public class ControllerPrefixProvider : DefaultDirectRouteProvider
    {
        private readonly ControllerPrefixConfiguration _config;

        public ControllerPrefixProvider(ControllerPrefixConfiguration config)
        {
            _config = config;
        }

        protected override string GetRoutePrefix(HttpControllerDescriptor controllerDescriptor)
        {
            return GetConfiguredRoutePrefix(controllerDescriptor);
        }

        public string GetConfiguredRoutePrefix(HttpControllerDescriptor controllerDescriptor)
        {
            var existingPrefix = base.GetRoutePrefix(controllerDescriptor) ?? string.Empty;
            var prefix = _config?.DefaultPrefix ?? string.Empty;
            if (_config?.ControllerPrefixes != null && _config.ControllerPrefixes.Any())
            {
                foreach (var prefixConfig in _config.ControllerPrefixes)
                {
                    try
                    {
                        var prefixControllerType = Type.GetType(prefixConfig.Type);
                        if (prefixControllerType == null)
                        {
                            throw new Exception($"ControllerPrefixConfiguration is invalid. Type '{prefixConfig.Type}' was not found.");
                        }
                        if (prefixControllerType.IsSameOrSubclassOf(controllerDescriptor.ControllerType))
                        {
                            prefix = prefixConfig.Prefix;
                            break;
                        }
                    }
                    catch (Exception)
                    {
                        // ControllerPrefixConfiguration is invalid - this would be a nice place to log an error, but we're just gonna
                        // let the exception kill us.
                        throw;
                    }
                }
            }
            var separator = string.Empty;
            if (prefix != string.Empty && existingPrefix != string.Empty)
            {
                separator = "/";
            }
            Debug.WriteLine($"{prefix}{separator}{existingPrefix}");
            return $"{prefix}{separator}{existingPrefix}";
        }
    }
}

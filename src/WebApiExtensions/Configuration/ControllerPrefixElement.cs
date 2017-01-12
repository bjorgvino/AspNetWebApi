using System.Configuration;

namespace AspNetWebApi.WebApiExtensions.Configuration
{
    public class ControllerPrefixElement : ConfigurationElement
    {
        [ConfigurationProperty("prefix", IsRequired = false, DefaultValue = "")]
        public string Prefix
        {
            get { return (string)this["prefix"]; }
            set { this["prefix"] = value; }
        }

        [ConfigurationProperty("type", IsRequired = true)]
        public string Type
        {
            get { return (string)this["type"]; }
            set { this["type"] = value; }
        }
    }
}

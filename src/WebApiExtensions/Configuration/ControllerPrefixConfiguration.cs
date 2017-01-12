using System.Configuration;

namespace AspNetWebApi.WebApiExtensions.Configuration
{
    public class ControllerPrefixConfiguration : ConfigurationSection
    {
        [ConfigurationProperty("defaultPrefix", IsRequired = false, DefaultValue = "")]
        public string DefaultPrefix
        {
            get { return (string)this["defaultPrefix"]; }
            set { this["defaultPrefix"] = value; }
        }

        [ConfigurationProperty("controllerPrefixes", IsRequired = false, IsDefaultCollection = true)]
        public ControllerPrefixCollection ControllerPrefixes => (ControllerPrefixCollection)base["controllerPrefixes"];
    }
}

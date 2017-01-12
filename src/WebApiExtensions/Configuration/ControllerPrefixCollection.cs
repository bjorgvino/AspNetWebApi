using System.Collections.Generic;
using System.Configuration;

namespace AspNetWebApi.WebApiExtensions.Configuration
{
    public class ControllerPrefixCollection : ConfigurationElementCollection, IEnumerable<ControllerPrefixElement>
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new ControllerPrefixElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ControllerPrefixElement)element).Type;
        }

        public override ConfigurationElementCollectionType CollectionType => ConfigurationElementCollectionType.BasicMap;

        protected override string ElementName => "controllerPrefix";

        public new IEnumerator<ControllerPrefixElement> GetEnumerator()
        {
            var count = Count;
            for (var i = 0; i < count; i++)
            {
                yield return BaseGet(i) as ControllerPrefixElement;
            }
        }
    }
}

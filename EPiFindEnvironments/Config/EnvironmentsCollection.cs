using System.Configuration;

namespace EPiFindEnvironments
{
    public class EnvironmentsCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new EPiFindEnvironmentConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EPiFindEnvironmentConfigurationElement) element).Name;
        }
    }
}
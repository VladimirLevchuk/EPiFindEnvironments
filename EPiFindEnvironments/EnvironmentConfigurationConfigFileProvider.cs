using System.Configuration;

namespace EPiFindEnvironments
{
    public class EnvironmentConfigurationConfigFileProvider : IEPiFindConfigurationProvider
    {
        private readonly EPiFindEnvironmentsConfigurationSection _config;

        public EnvironmentConfigurationConfigFileProvider()
        {
            this._config = EPiFindEnvironmentsConfigurationSection.ReadFromConfig();
        }

        public EnvironmentConfiguration GetEnvironmentConfiguration(string name)
        {
            if (name == null)
            {
                return GetCurrentEnvironmentConfiguration();
            }

            var environmentConfig = _config.GetEnvironmentByName(name);
            var result = CreateEnvironmentConfiguration(environmentConfig);
            return result;
        }

        public EnvironmentConfiguration GetCurrentEnvironmentConfiguration()
        {
            var currentEnvironment = _config.CurrentEnvironment;

            if (currentEnvironment == null)
            {
                throw new ConfigurationErrorsException("Invalid current episerver.find.environments configuration. ");
            }

            var result = CreateEnvironmentConfiguration(currentEnvironment);

            return result;
        }

        private EnvironmentConfiguration CreateEnvironmentConfiguration(EPiFindEnvironmentConfigurationElement configurationElement)
        {
            var result = new EnvironmentConfiguration
            {
                Name = configurationElement.Name,
                IndexName = configurationElement.IndexName,
                PrivateUrl = configurationElement.PrivateUrl,
                PublicUrl = configurationElement.PublicUrl
            };

            return result;
        }
    }
}
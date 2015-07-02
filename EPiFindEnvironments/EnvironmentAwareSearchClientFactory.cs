using EPiServer.Find;

namespace EPiFindEnvironments
{
    public class EnvironmentAwareSearchClientFactory : ISearchClientFactory
    {
        private readonly IEPiFindConfigurationProvider _configurationProvider;

        public EnvironmentAwareSearchClientFactory(IEPiFindConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        public virtual IClient CreateClient()
        {
            var currentEnvironmentConfig = _configurationProvider.GetEnvironmentConfiguration(null);

            var client = new Client(currentEnvironmentConfig.PrivateUrl, 
                currentEnvironmentConfig.IndexName);

            return client;
        }

        private static readonly ISearchClientFactory DefaultFallback = new EnvironmentAwareSearchClientFactory(new EnvironmentConfigurationConfigFileProvider());
        private static ISearchClientFactory _default;

        public static ISearchClientFactory Default
        {
            get { return _default ?? DefaultFallback; }
            set { _default = value; }
        }
    }
}
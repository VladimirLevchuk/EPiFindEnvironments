using FluentAssertions;
using NUnit.Framework;

namespace EPiFindEnvironments.Tests
{
    public class EnvironmentAwareSearchClientFactory_AcceptanceTests : TestBase
    {
        [Test]
        public void CreateClient_CreatesClientForTheCurrentEnvironment()
        {
            var configProvider = new EnvironmentConfigurationConfigFileProvider();

            EnvironmentAwareSearchClientFactory.Default = new EnvironmentAwareSearchClientFactory(configProvider);

            var client = EnvironmentAwareSearchClientFactory.Default.CreateClient();

            var currentConfig = configProvider.GetCurrentEnvironmentConfiguration();

            client.DefaultIndex.Should().Be(currentConfig.IndexName);
            client.ServiceUrl.Should().Be(currentConfig.PrivateUrl);
        }
    }
}
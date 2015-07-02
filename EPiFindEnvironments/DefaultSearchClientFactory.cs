using EPiServer.Find;

namespace EPiFindEnvironments
{
    public class DefaultSearchClientFactory : ISearchClientFactory
    {
        public virtual IClient CreateClient()
        {
            return Client.CreateFromConfig();
        }
    }
}
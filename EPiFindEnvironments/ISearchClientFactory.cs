using EPiServer.Find;

namespace EPiFindEnvironments
{
    public interface ISearchClientFactory
    {
        IClient CreateClient();
    }
}
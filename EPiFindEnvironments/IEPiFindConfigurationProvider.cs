using JetBrains.Annotations;

namespace EPiFindEnvironments
{
    public interface IEPiFindConfigurationProvider
    {
        EnvironmentConfiguration GetEnvironmentConfiguration([CanBeNull] string name);
    }
}
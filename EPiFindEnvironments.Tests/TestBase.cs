using NUnit.Framework;

namespace EPiFindEnvironments.Tests
{
    public class TestBase
    {
        [SetUp]
        public virtual void Setup()
        {}

        [TearDown]
        public virtual void Cleanup()
        {}
    }
}
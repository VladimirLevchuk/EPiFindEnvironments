using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace EPiFindEnvironments.Tests
{
    public class EPiFindEnvironmentsConfigurationSection_AcceptanceTests
    {
        [Test]
        public void ReadFromConfig_returns_config_section()
        {
            var config = EPiFindEnvironmentsConfigurationSection.ReadFromConfig();

            config.Should().NotBeNull();
        }

        public class with_test_config : TestBase
        {
            public EPiFindEnvironmentsConfigurationSection Config { get; protected set; }

            public override void Setup()
            {
                Config = EPiFindEnvironmentsConfigurationSection.ReadFromConfig();
            }

            public override void Cleanup()
            {
                Config = null;
                base.Cleanup();
            }

            [Test]
            public void returns_seven_environments()
            {
                Config.Environments.Count.Should().Be(7);
            }

            [Test]
            public void and_default_environment_is_a_computer_name_variable()
            {
                Config.Current.Should().Be("$computerName");
            }

            [Test]
            public void and_default_environment_is_a_computer_name()
            {
                if (Environment.MachineName.Equals("PC-LEVA", StringComparison.InvariantCultureIgnoreCase))
                {
                    Config.CurrentEnvironmentName.Should().Be("PC-LEVA");
                }
                else
                {
                    Config.CurrentEnvironmentName.Should().Be("$computerName");
                }
            }

            [Test]
            public void and_computer_name_variable_evaluates_to_computer_name()
            {
                Config.EvaluateIfVariable("$computerName").Should().Be(Environment.MachineName);
            }

            [Test]
            public void and_system_variable_can_also_be_evaluated()
            {
                Config.EvaluateIfVariable("$path").Should().Be(Environment.GetEnvironmentVariable("PATH"));
            }

            [Test]
            public void and_fallback_is_used_when_current_name_is_not_found()
            {
                var old = Config.Current;
                try
                {
                    Config.Current = "we'll never find this name";
                    Config.CurrentEnvironmentName.Should().Be("one");
                }
                finally
                {
                    Config.Current = old;
                }
            }
        }
    }
}

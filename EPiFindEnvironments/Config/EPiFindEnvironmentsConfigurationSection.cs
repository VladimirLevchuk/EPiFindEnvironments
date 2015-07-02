using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using EPiFindEnvironments.Extensions;
using JetBrains.Annotations;

namespace EPiFindEnvironments
{
    public class EPiFindEnvironmentsConfigurationSection : ConfigurationSection
    {
        public static EPiFindEnvironmentsConfigurationSection ReadFromConfig()
        {
            var config = (EPiFindEnvironmentsConfigurationSection)ConfigurationManager.GetSection("episerver.find.environments");
            return config;
        }

        public EPiFindEnvironmentConfigurationElement CurrentEnvironment
        {
            get
            {
                var result = GetEnvironmentByName(CurrentEnvironmentName);
                return result;
            }
        }

        public string CurrentEnvironmentName
        {
            get
            {
                var currentEnvironmentName = NullIfEnvironmentNotFound(Current) ?? NullIfEnvironmentNotFound(Fallback.NullIfEmpty()) ?? "$computerName";

                currentEnvironmentName = EvaluateIfVariable(currentEnvironmentName);

                return currentEnvironmentName;
            }
        }

        private string NullIfEnvironmentNotFound(string environmentName)
        {
            environmentName = EvaluateIfVariable(environmentName);

            if (string.IsNullOrEmpty(environmentName))
            {
                return null;
            }

            if (GetEnvironmentByName(environmentName) == null)
            {
                return null;
            }

            return environmentName;
        }

        public virtual EPiFindEnvironmentConfigurationElement GetEnvironmentByName(string name)
        {
            name = name ?? CurrentEnvironmentName;
            
            var result = TypedEnvironments
                    .FirstOrDefault(
                        x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

            return result;
        }

        internal string EvaluateIfVariable([NotNull] string variableName)
        {
            if (variableName == null) throw new ArgumentNullException("variableName");
            return ConfigVariableEvaluator.Current.Evaluate(variableName);
        }

        private string _testCurrent;

        [ConfigurationProperty("current", IsRequired = true, DefaultValue = "$computerName")]
        public string Current
        {
            get { return _testCurrent ?? (string) this["current"]; }
            set { _testCurrent = value; }
        }

        [ConfigurationProperty("fallback", IsRequired = false)]
        public string Fallback
        {
            get { return (string)this["fallback"]; }
            set { this["fallback"] = value; }
        }

        [ConfigurationProperty("", IsDefaultCollection = true)]
        [ConfigurationCollection(typeof (EnvironmentsCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public EnvironmentsCollection Environments
        {
            get { return (EnvironmentsCollection) base[""]; }
        }

        public virtual IEnumerable<EPiFindEnvironmentConfigurationElement> TypedEnvironments
        {
            get { return Environments.Cast<EPiFindEnvironmentConfigurationElement>(); }
        }
    }
}
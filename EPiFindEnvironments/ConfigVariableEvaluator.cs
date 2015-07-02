using System;
using EPiFindEnvironments.Extensions;

namespace EPiFindEnvironments
{
    public class ConfigVariableEvaluator : IConfigVariableEvaluator
    {
        private static IConfigVariableEvaluator _current;

        public virtual bool IsVariable(string name)
        {
            return name.StartsWith("$");
        }

        public virtual string Evaluate(string name)
        {
            if (!IsVariable(name))
            {
                return name;
            }

            // handle predefined variables
            if (name.Equals("$computerName", StringComparison.InvariantCultureIgnoreCase))
            {
                return Environment.MachineName;
            }

            // remove leading $
            var varName = name.Substring(1);

            var result = Environment.GetEnvironmentVariable(varName).NullIfEmpty();
            return result;
        }

        private static readonly IConfigVariableEvaluator Default = new ConfigVariableEvaluator();

        public static IConfigVariableEvaluator Current
        {
            get { return _current ?? Default; }
            set { _current = value; }
        }
    }
}
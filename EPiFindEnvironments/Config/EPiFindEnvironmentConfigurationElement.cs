using System.Configuration;

namespace EPiFindEnvironments
{
    public class EPiFindEnvironmentConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("publicUrl", IsRequired = false)]
        public string PublicUrl
        {
            get { return (string)this["publicUrl"]; }
            set { this["publicUrl"] = value; }
        }

        [ConfigurationProperty("privateUrl", IsRequired = true)]
        public string PrivateUrl
        {
            get { return (string)this["privateUrl"]; }
            set { this["privateUrl"] = value; }
        }

        [ConfigurationProperty("indexName", IsRequired = true)]
        public string IndexName
        {
            get { return (string)this["indexName"]; }
            set { this["indexName"] = value; }
        }
    }
}
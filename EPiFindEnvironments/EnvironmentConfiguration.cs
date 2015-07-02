using EPiFindEnvironments.Extensions;

namespace EPiFindEnvironments
{
    public class EnvironmentConfiguration
    {
        private string _privateUrl;
        private string _publicUrl;

        public string PrivateUrl
        {
            get { return _privateUrl; }
            set { _privateUrl = value == null ? null : value.EnsureEndsWith("/"); }
        }

        public string PublicUrl
        {
            get { return _publicUrl; }
            set { _publicUrl = value == null ? null : value.EnsureEndsWith("/"); }
        }

        public string IndexName { get; set; }

        public string Name { get; set; }
        
        public virtual string SearchUrl
        {
            get { return string.Format("{0}{1}/_search", PrivateUrl, IndexName); }
        }
    }
}
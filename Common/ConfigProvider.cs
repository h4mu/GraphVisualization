using System.Configuration;

namespace Common
{
    public class ConfigProvider : IConfigProvider
    {
        public string DbConnection
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["neo4j"].ConnectionString;
            }
        }

        public string DbPass
        {
            get
            {
                return ConfigurationManager.AppSettings["dbPass"];
            }
        }

        public string DbUser
        {
            get
            {
                return ConfigurationManager.AppSettings["dbUser"];
            }
        }
    }
}
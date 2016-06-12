using Neo4j.Driver.V1;

namespace Common
{
    public class DbDriverFactory : IDbDriverFactory
    {
        private IConfigProvider configProvider;

        public DbDriverFactory(IConfigProvider configProvider)
        {
            this.configProvider = configProvider;
        }

        public IDriver CreateDriver()
        {
            return GraphDatabase.Driver(configProvider.DbConnection,
                AuthTokens.Basic(configProvider.DbUser, configProvider.DbPass));
        }
    }
}
using System;
using Neo4j.Driver.V1;

namespace Common
{
    public class DbDriverFactory : IDbDriverFactory
    {
        private IConfigProvider configProvider;

        internal DbDriverFactory(IConfigProvider configProvider)
        {
            this.configProvider = configProvider;
        }

        public IDriver CreateDriver()
        {
            return GraphDatabase.Driver(configProvider.DbConnection,
                AuthTokens.Basic(configProvider.DbUser, configProvider.DbPass),
                Config.Builder.WithEncryptionLevel(EncryptionLevel.Encrypted).ToConfig());
        }
    }
}
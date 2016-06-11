using Neo4j.Driver.V1;

namespace Common
{
    public interface IDbDriverFactory
    {
        IDriver CreateDriver();
    }
}
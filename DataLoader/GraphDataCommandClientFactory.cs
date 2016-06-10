using GraphDataService.Command.Contract;

namespace DataLoader
{
    internal class GraphDataCommandClientFactory : IGraphDataCommandClientFactory
    {
        public IGraphDataServiceCommandClient GetGraphDataCommandClient()
        {
            return new GraphDataServiceCommandClient();
        }
    }
}
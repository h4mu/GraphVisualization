using System;
using GraphDataService.ShortestPath.Contract;

namespace GraphDataService.ShortestPath
{
    internal class GraphDataServiceShortestPathClientFactory : IGraphDataServiceShortestPathClientFactory
    {
        public IGraphDataServiceShortestPathClient CreateClient()
        {
            return new GraphDataServiceShortestPathClient();
        }
    }
}
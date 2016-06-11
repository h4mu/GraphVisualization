using System;
using GraphBusinessService.ShortestPath.Contract;

namespace GraphBusinessService.ShortestPath
{
    internal class GraphBusinessServiceShortestPathClientFactory : IGraphBusinessServiceShortestPathClientFactory
    {
        public IGraphBusinessServiceShortestPathClient CreateClient()
        {
            return new GraphDataServiceShortestPathClient();
        }
    }
}
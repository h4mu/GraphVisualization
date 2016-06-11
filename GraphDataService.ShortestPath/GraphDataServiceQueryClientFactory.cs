using System;
using GraphDataService.Query.Contract;

namespace GraphDataService.ShortestPath
{
    internal class GraphDataServiceQueryClientFactory : IGraphDataServiceQueryClientFactory
    {
        public IGraphDataServiceQueryClient CreateClient()
        {
            return new GraphDataServiceQueryClient();
        }
    }
}
using GraphDataService.Query.Contract;

namespace GraphDataService.ShortestPath
{
    public interface IGraphDataServiceQueryClientFactory
    {
        IGraphDataServiceQueryClient CreateClient();
    }
}
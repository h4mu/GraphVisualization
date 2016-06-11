using GraphDataService.ShortestPath.Contract;

namespace GraphDataService.ShortestPath
{
    public interface IGraphDataServiceShortestPathClientFactory
    {
        IGraphDataServiceShortestPathClient CreateClient();
    }
}
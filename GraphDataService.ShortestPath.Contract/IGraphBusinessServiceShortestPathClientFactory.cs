using GraphBusinessService.ShortestPath.Contract;

namespace GraphBusinessService.ShortestPath
{
    public interface IGraphBusinessServiceShortestPathClientFactory
    {
        IGraphBusinessServiceShortestPathClient CreateClient();
    }
}
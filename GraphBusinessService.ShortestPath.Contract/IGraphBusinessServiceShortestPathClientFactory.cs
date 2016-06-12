namespace GraphBusinessService.ShortestPath.Contract
{
    public interface IGraphBusinessServiceShortestPathClientFactory
    {
        IGraphBusinessServiceShortestPathClient CreateClient();
    }
}
namespace GraphBusinessService.ShortestPath.Contract
{
    public class GraphBusinessServiceShortestPathClientFactory : IGraphBusinessServiceShortestPathClientFactory
    {
        public IGraphBusinessServiceShortestPathClient CreateClient()
        {
            return new GraphDataServiceShortestPathClient();
        }
    }
}
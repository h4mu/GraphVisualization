namespace GraphDataService.Query.Contract
{
    public class GraphDataServiceQueryClientFactory : IGraphDataServiceQueryClientFactory
    {
        public IGraphDataServiceQueryClient CreateClient()
        {
            return new GraphDataServiceQueryClient();
        }
    }
}
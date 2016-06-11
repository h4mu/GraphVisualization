namespace GraphDataService.Query.Contract
{
    public interface IGraphDataServiceQueryClientFactory
    {
        IGraphDataServiceQueryClient CreateClient();
    }
}
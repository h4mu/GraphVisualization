using GraphDataService.Command.Contract;

namespace DataLoader
{
    public interface IGraphDataCommandClientFactory
    {
        IGraphDataServiceCommandClient GetGraphDataCommandClient();
    }
}
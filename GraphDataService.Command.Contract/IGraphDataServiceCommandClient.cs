using System;

namespace GraphDataService.Command.Contract
{
    public interface IGraphDataServiceCommandClient : IGraphDataServiceCommand, IDisposable
    {
    }
}

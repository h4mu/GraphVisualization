using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphDataService.ShortestPath.Contract
{
    public interface IGraphDataServiceShortestPathClient : IGraphDataServiceShortestPath, IDisposable
    {
    }
}

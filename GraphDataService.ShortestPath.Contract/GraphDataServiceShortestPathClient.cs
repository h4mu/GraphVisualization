using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GraphDataService.ShortestPath.Contract
{
    public class GraphDataServiceShortestPathClient : ClientBase<IGraphDataServiceShortestPath>, IGraphDataServiceShortestPathClient
    {
        public IList<int> GetVertexIdsOnShortestPath(int idFrom, int idTo)
        {
            return Channel.GetVertexIdsOnShortestPath(idFrom, idTo);
        }
    }
}

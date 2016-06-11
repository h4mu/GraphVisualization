using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GraphBusinessService.ShortestPath.Contract
{
    public class GraphDataServiceShortestPathClient : ClientBase<IGraphBusinessServiceShortestPath>, IGraphBusinessServiceShortestPathClient
    {
        public IList<Edge> GetShortestPath(int idFrom, int idTo)
        {
            return Channel.GetShortestPath(idFrom, idTo);
        }
    }
}

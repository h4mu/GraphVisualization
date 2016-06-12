using System.Collections.Generic;
using System.ServiceModel;

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

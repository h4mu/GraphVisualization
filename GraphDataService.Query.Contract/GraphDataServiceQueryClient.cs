using System.Collections.Generic;
using System.ServiceModel;

namespace GraphDataService.Query.Contract
{
    public class GraphDataServiceQueryClient : ClientBase<IGraphDataServiceQuery>, IGraphDataServiceQueryClient
    {
        public IList<Edge> GetEdges()
        {
            return Channel.GetEdges();
        }

        public Graph GetGraph()
        {
            return Channel.GetGraph();
        }
    }
}

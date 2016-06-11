using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

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

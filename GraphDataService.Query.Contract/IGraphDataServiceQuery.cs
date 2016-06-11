using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GraphDataService.Query.Contract
{
    [ServiceContract(Namespace = "https://github.com/h4mu/GraphVisualization/GraphDataService.Query.Contract")]
    public interface IGraphDataServiceQuery
    {
        [OperationContract]
        Graph GetGraph();

        [OperationContract]
        IList<Edge> GetEdges();
    }
}

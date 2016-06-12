using System.Collections.Generic;
using System.ServiceModel;

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

using System.Collections.Generic;
using System.ServiceModel;

namespace GraphBusinessService.ShortestPath.Contract
{
    [ServiceContract(Namespace = "https://github.com/h4mu/GraphVisualization/GraphBusinessService.ShortestPath.Contract")]
    public interface IGraphBusinessServiceShortestPath
    {
        [OperationContract]
        IList<Edge> GetShortestPath(int idFrom, int idTo);
    }
}

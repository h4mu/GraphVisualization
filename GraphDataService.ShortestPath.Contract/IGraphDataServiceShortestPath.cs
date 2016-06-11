using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GraphDataService.ShortestPath.Contract
{
    [ServiceContract(Namespace = "https://github.com/h4mu/GraphVisualization/GraphDataService.ShortestPath.Contract")]
    public interface IGraphDataServiceShortestPath
    {
        [OperationContract]
        IList<int> GetVertexIdsOnShortestPath(int idFrom, int idTo);
    }
}

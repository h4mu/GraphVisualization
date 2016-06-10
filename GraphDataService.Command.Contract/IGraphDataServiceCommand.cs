using System.ServiceModel;

namespace GraphDataService.Command.Contract
{
    [ServiceContract(Namespace = "https://github.com/h4mu/GraphVisualization/GraphDataService.Command.Contract")]
    public interface IGraphDataServiceCommand
    {
        [OperationContract(IsOneWay = true)]
        void RefreshGraph(Graph graph);
    }
}

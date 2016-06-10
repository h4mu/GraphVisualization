using System.ServiceModel;

namespace GraphDataService.Command.Contract
{
    public class GraphDataServiceCommandClient : ClientBase<IGraphDataServiceCommand>, IGraphDataServiceCommandClient
    {
        public void RefreshGraph(Graph graph)
        {
            Channel.RefreshGraph(graph);
        }
    }
}

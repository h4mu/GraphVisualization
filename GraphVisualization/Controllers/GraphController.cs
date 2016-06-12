using GraphBusinessService.ShortestPath.Contract;
using GraphDataService.Query.Contract;
using GraphVisualization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Graph = GraphVisualization.Models.Graph;
using Edge = GraphVisualization.Models.Edge;

namespace GraphVisualization.Controllers
{
    public class GraphController : ApiController
    {
        private IGraphBusinessServiceShortestPathClientFactory businessClientFactory;
        private IGraphDataServiceQueryClientFactory queryClientFactory;

        public GraphController(IGraphDataServiceQueryClientFactory queryClientFactory, IGraphBusinessServiceShortestPathClientFactory businessClientFactory)
        {
            this.queryClientFactory = queryClientFactory;
            this.businessClientFactory = businessClientFactory;
        }

        public Graph GetGraph()
        {
            using (var client = queryClientFactory.CreateClient())
            {
                var queryGraph = client.GetGraph();
                return new Graph
                {
                    Nodes = queryGraph.Vertices
                        .Select(v => new Node { Id = v.Id, Label = v.Label }),
                    Edges = queryGraph.Edges
                        .Select(e => new Edge
                        {
                            From = Math.Min(e.Vertex1, e.Vertex2),
                            To = Math.Max(e.Vertex1, e.Vertex2)
                        })
                        .Distinct()
                };
            }
        }

        public IEnumerable<Edge> GetShortestPath(int idFrom, int idTo)
        {
            using (var client = businessClientFactory.CreateClient())
            {
                return client.GetShortestPath(idFrom, idTo).Select(e => new Edge
                {
                    From = Math.Min(e.Vertex1, e.Vertex2),
                    To = Math.Max(e.Vertex1, e.Vertex2)
                }).Distinct();
            }
        }
    }
}

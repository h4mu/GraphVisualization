using GraphBusinessService.ShortestPath.Contract;
using GraphDataService.Query.Contract;
using GraphVisualization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Graph = GraphVisualization.Models.Graph;
using Edge = GraphVisualization.Models.Edge;
using Common;
using log4net;

namespace GraphVisualization.Controllers
{
    public class GraphController : ApiController
    {
        private ILog log;
        private IGraphBusinessServiceShortestPathClientFactory businessClientFactory;
        private IGraphDataServiceQueryClientFactory queryClientFactory;

        public GraphController(ILoggerFactory loggerFactory, IGraphDataServiceQueryClientFactory queryClientFactory, IGraphBusinessServiceShortestPathClientFactory businessClientFactory)
        {
            log = loggerFactory.GetLogger(GetType());
            this.queryClientFactory = queryClientFactory;
            this.businessClientFactory = businessClientFactory;
        }

        public Graph GetGraph()
        {
            log.Info("Serving graph request");
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
                            Id = string.Format("{0}-{1}",
                                Math.Min(e.Vertex1, e.Vertex2),
                                Math.Max(e.Vertex1, e.Vertex2)),
                            From = Math.Min(e.Vertex1, e.Vertex2),
                            To = Math.Max(e.Vertex1, e.Vertex2)
                        })
                        .Distinct()
                };
            }
        }

        public IDictionary<string, bool> GetShortestPath(int idFrom, int idTo)
        {
            log.Info("Serving shortest path request");
            using (var client = businessClientFactory.CreateClient())
            {
                return client.GetShortestPath(idFrom, idTo).ToDictionary(
                    e => string.Format("{0}-{1}",
                        Math.Min(e.Vertex1, e.Vertex2),
                        Math.Max(e.Vertex1, e.Vertex2)),
                    e => true);
            }
        }
    }
}

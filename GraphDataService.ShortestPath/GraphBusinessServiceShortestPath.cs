using Common;
using GraphBusinessService.ShortestPath.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using log4net;
using QuickGraph;
using QuickGraph.Algorithms;
using Edge = GraphBusinessService.ShortestPath.Contract.Edge;
using GraphDataService.Query.Contract;

namespace GraphBusinessService.ShortestPath
{
    public class GraphBusinessServiceShortestPath : IGraphBusinessServiceShortestPath
    {
        private IGraphDataServiceQueryClientFactory clientFactory;
        private ILog log;

        public GraphBusinessServiceShortestPath(ILoggerFactory loggerFactory, IGraphDataServiceQueryClientFactory clientFactory)
        {
            log = loggerFactory.GetLogger(GetType());
            this.clientFactory = clientFactory;
        }

        public IList<Edge> GetShortestPath(int idFrom, int idTo)
        {
            log.Info("Received shortest path request.");
            try
            {
                using (var client = clientFactory.CreateClient())
                {
                    log.Debug("Retrieving data...");
                    var edges = client.GetEdges();
                    log.DebugFormat("Received {0} edges.", edges.Count);
                    var paths = edges.Select(edge => new SEdge<int>(edge.Vertex1, edge.Vertex2))
                        .ToAdjacencyGraph<int, SEdge<int>>()
                        .ShortestPathsDijkstra(e => 1.0, idFrom);
                    IEnumerable<SEdge<int>> result;
                    if (paths(idTo, out result))
                    {
                        return result.Select(e => new Edge { Vertex1 = e.Source, Vertex2 = e.Target }).ToList();
                    }
                    log.Debug("No Path found.");
                    return new List<Edge>();
                }
            }
            catch (Exception e)
            {
                log.Fatal("Error occured while querying the graph.", e);
                throw;
            }
        }
    }
}

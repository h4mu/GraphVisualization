using Common;
using GraphDataService.Query.Contract;
using log4net;
using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GraphDataService.Query
{
    public class GraphDataServiceQuery : IGraphDataServiceQuery
    {
        private const string GetEdgesCypher = "MATCH (n:Vertex)--(m:Vertex) RETURN n.id, m.id";
        private const string GetNodesCypher = "MATCH (v:Vertex) RETURN v.id, v.label";
        private ILog log;
        private IDriver driver;

        public GraphDataServiceQuery(ILoggerFactory loggerFactory, IDriver driver)
        {
            log = loggerFactory.GetLogger(GetType());
            this.driver = driver;
        }

        public IList<Edge> GetEdges()
        {
            log.Info("Requested edges.");
            try
            {
                using (var session = driver.Session())
                {
                    return GetEdges(session);
                }
            }
            catch (Exception e)
            {
                log.Fatal("Error occured while querying the graph.", e);
                throw;
            }
        }

        private IList<Edge> GetEdges(IStatementRunner runner)
        {
            log.Debug("Retrieving edges...");
            var result = runner.Run(GetEdgesCypher);
            log.Debug("Done.");
            return result.Select(rec => new Edge
            {
                Vertex1 = Convert.ToInt32(rec["n.id"]),
                Vertex2 = Convert.ToInt32(rec["m.id"])
            }).ToList();
        }

        public Graph GetGraph()
        {
            log.Info("Requested graph.");
            try
            {
                using (var session = driver.Session())
                using (var transaction = session.BeginTransaction())
                {
                    log.Debug("Retrieving vertices...");
                    var vertices = transaction.Run(GetNodesCypher);
                    log.Debug("Done.");
                    var verticesList = vertices.Select(rec => new Vertex
                    {
                        Id = Convert.ToInt32(rec["v.id"]),
                        Label = rec["v.label"].ToString()
                    }).ToList();
                    return new Graph
                    {
                        Edges = GetEdges(transaction),
                        Vertices = verticesList
                    };
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

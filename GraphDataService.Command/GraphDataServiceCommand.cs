using Common;
using GraphDataService.Command.Contract;
using log4net;
using Neo4j.Driver.V1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace GraphDataService.Command
{
    public class GraphDataServiceCommand : IGraphDataServiceCommand
    {
        private const string CleanCypher = "MATCH (v) DETACH DELETE v";
        private const string InitCypher = "CREATE CONSTRAINT ON (vertex:Vertex) ASSERT vertex.id IS UNIQUE";
        private const string PopulateCypher = @"
UNWIND {vertexes} AS vtx
UNWIND vtx.AdjacentNodeIds AS nodeId
MERGE (v:Vertex { id: vtx.Id })
ON CREATE SET v.label = vtx.Label
ON MATCH SET v.label = vtx.Label
MERGE (n:Vertex { id: nodeId })
MERGE (v)-[:CONNECTED]-(n)
";
        private ILog log;
        private IDriver driver;

        public GraphDataServiceCommand(ILoggerFactory loggerFactory, IDriver driver)
        {
            log = loggerFactory.GetLogger(GetType());
            this.driver = driver;
        }

        public void RefreshGraph(Graph graph)
        {
            log.Info("Received graph to persist.");
            try
            {
                using (var session = driver.Session())
                using (var transaction = session.BeginTransaction())
                {
                    log.Debug("Cleaning the database...");
                    transaction.Run(CleanCypher);
                    log.Debug("Initializing schema...");
                    transaction.Run(InitCypher);
                    log.Debug("Populating the database...");
                    transaction.Run(PopulateCypher, new Dictionary<string, object> { { "vertexes", graph.Vertices } });
                    transaction.Success();
                    log.Debug("Done.");
                }
            }
            catch (Exception e)
            {
                log.Fatal("Error occured while persisting the graph.", e);
                throw;
            }
        }
    }
}

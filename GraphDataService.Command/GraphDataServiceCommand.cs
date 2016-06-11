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
        private const string CleanCypher = "MATCH (v:Vertex) DETACH DELETE v";
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
                {
                    log.Debug("Cleaning the database...");
                    session.Run(CleanCypher);
                    log.Debug("Initializing schema...");
                    session.Run(InitCypher);
                    log.Debug("Populating the database...");
                    session.Run(PopulateCypher, new Dictionary<string, object>
                        {
                            {
                                "vertexes",
                                graph.Vertices
                                    .Select(v => new Dictionary<string, object> {
                                        { "Id", v.Id }, { "Label", v.Label }, { "AdjacentNodeIds", v.AdjacentNodeIds }
                                    }).ToList()
                            }
                        });
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

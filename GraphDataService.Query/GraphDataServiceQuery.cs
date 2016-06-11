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
                    log.Debug("Retrieving data...");
                    var result = session.Run(GetEdgesCypher);
                    log.Debug("Done.");
                    return result.Select(rec => new Edge
                        {
                            Vertex1 = Convert.ToInt32(rec.Values["n.id"]),
                            Vertex2 = Convert.ToInt32(rec.Values["m.id"])
                        }).ToList();
                }
            }
            catch (Exception e)
            {
                log.Fatal("Error occured while querying the graph.", e);
                throw;
            }
        }

        public Graph GetGraph()
        {
            throw new NotImplementedException();
        }
    }
}

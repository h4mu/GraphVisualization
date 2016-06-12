using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphVisualization.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Common;
using log4net;
using GraphDataService.Query.Contract;
using GraphBusinessService.ShortestPath.Contract;

namespace GraphVisualization.Controllers.Tests
{
    [TestClass]
    public class GraphControllerTests
    {
        Mock<ILoggerFactory> loggerFactory;
        Mock<ILog> log;
        Mock<IGraphDataServiceQueryClientFactory> queryFactory;
        Mock<IGraphDataServiceQueryClient> queryClient;
        Mock<IGraphBusinessServiceShortestPathClientFactory> businessFactory;
        Mock<IGraphBusinessServiceShortestPathClient> businessClient;

        [TestInitialize]
        public void SetUpMocks()
        {
            log = new Mock<ILog>();
            loggerFactory = new Mock<ILoggerFactory>();
            loggerFactory.Setup(f => f.GetLogger(It.IsAny<Type>())).Returns(log.Object);
            queryClient = new Mock<IGraphDataServiceQueryClient>();
            queryFactory = new Mock<IGraphDataServiceQueryClientFactory>();
            queryFactory.Setup(f => f.CreateClient()).Returns(queryClient.Object);
            businessClient = new Mock<IGraphBusinessServiceShortestPathClient>();
            businessFactory = new Mock<IGraphBusinessServiceShortestPathClientFactory>();
            businessFactory.Setup(f => f.CreateClient()).Returns(businessClient.Object);
        }

        [TestMethod]
        public void WhenGettingGraphDuplicateEdgesShouldBeRemoved()
        {
            queryClient.Setup(c => c.GetGraph()).Returns(new Graph
            {
                Edges = new List<GraphDataService.Query.Contract.Edge>
                {
                    new GraphDataService.Query.Contract.Edge { Vertex1 = 1, Vertex2 = 2 },
                    new GraphDataService.Query.Contract.Edge { Vertex1 = 2, Vertex2 = 1 },
                    new GraphDataService.Query.Contract.Edge { Vertex1 = 2, Vertex2 = 2 }
                },
                Vertices = new List<Vertex>
                {
                    new Vertex { Id = 1, Label = "a" },
                    new Vertex { Id = 2, Label = "b" }
                }
            });

            var sut = new GraphController(loggerFactory.Object, queryFactory.Object, businessFactory.Object);
            var graph = sut.GetGraph();

            Assert.AreEqual(2, graph.Nodes.Count());
            Assert.AreEqual(2, graph.Edges.Count());
            Assert.IsTrue(graph.Edges.Any(e => e.Id == "1-2"));
            Assert.IsTrue(graph.Edges.Any(e => e.Id == "2-2"));
        }

        [TestMethod]
        public void WhenGettingShortestPathIdsShouldBeWellFormatted()
        {
            businessClient.Setup(c => c.GetShortestPath(It.IsAny<int>(), It.IsAny<int>())).Returns(new List<GraphBusinessService.ShortestPath.Contract.Edge>
            {
                new GraphBusinessService.ShortestPath.Contract.Edge { Vertex1 = 2, Vertex2 = 1 }
            });

            var sut = new GraphController(loggerFactory.Object, queryFactory.Object, businessFactory.Object);
            var path = sut.GetShortestPath(2,1);

            Assert.AreEqual(1, path.Count);
            Assert.IsTrue(path["1-2"]);
        }
    }
}
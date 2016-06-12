using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Moq;
using Common;
using log4net;
using Neo4j.Driver.V1;

namespace GraphDataService.Query.Tests
{
    [TestClass]
    public class GraphDataServiceQueryTests
    {
        Mock<ILoggerFactory> loggerFactory;
        Mock<ILog> log;
        Mock<IDriver> driver;
        Mock<ISession> session;
        Mock<ITransaction> transaction;

        [TestInitialize]
        public void SetUpMocks()
        {
            log = new Mock<ILog>();
            loggerFactory = new Mock<ILoggerFactory>();
            loggerFactory.Setup(f => f.GetLogger(It.IsAny<Type>())).Returns(log.Object);
            transaction = new Mock<ITransaction>();
            session = new Mock<ISession>();
            session.Setup(s => s.BeginTransaction()).Returns(transaction.Object);
            driver = new Mock<IDriver>();
            driver.Setup(d => d.Session()).Returns(session.Object);
        }

        [TestMethod]
        public void GetEdgesShouldReturnTheEdgesReceivedFromTheDb()
        {
            var record = new Mock<IRecord>();
            record.Setup(r => r["n.id"]).Returns(1);
            record.Setup(r => r["m.id"]).Returns(2);
            var result = new Mock<IStatementResult>();
            result.Setup(r => r.GetEnumerator()).Returns(new List<IRecord> { record.Object }.GetEnumerator());
            session.Setup(s => s.Run(It.IsAny<string>(), null)).Returns(result.Object);

            var sut = new GraphDataServiceQuery(loggerFactory.Object, driver.Object);
            var edges = sut.GetEdges();

            Assert.AreEqual(1, edges.Count);
            Assert.AreEqual(1, edges[0].Vertex1);
            Assert.AreEqual(2, edges[0].Vertex2);
        }

        [TestMethod()]
        public void GetGraphShouldReturnTheVerticesReceivedFromTheDb()
        {
            var record = new Mock<IRecord>();
            record.Setup(r => r["n.id"]).Returns(1);
            record.Setup(r => r["m.id"]).Returns(2);
            record.Setup(r => r["v.id"]).Returns(1);
            record.Setup(r => r["v.label"]).Returns("aaa");
            var result = new Mock<IStatementResult>();
            result.Setup(r => r.GetEnumerator()).Returns(new List<IRecord> { record.Object }.GetEnumerator());
            session.Setup(s => s.Run(It.IsAny<string>(), null)).Returns(result.Object);
            transaction.Setup(s => s.Run(It.IsAny<string>(), null)).Returns(result.Object);

            var sut = new GraphDataServiceQuery(loggerFactory.Object, driver.Object);
            var graph = sut.GetGraph();

            Assert.AreEqual(1, graph.Vertices.Count);
            Assert.AreEqual(1, graph.Vertices[0].Id);
            Assert.AreEqual("aaa", graph.Vertices[0].Label);
        }
    }
}
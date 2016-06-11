using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphDataService.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        [TestInitialize]
        public void SetUpMocks()
        {
            log = new Mock<ILog>();
            loggerFactory = new Mock<ILoggerFactory>();
            loggerFactory.Setup(f => f.GetLogger(It.IsAny<Type>())).Returns(log.Object);
            session = new Mock<ISession>();
            driver = new Mock<IDriver>();
            driver.Setup(d => d.Session()).Returns(session.Object);
        }

        [TestMethod]
        public void GraphDataServiceQueryTest()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetEdgesShouldReturnTheEdgesReceivedFromTheDb()
        {
            var record = new Mock<IRecord>();
            record.Setup(r => r.Values).Returns(new Dictionary<string, object> { { "n.id", 1 }, { "m.id", 2 } });
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
        public void GetGraphTest()
        {
            Assert.Fail();
        }
    }
}
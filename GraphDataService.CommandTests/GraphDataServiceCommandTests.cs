using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphDataService.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Common;
using log4net;
using Neo4j.Driver.V1;
using GraphDataService.Command.Contract;

namespace GraphDataService.Command.Tests
{
    [TestClass]
    public class GraphDataServiceCommandTests
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
        public void WhenProvidedWithGraphRefreshRuns()
        {
            var sut = new GraphDataServiceCommand(loggerFactory.Object, driver.Object);
            sut.RefreshGraph(new Graph
            {
                Vertices = new List<Vertex> { new Vertex { Id = 0, Label = "a", AdjacentNodeIds = new List<int> { } } }
            });

            transaction.Verify(t => t.Run(It.IsAny<string>(), It.IsAny< Dictionary<string, object>>()), Times.Exactly(3));
            transaction.Verify(t => t.Success(), Times.Once);
        }
    }
}
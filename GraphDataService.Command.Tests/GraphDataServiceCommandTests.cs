using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
        public void WhenProvidedWithGraphRefreshRuns()
        {
            var sut = new GraphDataServiceCommand(loggerFactory.Object, driver.Object);
            sut.RefreshGraph(new Graph
            {
                Vertices = new List<Vertex> { new Vertex { Id = 0, Label = "a", AdjacentNodeIds = new List<int> { } } }
            });

            session.Verify(t => t.Run(It.IsAny<string>(), It.IsAny< Dictionary<string, object>>()), Times.Exactly(3));
        }
    }
}
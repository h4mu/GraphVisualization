using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using log4net;
using GraphDataService.Command.Contract;
using System.IO;
using System.Linq;
using Common;

namespace DataLoader.Test
{
    [TestClass]
    public class DataParserUnitTests
    {
        private const string FileName = "aaa.xml";
        Mock<ILoggerFactory> loggerFactory;
        Mock<ILog> log;
        Mock<IGraphDataCommandClientFactory> factory;
        Mock<IGraphDataServiceCommandClient> client;
        Mock<IInputFilenameProvider> filenameProvider;

        [TestInitialize]
        public void SetUpMocks()
        {
            log = new Mock<ILog>();
            loggerFactory = new Mock<ILoggerFactory>();
            loggerFactory.Setup(f => f.GetLogger(It.IsAny<Type>())).Returns(log.Object);
            factory = new Mock<IGraphDataCommandClientFactory>();
            client = new Mock<IGraphDataServiceCommandClient>();
            factory.Setup(f => f.GetGraphDataCommandClient()).Returns(client.Object);
            filenameProvider = new Mock<IInputFilenameProvider>();
        }

        [TestCleanup]
        public void RemoveTestFiles()
        {
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
        }

        [TestMethod]
        public void WhenThereIsNoInputDoNothing()
        {
            filenameProvider.Setup(provider => provider.GetInputFileNames()).Returns(new string[0]);

            var sut = new DataParser(loggerFactory.Object, factory.Object, filenameProvider.Object);
            sut.Parse();

            client.Verify(c => c.RefreshGraph(It.IsAny<Graph>()), Times.Never);
        }

        [TestMethod]
        public void WhenThereIsInputParseItCorrectly()
        {
            filenameProvider.Setup(provider => provider.GetInputFileNames()).Returns(new[] { FileName });
            File.WriteAllText(FileName, "<node><id>6</id><label>aaa</label><adjacentNodes><id>1</id><id>2</id></adjacentNodes></node>");
            Graph graph = null;
            client.Setup(c => c.RefreshGraph(It.IsAny<Graph>())).Callback<Graph>(g => graph = g);

            var sut = new DataParser(loggerFactory.Object, factory.Object, filenameProvider.Object);
            sut.Parse();

            client.Verify(c => c.RefreshGraph(It.IsAny<Graph>()));
            Assert.IsNotNull(graph);
            var vertices = graph.Vertices.ToArray();
            Assert.AreEqual(1, vertices.Length);
            Assert.AreEqual("aaa", vertices[0].Label);
            Assert.AreEqual(6, vertices[0].Id);
            var ids = vertices[0].AdjacentNodeIds.ToArray();
            Assert.AreEqual(2, ids.Length);
            Assert.AreEqual(1, ids[0]);
            Assert.AreEqual(2, ids[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void WhenThereIsAnErrorThrowAnException()
        {
            filenameProvider.Setup(provider => provider.GetInputFileNames()).Returns(new[] { FileName });

            var sut = new DataParser(loggerFactory.Object, factory.Object, filenameProvider.Object);
            sut.Parse();

            client.Verify(c => c.RefreshGraph(It.IsAny<Graph>()), Times.Never);
        }
    }
}

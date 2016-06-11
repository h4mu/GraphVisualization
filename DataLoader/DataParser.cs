using Common;
using GraphDataService.Command.Contract;
using log4net;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DataLoader
{
    public class DataParser
    {
        private readonly IGraphDataCommandClientFactory clientFactory;
        private readonly IInputFilenameProvider filesProvider;
        private readonly ILog log;

        public DataParser(ILoggerFactory loggerFactory, IGraphDataCommandClientFactory clientFactory, IInputFilenameProvider filesProvider)
        {
            log = loggerFactory.GetLogger(GetType());
            this.clientFactory = clientFactory;
            this.filesProvider = filesProvider;
        }

        public void Parse()
        {
            try
            {
                log.Info("Parsing XML files.");
                var files = filesProvider.GetInputFileNames();
                if (files.Length > 0)
                {
                    log.InfoFormat("Found {0} files, proceeding.", files.Length);
                    using (var client = clientFactory.GetGraphDataCommandClient())
                    {
                        client.RefreshGraph(new Graph
                        {
                            Vertices = (from file in files
                                       let xml = XDocument.Load(file).Element("node")
                                       select new Vertex
                                       {
                                           Label = xml.Element("label").Value,
                                           Id = int.Parse(xml.Element("id").Value),
                                           AdjacentNodeIds = (from id in xml.Element("adjacentNodes").Descendants()
                                                             select int.Parse(id.Value)).ToList()
                                       }).ToList()
                        });
                        log.Info("Refresh request sent to data server");
                    }
                }
                else
                {
                    log.Info("No input, returning.");
                }
            }
            catch (ArgumentNullException e)
            {
                log.Fatal("Parsing error occured while processing.", e);
                throw;
            }
            catch (FormatException e)
            {
                log.Fatal("Parsing error occured while processing.", e);
                throw;
            }
            catch (NullReferenceException e)
            {
                log.Fatal("Parsing error occured while processing.", e);
                throw;
            }
            catch (Exception e)
            {
                log.Fatal("Error occured while processing.", e);
                throw;
            }
        }
    }
}
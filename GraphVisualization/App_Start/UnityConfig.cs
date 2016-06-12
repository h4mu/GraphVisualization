using Common;
using GraphBusinessService.ShortestPath.Contract;
using GraphDataService.Query.Contract;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace GraphVisualization
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<ILoggerFactory, LogManagerFacade>()
                .RegisterType<IGraphDataServiceQueryClientFactory, GraphDataServiceQueryClientFactory>()
                .RegisterType<IGraphBusinessServiceShortestPathClientFactory, GraphBusinessServiceShortestPathClientFactory>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}
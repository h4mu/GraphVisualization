using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Common;
using GraphDataService.Query.Contract;

namespace GraphVisualization
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            container.RegisterType<ILoggerFactory, LogManagerFacade>()
                .RegisterType<IGraphDataServiceQueryClientFactory, GraphDataServiceQueryClientFactory>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
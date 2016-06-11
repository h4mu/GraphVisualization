using Common;
using GraphBusinessService.ShortestPath.Contract;
using GraphDataService.Query.Contract;
using Microsoft.Practices.Unity;
using Unity.Wcf;

namespace GraphBusinessService.ShortestPath
{
	public class WcfServiceFactory : UnityServiceHostFactory
    {
        protected override void ConfigureContainer(IUnityContainer container)
        {
            container.RegisterType<ILoggerFactory, LogManagerFacade>()
                .RegisterType<IGraphDataServiceQueryClientFactory, GraphDataServiceQueryClientFactory>()
                .RegisterType<IGraphBusinessServiceShortestPath, GraphBusinessServiceShortestPath>();
        }
    }    
}
using Common;
using GraphDataService.ShortestPath.Contract;
using Microsoft.Practices.Unity;
using Unity.Wcf;

namespace GraphDataService.ShortestPath
{
	public class WcfServiceFactory : UnityServiceHostFactory
    {
        protected override void ConfigureContainer(IUnityContainer container)
        {
            container.RegisterType<ILoggerFactory, LogManagerFacade>()
                .RegisterType<IGraphDataServiceQueryClientFactory, GraphDataServiceQueryClientFactory>()
                .RegisterType<IGraphDataServiceShortestPath, GraphDataServiceShortestPath>();
        }
    }    
}
using Common;
using GraphDataService.Query.Contract;
using Microsoft.Practices.Unity;
using Neo4j.Driver.V1;
using Unity.Wcf;

namespace GraphDataService.Query
{
	public class WcfServiceFactory : UnityServiceHostFactory
    {
        protected override void ConfigureContainer(IUnityContainer container)
        {
            container.RegisterType<ILoggerFactory, LogManagerFacade>()
                .RegisterType<IGraphDataServiceQuery, GraphDataServiceQuery>()
                .RegisterType<IConfigProvider, ConfigProvider>()
                .RegisterType<IDbDriverFactory, DbDriverFactory>()
                .RegisterType<IDriver>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionFactory(c => c.Resolve<IDbDriverFactory>().CreateDriver()));
        }
    }    
}
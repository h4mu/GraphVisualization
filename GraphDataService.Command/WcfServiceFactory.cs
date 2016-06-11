using Common;
using GraphDataService.Command.Contract;
using Microsoft.Practices.Unity;
using Neo4j.Driver.V1;
using Unity.Wcf;

namespace GraphDataService.Command
{
    public class WcfServiceFactory : UnityServiceHostFactory
    {
        protected override void ConfigureContainer(IUnityContainer container)
        {
            container.RegisterType<ILoggerFactory, LogManagerFacade>()
                .RegisterType<IGraphDataServiceCommand, GraphDataServiceCommand>()
                .RegisterType<IConfigProvider, ConfigProvider>()
                .RegisterType<IDbDriverFactory, DbDriverFactory>()
                .RegisterType<IDriver>(
                    new ContainerControlledLifetimeManager(),
                    new InjectionFactory(c => c.Resolve<IDbDriverFactory>().CreateDriver()));
        }
    }    
}
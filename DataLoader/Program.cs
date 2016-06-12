using Common;
using Microsoft.Practices.Unity;

namespace DataLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            container.RegisterType<ILoggerFactory, LogManagerFacade>()
                .RegisterType<IGraphDataCommandClientFactory, GraphDataCommandClientFactory>()
                .RegisterType<IInputFilenameProvider, InputFilenameProvider>()
                .RegisterInstance<string[]>(args);
            var log = container.Resolve<ILoggerFactory>().GetLogger(typeof(Program));
            log.Info("Program started, resolving parser.");
            var parser = container.Resolve<DataParser>();
            log.Info("Running parser.");
            parser.Parse();
            log.Info("Finished.");
        }
    }
}

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
                .RegisterType<IInputFilenameProvider, InputFilenameProvider>();
            var parser = container.Resolve<DataParser>();
            parser.Parse();
        }
    }
}

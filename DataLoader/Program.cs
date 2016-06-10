using log4net;
using Microsoft.Practices.Unity;

namespace DataLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            container.RegisterType<ILog>(new InjectionFactory(c => LogManager.GetLogger(typeof(Program))))
                .RegisterType<IGraphDataCommandClientFactory, GraphDataCommandClientFactory>()
                .RegisterType<IInputFilenameProvider, InputFilenameProvider>();
            var parser = container.Resolve<DataParser>();
            parser.Parse();
        }
    }
}

using log4net;
using log4net.Config;
using System;

namespace Common
{
    public class LogManagerFacade : ILoggerFactory
    {
        public LogManagerFacade()
        {
            XmlConfigurator.Configure();
        }

        public ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(type);
        }
    }
}

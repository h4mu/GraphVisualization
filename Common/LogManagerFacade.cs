using log4net;
using System;

namespace Common
{
    public class LogManagerFacade : ILoggerFactory
    {
        public ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(type);
        }
    }
}

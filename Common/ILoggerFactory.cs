using System;
using log4net;

namespace Common
{
    public interface ILoggerFactory
    {
        ILog GetLogger(Type type);
    }
}
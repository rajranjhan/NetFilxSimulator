using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace AR.NETFixSimulator.Infrastructure
{
    /// <summary>
    /// Adapter for Log4net library
    /// </summary>
    internal  class Log4netLogger : ILog
    {
        #region Fields
        private readonly log4net.ILog _l4nLogger;
        #endregion


        public Log4netLogger(Type type)
        {
            _l4nLogger = log4net.LogManager.GetLogger(type);
        }

        public void Error(Exception exception)
        {
            _l4nLogger.Error(exception.Message);
        }

        public void Info(string format, params object[] args)
        {
            _l4nLogger.InfoFormat(format, args);
        }

        public void Warn(string format, params object[] args)
        {
            _l4nLogger.WarnFormat(format, args);
        }
    }
}

using log4net;
using System;
using System.Threading.Tasks;

namespace SEO.Lib.Utilities
{
    public static class Log4netWrapper
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Error(Exception ex, string message)
        {
            Task.Factory.StartNew(() => _log.Error(string.Format("Custom Message : {0} Error message : {1}, Error Stack Trace : {2}", message, ex.Message, ex.StackTrace)));
        }

        public static void Error(Exception ex)
        {
            Task.Factory.StartNew(() => _log.Error(string.Format("Error message : {0}, Error Stack Trace : {1}", ex.Message, ex.StackTrace)));
        }

        public static void Error(string message)
        {
            Task.Factory.StartNew(() => _log.Error(string.Format("Error message : {0}", message)));
        }

        public static void Info(string message)
        {
            Task.Factory.StartNew(() => _log.Info(string.Format("Info message : {0}", message)));
        }

        public static void Debug(string message)
        {
            Task.Factory.StartNew(() => _log.Debug(string.Format("Debug message : {0}", message)));
        }
    }
}
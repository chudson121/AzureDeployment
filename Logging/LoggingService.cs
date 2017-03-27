using System;
using System.Reflection;
using NLog;
using ILogger = Logging.Contracts.ILogger;

namespace Logging
{
    /// <summary>
    ///     Log implementation uses nlog
    /// </summary>
    /// <example>
    ///private static readonly ILogger Logger = new LoggingService();
    ///public static void Main(string[] args)
    ///{
    ///    logger.Info("Program startup");
    ///}
    /// </example>
    public class LoggingService : ILogger
    {
        private static Func<Logger> _logger;

        public LoggingService()
        {
            var appName = (Assembly.GetEntryAssembly() != null)
                ? Assembly.GetEntryAssembly().GetName().Name //Needed for calling assembly
                : GetType().Assembly.GetName(false).Name; //needed for unit test calls
           
            _logger = () => LogManager.GetLogger(appName);

        }

        public string Name => _logger().Name;

        public void Error(string message)
        {
            _logger().Error(message);
        }

        public void Error(string message, Exception ex)
        {
            _logger().Error(ex, message);
        }

        public virtual void Info(string message)
        {
            _logger().Info(message);
        }
    }
}
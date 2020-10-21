using NLog;
using System;

namespace Improve.LoggerLib
{
    public enum Level
    {
        Debug,
        Info,
        Error,
        Fatal,
        Warn
    }

    public class Logger
    {
        private static NLog.Logger _logger = LogManager.GetCurrentClassLogger();

        public static void Log(Level level,string message)
        {
            switch (level)
            {
                case Level.Debug:
                    _logger.Debug(message);
                    break;
                case Level.Info:
                    _logger.Info(message);
                    break;
                case Level.Error:
                    _logger.Error(message);
                    break;
                case Level.Fatal:
                    _logger.Fatal(message);
                    break;
                case Level.Warn:
                    _logger.Warn(message);
                    break;
                default:
                    break;
            }
        }
    }
}

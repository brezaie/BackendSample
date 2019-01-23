using System;
using NLog;

namespace BackendTest.Common
{
    public class CustomLogger
    {
        private static NLog.Logger _logger;

        public CustomLogger()
        {
            if (_logger == null)
            {
                _logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            }
        }

        public void ErrorException(string message, System.Exception exception)
        {
            GenerateLog<dynamic>(message, LogLevel.Error, exception, null);
        }
        public void ErrorException<T>(string message, System.Exception exception, T data)
        {
            GenerateLog(message, LogLevel.Error, exception, data);

        }

        public void Info<T>(string message, T data)
        {
            GenerateLog(message, LogLevel.Info, null, data);
        }


        public void Info(string message)
        {
            GenerateLog<dynamic>(message, LogLevel.Info, null, null);

        }

        public void Error(string message)
        {
            GenerateLog<dynamic>(message, LogLevel.Error, null, null);

        }
        private static void GenerateLog<T>(string message, LogLevel logLevel, Exception ex, T data)
        {
            if (ex == null)
            {
                ex = new Exception(message);
            }
            if (data != null)
            {
                ex.Data.Add("Input", data);

            }
            var logEvent = new LogEventInfo(logLevel, _logger.Name, message)
            {
                Exception = new System.Exception(message),
            };

            //var l = new LogEventInfo(logLevel, message, null, message);

            _logger.Log(typeof(CustomLogger), logEvent);
        }

    }
}
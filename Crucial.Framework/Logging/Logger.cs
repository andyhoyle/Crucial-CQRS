using System;
using System.Diagnostics;
using System.Reflection;
using System.Web;
using NLog;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Net;
using System.Text;
using NLog.Config;

namespace Crucial.Framework.Logging
{
    public class CrucialLogger : ILogger
    {
        private readonly Logger _nLogger;

        public CrucialLogger()
        {
            _nLogger = LogManager.GetLogger(GetType().FullName);
        }

        #region Exception Management
        public void LogException(string message, Exception exception, ExceptionType exceptionType = ExceptionType.Handled)
        {
            if (exceptionType == ExceptionType.Unhandled)
            {
                Fatal(message, exception);
            }
            else
            {
                Error(message, exception);
            }
        }

        public void LogException(string message, Exception exception, HttpRequest httpRequest, ExceptionType exceptionType = ExceptionType.Handled)
        {
            if (exceptionType == ExceptionType.Unhandled)
            {
                Fatal(message, exception, httpRequest);
            }
            else
            {
                Error(message, exception, httpRequest);
            }
        }

        public void LogException(string message, Exception exception, HttpRequestBase httpRequestBase, ExceptionType exceptionType = ExceptionType.Handled)
        {
            if (exceptionType == ExceptionType.Unhandled)
            {
                Fatal(message, exception, httpRequestBase);
            }
            else
            {
                Error(message, exception, httpRequestBase);
            }
        }

        public void LogException(Exception exception, ExceptionType exceptionType = ExceptionType.Handled)
        {
            if (exceptionType == ExceptionType.Unhandled)
            {
                Fatal(ExceptionToString(exception), exception);
            }
            else
            {
                Error(ExceptionToString(exception), exception);
            }
        }

        public void LogException(Exception exception, HttpRequestBase httpRequestBase, ExceptionType exceptionType = ExceptionType.Handled)
        {
            if (exceptionType == ExceptionType.Unhandled)
            {
                Fatal(ExceptionToString(exception), exception, httpRequestBase);
            }
            else
            {
                Error(ExceptionToString(exception), exception, httpRequestBase);
            }
        }

        public void LogException(Exception exception, HttpRequest httpRequest, ExceptionType exceptionType = ExceptionType.Handled)
        {
            if (exceptionType == ExceptionType.Unhandled)
            {
                Fatal(ExceptionToString(exception), exception, httpRequest);
            }
            else
            {
                Error(ExceptionToString(exception), exception, httpRequest);
            }
        }

        public void LogException(WebException webException, ExceptionType exceptionType = ExceptionType.Handled)
        {
            if (exceptionType == ExceptionType.Unhandled)
            {
                Fatal(ExceptionToString(webException), webException);
            }
            else
            {
                Error(ExceptionToString(webException), webException);
            }
        }

        public void LogException(DbUpdateException dbUpdateException)
        {
            Error(ExceptionToString(dbUpdateException), dbUpdateException);
        }

        public void LogException(DbEntityValidationException dbEntityValidationException)
        {
            Error(ExceptionToString(dbEntityValidationException), dbEntityValidationException);
        }

        private string ExceptionToString(WebException webException)
        {
            //ToDo : Build web exception
            return webException.ToString();
        }

        private string ExceptionToString(DbEntityValidationException dbEntityValidationException)
        {
            StringBuilder exceptionString = new StringBuilder();

            exceptionString.AppendLine("DbEntityValidationException:");

            foreach (var validationErrors in dbEntityValidationException.EntityValidationErrors)
            {
                foreach (var validationError in validationErrors.ValidationErrors)
                {
                    exceptionString.AppendFormat("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                }
            }

            return exceptionString.ToString();
        }

        private string ExceptionToString(DbUpdateException dbUpdateException)
        {
            StringBuilder exceptionString = new StringBuilder();

            exceptionString.AppendLine("DbUpdateException:");

            foreach (DbEntityEntry dbEntityEntry in dbUpdateException.Entries)
            {
                foreach (DbValidationError validationError in dbEntityEntry.GetValidationResult().ValidationErrors)
                {
                    exceptionString.AppendFormat("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                }
            }

            return exceptionString.ToString();
        }

        private string ExceptionToString(Exception exception)
        {
            StringBuilder sb = new StringBuilder();

            if (exception != null)
            {
                sb.AppendLine(exception.Message);
                sb.AppendLine("--------------");

                var err = exception;

                while (err.InnerException != null)
                {
                    sb.AppendLine(err.InnerException.Message);
                    sb.AppendLine("--------------");
                    err = err.InnerException;
                }
            }

            sb.AppendLine(string.Format("{0}: {1}", exception.TargetSite.Name, exception.Message));
            sb.AppendLine("--------------");
            sb.AppendLine(string.Format("Stack Trace: {0}", exception.StackTrace));

            return sb.ToString();
        }

        #endregion //Exception Management

        #region Logging

        public bool Initialised
        {
            get { return _nLogger != null; }
        }

        public bool IsDebugEnabled
        {
            get { return IsEnabled(LogLevel.Debug); }
        }
        public bool IsFatalEnabled
        {
            get { return IsEnabled(LogLevel.Fatal); }
        }

        public bool IsErrorEnabled
        {
            get { return IsEnabled(LogLevel.Error); }
        }
        public bool IsInfoEnabled
        {
            get { return IsEnabled(LogLevel.Info); }
        }

        public bool IsTraceEnabled
        {
            get { return IsEnabled(LogLevel.Trace); }
        }

        public bool IsWarnEnabled
        {
            get { return IsEnabled(LogLevel.Warn); }
        }

        public void Debug(string message, params Object[] parameters)
        {
            WriteMessage(LogLevel.Debug, message, parameters);
        }

        public void Debug(string message, Exception exception)
        {
            WriteMessage(LogLevel.Debug, message, exception);
        }

        public void Debug(string message, Exception exception, HttpRequest httpRequest)
        {
            WriteMessage(LogLevel.Debug, message, exception, httpRequest);
        }

        public void Fatal(string message, params Object[] parameters)
        {
            WriteMessage(LogLevel.Fatal, message, parameters);
        }

        public void Fatal(string message, Exception exception)
        {
            WriteMessage(LogLevel.Fatal, message, exception);
        }

        public void Fatal(string message, Exception exception, HttpRequest httpRequest)
        {
            WriteMessage(LogLevel.Fatal, message, exception, httpRequest);
        }

        public void Error(string message, params Object[] parameters)
        {
            WriteMessage(LogLevel.Error, message, parameters);
        }

        public void Error(string message, Exception exception)
        {
            WriteMessage(LogLevel.Error, message, exception);
        }

        public void Error(string message, Exception exception, HttpRequest httpRequest)
        {
            WriteMessage(LogLevel.Error, message, exception, httpRequest);
        }

        public void Info(string message, params Object[] parameters)
        {
            WriteMessage(LogLevel.Info, message, parameters);
        }

        public void Info(string message, Exception exception)
        {
            WriteMessage(LogLevel.Info, message, exception);
        }

        public void Info(string message, Exception exception, HttpRequest httpRequest)
        {
            WriteMessage(LogLevel.Info, message, exception, httpRequest);
        }

        public void Trace(string message, params Object[] parameters)
        {
            WriteMessage(LogLevel.Trace, message, parameters);
        }

        public void Trace(string message, Exception exception)
        {
            WriteMessage(LogLevel.Trace, message, exception);
        }

        public void Trace(string message, Exception exception, HttpRequest httpRequest)
        {
            WriteMessage(LogLevel.Trace, message, exception, httpRequest);
        }

        public void Warn(string message, params Object[] parameters)
        {
            WriteMessage(LogLevel.Warn, message, parameters);
        }

        public void Warn(string message, Exception exception)
        {
            WriteMessage(LogLevel.Warn, message, exception);
        }

        public void Warn(string message, Exception exception, HttpRequest httpRequest)
        {
            WriteMessage(LogLevel.Warn, message, exception, httpRequest);
        }

        #endregion Logging

        #region Private Methods
        private void WriteMessage(LogLevel logLevel, string message, params Object[] parameters)
        {
            if (_nLogger == null) return;
            if (_nLogger.IsEnabled(logLevel))
            {
                if (parameters != null && parameters.Length > 0)
                {
                    string parametersString = BuildParameters(parameters);
                    message = string.Concat(message, "------- Params -------", parametersString);
                }

                LogEventInfo logEvent = new LogEventInfo(logLevel, _nLogger.Name, message);

                logEvent.Properties["Reference"] = Guid.NewGuid();
                logEvent.Properties["AssemblyVersion"] = CallingAssemblyVersion();
                logEvent.Properties["QueryString"] = null;
                logEvent.Properties["FormData"] = null;

                _nLogger.Log(GetType(), logEvent);
            }
        }

        private void WriteMessage(LogLevel logLevel, string message, Exception exception)
        {
            if (_nLogger == null) return;
            if (_nLogger.IsEnabled(logLevel))
            {
                LogEventInfo logEvent = new LogEventInfo(logLevel, _nLogger.Name, message);
                logEvent.Exception = exception;

                logEvent.Properties["Reference"] = Guid.NewGuid();
                logEvent.Properties["AssemblyVersion"] = CallingAssemblyVersion();
                logEvent.Properties["QueryString"] = null;
                logEvent.Properties["FormData"] = null;

                _nLogger.Log(GetType(), logEvent);
            }
        }

        private void WriteMessage(LogLevel logLevel, string message, Exception exception, HttpRequest httpRequest)
        {
            if (_nLogger == null) return;
            if (_nLogger.IsEnabled(logLevel))
            {
                LogEventInfo logEvent = new LogEventInfo(logLevel, _nLogger.Name, message);
                logEvent.Exception = exception;

                logEvent.Properties["Reference"] = Guid.NewGuid();
                logEvent.Properties["AssemblyVersion"] = CallingAssemblyVersion();

                if (httpRequest != null)
                {
                    logEvent.Properties["QueryString"] = string.Concat(httpRequest.FilePath, httpRequest.QueryString.ToString());
                    logEvent.Properties["FormData"] = httpRequest.Form.ToString();
                }
                else
                {
                    logEvent.Properties["QueryString"] = null;
                    logEvent.Properties["FormData"] = null;
                }

                _nLogger.Log(GetType(), logEvent);
            }
        }

        private string BuildParameters(params object[] parameters)
        {
            StringBuilder result = new StringBuilder();

            foreach (object parameter in parameters)
            {
                result.AppendLine(ObjectDumper.Dump(parameter));
            }

            return result.ToString();
        }

        private bool IsEnabled(LogLevel logLevel)
        {
            if (_nLogger == null) return false;
            return _nLogger.IsEnabled(logLevel);
        }

        private string CallingAssemblyVersion()
        {
            //Trace("CallingAssemblyVersion Start");

            Stopwatch stopwatch = new Stopwatch();
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetCallingAssembly().Location);
            //Trace(string.Format("CallingAssemblyVersion Ended - {0}.{1} Seconds",  stopwatch.Elapsed.Seconds, stopwatch.Elapsed.Milliseconds));

            return fileVersionInfo.ProductVersion;
        }

        #endregion //Private Methods
    }
}

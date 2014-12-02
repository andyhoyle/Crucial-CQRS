using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Net;
using System.Web;

namespace Crucial.Framework.Logging
{
    public interface ILogger
    {
        bool Initialised { get; }
        bool IsDebugEnabled { get; }
        bool IsFatalEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsTraceEnabled { get; }
        bool IsWarnEnabled { get; }

        void Debug(string message, params Object[] parameters);
        void Fatal(string message, params Object[] parameters);
        void Error(string message, params Object[] parameters);
        void Info(string message, params Object[] parameters);
        void Trace(string message, params Object[] parameters);
        void Warn(string message, params Object[] parameters);

        void LogException(string message, Exception exception, ExceptionType exceptionType = ExceptionType.Handled);
        void LogException(string message, Exception exception, HttpRequestBase httpRequestBase, ExceptionType exceptionType = ExceptionType.Handled);
        void LogException(string message, Exception exception, HttpRequest httpRequest, ExceptionType exceptionType = ExceptionType.Handled);
        void LogException(Exception exception, ExceptionType exceptionType = ExceptionType.Handled);
        void LogException(Exception exception, HttpRequestBase httpRequestBase, ExceptionType exceptionType = ExceptionType.Handled);
        void LogException(Exception exception, HttpRequest httpRequest, ExceptionType exceptionType = ExceptionType.Handled);
        void LogException(WebException webException, ExceptionType exceptionType = ExceptionType.Handled);
        void LogException(DbUpdateException dbUpdateException);
        void LogException(DbEntityValidationException dbEntityValidationException);
    }

    public enum ExceptionType
    {
        Handled = 0,
        Unhandled = 1
    }
}
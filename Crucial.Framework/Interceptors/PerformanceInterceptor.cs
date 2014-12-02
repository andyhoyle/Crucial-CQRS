using System;
using Castle.DynamicProxy;
using System.Diagnostics;
using Crucial.Framework.Logging;

namespace Crucial.Framework.Interceptors
{
    public class PerformanceInterceptor : IInterceptor
    {
        private readonly ILogger _logger;

        public PerformanceInterceptor(ILogger prometheanLogger)
        {
            _logger = prometheanLogger;
        }

        public void Intercept(IInvocation invocation)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            invocation.Proceed();
            stopWatch.Stop();

            PerformanceLog l = new PerformanceLog
            {
                Duration = stopWatch.ElapsedMilliseconds,
                Method = invocation.Method.Name,
                FullClassName = invocation.Method.ReflectedType.FullName,
                ClassName = invocation.Method.ReflectedType.Name,
                StackTrace = Environment.StackTrace
            };

            _logger.Info(String.Format("\t{0}\t{1}\t{2}\t{3}", l.Duration, l.ClassName, l.Method, l.FullClassName));
        }
    }

    public class PerformanceLog
    {
        public string Method { get; set; }
        public string ClassName { get; set; }
        public string FullClassName { get; set; }
        public string StackTrace { get; set; }
        public long Duration { get; set; }
    }
}

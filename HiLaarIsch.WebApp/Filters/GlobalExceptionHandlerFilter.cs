using System.Web.Mvc;
using QuantumHive.Core;
using QuantumHive.Core.Extensions;
using QuantumHive.Core.Logging;

namespace HiLaarIsch.Filters
{
    public sealed class GlobalExceptionHandlerFilter : IExceptionFilter
    {
        private readonly ILogger logger;

        public GlobalExceptionHandlerFilter(ILogger logger)
        {
            this.logger = logger;
        }

        public void OnException(ExceptionContext filterContext)
        {
            this.logger.Log(LoggingEventType.Error, filterContext.Exception);
            filterContext.ExceptionHandled = false;
        }
    }
}
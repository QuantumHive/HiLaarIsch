using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using QuantumHive.Core;
using QuantumHive.Core.Extensions;
using QuantumHive.Core.Logging;

namespace HiLaarIsch
{
    public class ExceptionHandlingMiddleware : IMiddleWare
    {
        private readonly ILogger logger;

        public ExceptionHandlingMiddleware(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task Invoke(IOwinContext context, Func<Task> next)
        {
            try
            {
                await next.Invoke();
            }
            catch (Exception exception)
            {
                this.logger.Log(LoggingEventType.Fatal, exception);
                throw;
            }
        }
    }
}
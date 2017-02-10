using System;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace HiLaarIsch
{
    public class ExceptionHandlingMiddleware : IMiddleWare
    {
        public ExceptionHandlingMiddleware()
        {
        }

        public async Task Invoke(IOwinContext context, Func<Task> next)
        {
            try
            {
                await next.Invoke();
            }
            catch (Exception exception)
            {
                //TODO: log exception
                throw;
            }
        }
    }
}
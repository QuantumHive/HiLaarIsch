using System.Web.Mvc;

namespace HiLaarIsch.Filters
{
    public sealed class GlobalExceptionHandlerFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;

            //TODO: log exception

            filterContext.ExceptionHandled = false;
        }
    }
}
using System.Web.Mvc;
using HiLaarIsch.Helpers;

namespace HiLaarIsch.Filters.ActionFilters.Global
{
    /// <summary>
    /// Global Action Filter that preserves <c cref="System.Web.Routing.RouteData"/> in between requests (redirect action) 
    /// Similar to the UrlReferrer, except that you get the full blown MVC RouteData instead of an <see cref="System.Uri"/>
    /// </summary>
    public class RouteValuesTransferStateAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.Controller.TempData[TempDataKeys.RouteValuesTransferState] = filterContext.RouteData;
            base.OnResultExecuted(filterContext);
        }
    }
}
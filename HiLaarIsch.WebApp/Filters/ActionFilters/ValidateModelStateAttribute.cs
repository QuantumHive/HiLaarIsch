using System;
using System.Web.Mvc;
using System.Web.Routing;
using HiLaarIsch.Filters.ActionFilters;
using HiLaarIsch.Helpers;

namespace HiLaarIsch.Filters
{
    /// <summary>
    /// Assumes the PRG (Post-Redirect-Get) pattern.
    /// Exports invalid ModelStates in between requests (redirect action).
    /// Use in conjunction with <see cref="ImportModelStateAttribute"/>, so that the model gets imported back into the redirected action (GET). 
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidateModelStateAttribute : ModelStateTransfer
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!base.IsModelStateValid(filterContext))
            {
                base.SetModelState(filterContext);
                var previousRouteData = this.GetPreviousRouteData(filterContext);
                //force redirect before executing action
                filterContext.Result = new RedirectToRouteResult(previousRouteData.Values);
            }

            base.OnActionExecuting(filterContext);
        }

        private RouteData GetPreviousRouteData(ControllerContext context)
            => (RouteData)context.Controller.TempData[TempDataKeys.RouteValuesTransferState];
    }
}
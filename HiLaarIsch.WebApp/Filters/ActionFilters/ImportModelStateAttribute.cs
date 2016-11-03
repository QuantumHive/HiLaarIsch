using System;
using System.Web.Mvc;
using HiLaarIsch.Filters.ActionFilters;

namespace HiLaarIsch.Filters
{
    /// <summary>
    /// Assumes the PRG (Post-Redirect-Get) pattern.
    /// Imports invalid ModelStates in between requests (redirect action).
    /// Use this filter if you need to import the exported invalid model from the previous request (POST).
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ImportModelStateAttribute : ModelStateTransfer
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var modelstate = this.GetModelState(filterContext) as ModelStateDictionary;
            //TODO: merging with invalid model
            if(modelstate != null)
            {
                if (this.IsViewResult(filterContext))
                {
                    this.MergeModelState(filterContext, modelstate);
                }
                else
                {
                    this.RemoveModelState(filterContext);
                }
            }

            base.OnActionExecuted(filterContext);
        }

        private bool IsViewResult(ActionExecutedContext filterContext)
            => filterContext.Result is ViewResult;
    }
}
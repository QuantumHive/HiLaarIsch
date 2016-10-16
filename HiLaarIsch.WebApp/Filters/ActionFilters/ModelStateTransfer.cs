using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// https://www.exceptionnotfound.net/the-post-redirect-get-pattern-in-asp-net-mvc/
// http://benfoster.io/blog/automatic-modelstate-validation-in-aspnet-mvc

using System.Web.Mvc;
using HiLaarIsch.Helpers;

namespace HiLaarIsch.Filters.ActionFilters
{
    public class ModelStateTransfer : ActionFilterAttribute
    {
        protected const string Key = TempDataKeys.ModelStateTransferState;

        protected object GetModelState(ControllerContext context)
            => context.Controller.TempData[ModelStateTransfer.Key];

        protected void SetModelState(ControllerContext context)
            => context.Controller.TempData[ModelStateTransfer.Key] = context.Controller.ViewData.ModelState;

        protected void RemoveModelState(ControllerContext context)
            => context.Controller.TempData.Remove(ModelStateTransfer.Key);

        protected void MergeModelState(ControllerContext context, ModelStateDictionary modelState)
            => context.Controller.ViewData.ModelState.Merge(modelState);

        protected bool IsModelStateValid(ControllerContext context)
            => context.Controller.ViewData.ModelState.IsValid;
    }
}
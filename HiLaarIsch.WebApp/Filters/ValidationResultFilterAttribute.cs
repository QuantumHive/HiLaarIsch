using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace HiLaarIsch.Filters
{
    public class ValidationResultFilter : IResultFilter
    {
        private const string Key = "validation-result";

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Items.Contains(Key))
            {
                var validationException = (ValidationException)filterContext.HttpContext.Items[Key];
                filterContext.Controller.ViewData.Add(Key, validationException.ValidationResult);
            }
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Items.Remove(Key);
            filterContext.Controller.ViewData.Remove(Key);
        }
    }
}
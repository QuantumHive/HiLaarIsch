using System;
using System.Linq;
using System.Web.Mvc;
using HiLaarIsch.Components;

namespace HiLaarIsch.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeRoleAttribute : FilterAttribute, IAuthorizationFilter
    {
        private Role role;

        public AuthorizeRoleAttribute(Role role)
        {
            this.role = role;
        }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            if (!user.IsInRole(role))
            {
                this.HandleUnauthorizedRequest(filterContext);
            }
        }

        private void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            ActionResult result = new RedirectResult("~/");

            switch (this.role)
            {
                case Role.Admin:
                    result = new RedirectResult("~/customers");
                    break;
                case Role.Customer:
                    //TODO: this will trigger an infinite loop
                    //change to the customers landing page when controller is implemented
                    result = new RedirectResult("~/customers");
                    break;
            }

            filterContext.Result = result;
        }
    }
}
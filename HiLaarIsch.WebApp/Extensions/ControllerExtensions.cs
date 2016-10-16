using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HiLaarIsch
{
    public static class ControllerExtensions
    {
        public static RedirectResult RedirectToRoot<TController>(this TController controller)
            where TController : Controller
        {
            return new RedirectResult("/");
        }
    }
}
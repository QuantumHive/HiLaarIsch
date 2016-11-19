using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace HiLaarIsch
{
    public static class HtmlPartialExtensions
    {
        public static MvcHtmlString PartialAddCommand(this HtmlHelper helper, string route)
        {
            return helper.Partial(Partials.Commands.Add, route);
        }
    }
}
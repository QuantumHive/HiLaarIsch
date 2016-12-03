using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace HiLaarIsch
{
    public static class HtmlPartialExtensions
    {
        public static IHtmlString PartialNoResults(this HtmlHelper helper)
        {
            return helper.Partial(Partials.Shared.NoResults);
        }
        public static IHtmlString PartialAddCommand(this HtmlHelper helper, string route)
        {
            return helper.Partial(Partials.Commands.Add, route);
        }

        public static IHtmlString PartialBackCommand(this HtmlHelper helper, string route)
        {
            return helper.Partial(Partials.Commands.Back, route);
        }

        public static IHtmlString PartialUnderConstruction(this HtmlHelper helper)
        {
            return helper.Partial(Partials.Shared.UnderConstruction);
        }
    }
}
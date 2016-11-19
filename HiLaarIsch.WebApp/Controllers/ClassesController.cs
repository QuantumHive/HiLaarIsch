using System.Web.Mvc;
using HiLaarIsch.Components;
using HiLaarIsch.Filters;
using QuantumHive.Core;

namespace HiLaarIsch.Controllers
{
    [Authorize]
    [AuthorizeRole(Role.Admin)]
    [RoutePrefix("classes")]
    public class ClassesController : Controller
    {
        private readonly IQueryProcessor queryProcessor;

        public ClassesController(IQueryProcessor queryProcessor)
        {
            this.queryProcessor = queryProcessor;
        }

        [HttpGet, Route]
        public ActionResult Index()
        {
            return this.View();
        }
    }
}
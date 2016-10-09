using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HiLaarIsch.Contract.Queries;
using QuantumHive.Core;

namespace HiLaarIsch.Controllers
{
    [RoutePrefix("Account")]
    public class AccountController : Controller
    {
        private readonly IQueryProcessor queryProcessor;

        public AccountController(IQueryProcessor queryProcessor)
        {
            this.queryProcessor = queryProcessor;
        }

        [Authorize]
        [Route("~/")]
        [HttpGet, Route("Users")]
        public ActionResult Index()
        {
            var users = this.queryProcessor.Process(new GetAllUsersQuery());
            return this.View(users);
        }

        [HttpGet, Route("Login")]
        public ActionResult Login()
        {
            return this.View();
        }

        [HttpPost, Route("Login")]
        public ActionResult Login(string email, string password)
        {
            return this.View();
        }
    }
}
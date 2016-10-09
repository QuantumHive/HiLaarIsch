using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HiLaarIsch.Contract.Queries;
using HiLaarIsch.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using QuantumHive.Core;

namespace HiLaarIsch.Controllers
{
    [RoutePrefix("Account")]
    public class AccountController : Controller
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly SignInManager<IdentityUser, Guid> signInManager;
        private readonly UserManager<IdentityUser, Guid> userManager;

        public AccountController(
            IQueryProcessor queryProcessor,
            SignInManager<IdentityUser, Guid> signInManager,
            UserManager<IdentityUser, Guid> userManager)
        {
            this.queryProcessor = queryProcessor;
            this.signInManager = signInManager;
            this.userManager = userManager;
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
            var user = this.userManager.FindByEmail(email);
            if(user == null)
            {
                //email does not exist
                return this.View();
            }

            if(!this.userManager.CheckPassword(user, password))
            {
                //wrong password
                return this.View();
            }

            this.signInManager.SignIn(user, isPersistent: false, rememberBrowser: false);

            return this.Redirect("/"); //TODO: clean redirect
        }
    }
}
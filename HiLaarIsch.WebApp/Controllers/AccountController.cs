using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HiLaarIsch.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using QuantumHive.Core;

namespace HiLaarIsch.Controllers
{
    [RoutePrefix("account")]
    public class AccountController : Controller
    {
        private readonly IAuthenticationManager authenticationManager;
        private readonly SignInManager<IdentityUser, Guid> signInManager;
        private readonly UserManager<IdentityUser, Guid> userManager;

        public AccountController(
            IAuthenticationManager authenticationManager,
            SignInManager<IdentityUser, Guid> signInManager,
            UserManager<IdentityUser, Guid> userManager)
        {
            this.authenticationManager = authenticationManager;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpGet, Route("login")]
        public ActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.Redirect("/"); //TODO: clean redirect
            }
            return this.View();
        }

        [HttpPost, Route("login")]
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

        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Logoff()
        {
            this.authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return this.Redirect("account/login");
        }
    }
}
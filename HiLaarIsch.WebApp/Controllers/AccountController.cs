using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HiLaarIsch.Filters;
using HiLaarIsch.Identity;
using HiLaarIsch.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using QuantumHive.Core;

namespace HiLaarIsch.Controllers
{
    [RoutePrefix("account")]
    public class AccountController : Controller
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly IAuthenticationManager authenticationManager;
        private readonly SignInManager<IdentityUser, Guid> signInManager;
        private readonly UserManager<IdentityUser, Guid> userManager;

        public AccountController(
            IQueryProcessor queryProcessor,
            IAuthenticationManager authenticationManager,
            SignInManager<IdentityUser, Guid> signInManager,
            UserManager<IdentityUser, Guid> userManager)
        {
            this.queryProcessor = queryProcessor;
            this.authenticationManager = authenticationManager;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpGet, Route("login")]
        public ActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToRoot();
            }
            return this.View();
        }

        [HttpPost, Route("login")]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            //TODO email not confirmed
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

            return this.RedirectToRoot();
        }

        [Authorize]
        [HttpGet, Route("logoff")]
        [ValidateAntiForgeryToken]
        public ActionResult Logoff()
        {
            this.authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return this.RedirectToRoot();
        }

        [HttpGet, Route("confirm/{userid}/{mailtoken}")]
        [ImportModelState]
        public ActionResult Confirm(Guid userid, string mailtoken)
        {
            //TODO: prefer extension method on the usermanager somehow for userexists
            if (!string.IsNullOrWhiteSpace(mailtoken)
                && this.userManager.VerifyUserToken(userid, "Confirmation", mailtoken))
            {
                var model = new ResetPasswordViewModel
                {
                    UserId = userid,
                    MailToken = mailtoken,
                };
                return this.View(model);
            }

            return this.RedirectToRoot();
        }

        [HttpPost, Route("confirm")]
        [ValidateModelState]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm(ResetPasswordViewModel model)
        {
            var result = this.userManager.ConfirmEmail(model.UserId, model.MailToken);

            if (result.Succeeded)
            {
                //TODO: passwordvalidator test for space
                var passwordToken = this.userManager.GeneratePasswordResetToken(model.UserId);
                result = this.userManager.ResetPassword(model.UserId, passwordToken, model.NewPassword);

                if (result.Succeeded)
                {
                    //TODO: signin
                }
            }

            return this.RedirectToRoot();
        }
    }
}
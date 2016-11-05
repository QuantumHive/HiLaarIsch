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
        private readonly SignInManager signInManager;
        private readonly UserManager userManager;

        public AccountController(
            IQueryProcessor queryProcessor,
            IAuthenticationManager authenticationManager,
            SignInManager signInManager,
            UserManager userManager)
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
            var user = this.userManager.FindByEmail(email);
            if(user == null || !user.EmailConfirmed)
            {
                return this.View();
            }

            if(!this.userManager.CheckPassword(user.Id, password))
            {
                return this.View();
            }

            this.signInManager.SignIn(user);

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
            if (this.userManager.VerifyEmailToken(userid, mailtoken))
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
            if (this.userManager.VerifyEmailToken(model.UserId, model.MailToken))
            {
                this.userManager.ConfirmEmail(model.UserId, model.MailToken);

                //TODO: passwordvalidator
                this.userManager.ResetPassword(model.UserId, model.NewPassword);

                //TODO: signin
            }

            return this.RedirectToRoot();
        }
    }
}
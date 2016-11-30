using System;
using System.Web.Mvc;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Filters;
using HiLaarIsch.Services;
using HiLaarIsch.Models;
using QuantumHive.Core;

namespace HiLaarIsch.Controllers
{
    [RoutePrefix("account")]
    public class AccountController : Controller
    {
        private readonly IAuthenticationManager<UserView> authenticationManager;
        private readonly UserManager userManager;

        public AccountController(
            IAuthenticationManager<UserView> authenticationManager,
            UserManager userManager)
        {
            this.authenticationManager = authenticationManager;
            this.userManager = userManager;
        }

        [HttpGet, Route("login")]
        public ActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return this.RedirectToRoot();
            }
            return this.View(model: string.Empty);
        }

        [HttpPost, Route("login")]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            var user = this.userManager.FindByEmail(email);
            if(user == null || !user.EmailConfirmed)
            {
                
                return this.View(model: email);
            }

            if(!this.userManager.CheckPassword(user.Id, password))
            {
                return this.View(model: email);
            }

            this.authenticationManager.SignIn(user);

            return this.RedirectToRoot();
        }

        [Authorize]
        [HttpPost, Route("logoff")]
        [ValidateAntiForgeryToken]
        public ActionResult Logoff()
        {
            this.authenticationManager.SignOut();
            return this.RedirectToRoot();
        }

        [HttpGet, Route("confirm")]
        public ActionResult Confirm(int userId, string mailToken)
        {
            if (this.ValidateUserForConfirmation(userId) &&
                this.userManager.VerifyEmailToken(userId, mailToken))
            {
                var model = new ResetPasswordViewModel
                {
                    UserId = userId,
                    MailToken = mailToken,
                };
                return this.View(model);
            }

            return this.RedirectToRoot();
        }

        [HttpPost, Route("confirm")]
        [ValidateAntiForgeryToken]
        public ActionResult Confirm(ResetPasswordViewModel model)
        {
            if (this.ModelState.IsValid &&
                this.ValidateUserForConfirmation(model.UserId) &&
                this.userManager.VerifyEmailToken(model.UserId, model.MailToken))
            {
                this.userManager.ConfirmEmail(model.UserId);
                //TODO: passwordvalidator
                this.userManager.ResetPassword(model.UserId, model.NewPassword);
                return this.RedirectToRoot();
            }
            return this.View(model);
        }

        private bool ValidateUserForConfirmation(int userId)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return false;
            }

            var user = this.userManager.GetById(userId);
            if(user == null || user.EmailConfirmed)
            {
                return false;
            }

            return true;
        }
    }
}
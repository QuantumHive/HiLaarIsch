using System;
using System.Net.Mail;
using System.Web.Mvc;
using HiLaarIsch.Components;
using HiLaarIsch.Contract.Commands;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Contract.Queries;
using HiLaarIsch.Filters;
using HiLaarIsch.Services;
using QuantumHive.Core;

namespace HiLaarIsch.Controllers
{
    [Authorize]
    [AuthorizeRole(Role.Admin)]
    [RoutePrefix("customers")]
    public class CustomersController : Controller
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly ICommandHandler<CreateModelCommand<CustomerModel>> createHandler;
        private readonly ICommandHandler<UpdateModelCommand<CustomerModel>> updateHandler;
        private readonly UserManager userManager;

        public CustomersController(
            IQueryProcessor queryProcessor,
            ICommandHandler<CreateModelCommand<CustomerModel>> createHandler,
            ICommandHandler<UpdateModelCommand<CustomerModel>> updateHandler,
            UserManager userManager)
        {
            this.queryProcessor = queryProcessor;
            this.createHandler = createHandler;
            this.updateHandler = updateHandler;
            this.userManager = userManager;
        }
        
        [Route("~/")]
        [HttpGet, Route]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet, Route("list")]
        public ActionResult List()
        {
            var customers = this.queryProcessor.Process(new GetAllModelsQuery<CustomerView>());
            return this.PartialView(customers);
        }

        [HttpGet, Route("new")]
        public ActionResult New()
        {
            return this.View(new CustomerModel());
        }

        [HttpPost, Route("new")]
        [ValidateAntiForgeryToken]
        public ActionResult New(CustomerModel model)
        {
            //TODO: refactor to validator class
            //TODO: check if email exists
            var valid = true;
            try
            {
                var m = new MailAddress(model.Email);
            }
            catch (FormatException)
            {
                valid = false;
            }

            if (valid && ModelState.IsValid)
            {
                this.userManager.Create(model.Email);
                this.createHandler.Handle(new CreateModelCommand<CustomerModel>(model));

                var user = this.userManager.FindByEmail(model.Email);
                var mailToken = this.userManager.GenerateEmailConfirmationToken(user.Id);

                var confirmationUrl = this.Url.Action("confirm", "account", new { userId = user.Id, mailToken = mailToken }, this.Request.Url.Scheme);

                var body =
$@"Beste {model.Firstname},

Ten behoeve van Stal van Laar (Manage Arnhem) is er een account voor je aangemaakt.
Door op de onderstaande activatielink te klikken kun je je account activeren, eenmalig een wachtwoord instellen.
{confirmationUrl}

De activatielink vervalt na 3 dagen.

Dit bericht is verstuurd door een automatisch systeem en antwoorden op deze mail worden doorgaans genegeerd.";

                this.userManager.SendEmail(user.Email, "HiLaarIsch account activatie", body);

                return this.Redirect("/customers");
            }

            return this.View(model);
        }

        [HttpGet, Route("edit/{customerId}")]
        public ActionResult Edit(int customerId)
        {
            var model = this.queryProcessor.Process(new GetModelByIdQuery<CustomerModel>(customerId));
            return this.View(model);
        }

        [HttpPost, Route("edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                //TODO: notify user when email changed?
                this.updateHandler.Handle(new UpdateModelCommand<CustomerModel>(model, model.Id));
                return this.Redirect("/customers");
            }
            return this.View(model);
        }
    }
}
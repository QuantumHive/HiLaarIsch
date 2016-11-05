﻿using System;
using System.Net.Mail;
using System.Web.Mvc;
using HiLaarIsch.Contract.Commands;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Contract.Queries;
using HiLaarIsch.Filters;
using HiLaarIsch.Services;
using QuantumHive.Core;

namespace HiLaarIsch.Controllers
{
    [Authorize]
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
            var customers = this.queryProcessor.Process(new GetAllModelsQuery<CustomerView>());
            return this.View(customers);
        }

        [HttpGet, Route("new")]
        [ImportModelState]
        public ActionResult New()
        {
            return this.View(new CustomerModel());
        }

        [HttpPost, Route("new")]
        [ValidateModelState]
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

            if (valid)
            {
                this.userManager.CreateUser(model.Email);
                this.createHandler.Handle(new CreateModelCommand<CustomerModel>(model));

                var user = this.userManager.FindByEmail(model.Email);
                var mailToken = this.userManager.GenerateEmailConfirmationToken(user.Id);

                //TODO: send email

                return this.Redirect("/customers");
            }

            return this.Redirect("/new");
        }

        [HttpGet, Route("edit/{customerId}")]
        [ImportModelState]
        public ActionResult Edit(Guid customerId)
        {
            var model = this.queryProcessor.Process(new GetModelByIdQuery<CustomerModel>(customerId));
            return this.View(model);
        }

        [HttpPost, Route("edit")]
        [ValidateModelState]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerModel model)
        {
            //TODO: notify user when email changed?
            this.updateHandler.Handle(new UpdateModelCommand<CustomerModel>(model, model.Id));
            return this.Redirect("/customers");
        }
    }
}
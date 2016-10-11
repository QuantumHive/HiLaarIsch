using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HiLaarIsch.Contract.Commands;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Contract.Queries;
using QuantumHive.Core;

namespace HiLaarIsch.Controllers
{
    [Authorize]
    [RoutePrefix("customers")]
    public class CustomersController : Controller
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly ICommandHandler<CreateModelCommand<CustomerModel>> createHandler;

        public CustomersController(
            IQueryProcessor queryProcessor,
            ICommandHandler<CreateModelCommand<CustomerModel>> createHandler)
        {
            this.queryProcessor = queryProcessor;
            this.createHandler = createHandler;
        }
        
        [Route("~/")]
        [HttpGet, Route]
        public ActionResult Index()
        {
            var customers = this.queryProcessor.Process(new GetAllModelsQuery<CustomerView>());
            return this.View(customers);
        }

        [HttpGet, Route("new")]
        public ActionResult New()
        {
            return this.View();
        }

        [HttpPost, Route("new")]
        [ValidateAntiForgeryToken]
        public ActionResult New(CustomerModel model)
        {
            this.createHandler.Handle(new CreateModelCommand<CustomerModel>(model));
            return this.Redirect("/"); //TODO: clean redirect
        }
    }
}
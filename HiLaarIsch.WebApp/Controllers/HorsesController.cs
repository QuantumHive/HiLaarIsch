﻿using System.Web.Mvc;
using HiLaarIsch.Components;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Contract.Queries;
using HiLaarIsch.Filters;
using QuantumHive.Core;

namespace HiLaarIsch.Controllers
{
    [Authorize]
    [AuthorizeRole(Role.Admin)]
    [RoutePrefix("horses")]
    public class HorsesController : Controller
    {
        private readonly IQueryProcessor queryProcessor;

        public HorsesController(IQueryProcessor queryProcessor)
        {
            this.queryProcessor = queryProcessor;
        }

        [HttpGet, Route]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet, Route("list")]
        public ActionResult List()
        {
            var model = this.queryProcessor.Process(new GetAllModelsQuery<HorseView>());
            return this.PartialView(model);
        }
    }
}
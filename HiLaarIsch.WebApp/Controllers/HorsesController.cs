using System.Web.Mvc;
using HiLaarIsch.Components;
using HiLaarIsch.Contract.Commands;
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
        private readonly ICommandHandler<CreateModelCommand<HorseModel>> createHandler;
        private readonly ICommandHandler<UpdateModelCommand<HorseModel>> updateHandler;

        public HorsesController(IQueryProcessor queryProcessor,
            ICommandHandler<CreateModelCommand<HorseModel>> createHandler,
            ICommandHandler<UpdateModelCommand<HorseModel>> updateHandler)
        {
            this.queryProcessor = queryProcessor;
            this.createHandler = createHandler;
            this.updateHandler = updateHandler;
        }

        [HttpGet, Route]
        public ActionResult Index()
        {
            return this.View();
        }

        [HttpGet, Route("list")]
        public ActionResult List()
        {
            var horses = this.queryProcessor.Process(new GetAllModelsQuery<HorseView>());
            return this.PartialView(horses);
        }

        [HttpGet, Route("new")]
        public ActionResult New()
        {
            return this.View(new HorseModel());
        }

        [HttpPost, Route("new")]
        [ValidateAntiForgeryToken]
        public ActionResult New(HorseModel horse)
        {
            if (ModelState.IsValid)
            {
                this.createHandler.Handle(new CreateModelCommand<HorseModel>(horse));
                return this.Redirect("/horses");
            }
            return this.View(horse);
        }

        [HttpGet, Route("edit/{horseId}")]
        public ActionResult Edit(int horseId)
        {
            var horse = this.queryProcessor.Process(new GetModelByIdQuery<HorseModel>(horseId));
            return this.View(horse);
        }

        [HttpPost, Route("edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HorseModel horse)
        {
            if (ModelState.IsValid)
            {
                this.updateHandler.Handle(new UpdateModelCommand<HorseModel>(horse, horse.Id));
                return this.Redirect("/horses");
            }
            return this.View(horse);
        }
    }
}
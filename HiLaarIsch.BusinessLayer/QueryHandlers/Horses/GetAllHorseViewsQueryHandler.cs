using System.Linq;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Contract.Queries;
using HiLaarIsch.Domain;
using QuantumHive.Core;

namespace HiLaarIsch.BusinessLayer.QueryHandlers.Horses
{
    public class GetAllHorseViewsQueryHandler : IQueryHandler<GetAllModelsQuery<HorseView>, HorseView[]>
    {
        private readonly IRepository<HorseEntity> horseRepository;

        public GetAllHorseViewsQueryHandler(IRepository<HorseEntity> horseRepository)
        {
            this.horseRepository = horseRepository;
        }

        public HorseView[] Handle(GetAllModelsQuery<HorseView> query)
        {
            var horses =
                from horse in this.horseRepository.Entities
                select new HorseView
                {
                    Id = horse.Id,
                    Name = horse.Name,
                };

            return horses.ToArray();
        }
    }
}

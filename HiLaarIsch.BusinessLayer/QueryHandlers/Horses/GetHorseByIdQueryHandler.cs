using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Contract.Queries;
using HiLaarIsch.Domain;
using QuantumHive.Core;

namespace HiLaarIsch.BusinessLayer.QueryHandlers.Horses
{
    public class GetHorseByIdQueryHandler : IQueryHandler<GetModelByIdQuery<HorseModel>, HorseModel>
    {
        private readonly IRepository<HorseEntity> horseRepository;

        public GetHorseByIdQueryHandler(IRepository<HorseEntity> horseRepository)
        {
            this.horseRepository = horseRepository;
        }

        public HorseModel Handle(GetModelByIdQuery<HorseModel> query)
        {
            var horse = this.horseRepository.GetById(query.Id);
            return new HorseModel
            {
                Id = horse.Id,
                Name = horse.Name,
            };
        }
    }
}

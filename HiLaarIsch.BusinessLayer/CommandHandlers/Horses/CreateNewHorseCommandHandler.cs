using HiLaarIsch.Contract.Commands;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Domain;
using QuantumHive.Core;

namespace HiLaarIsch.BusinessLayer.CommandHandlers.Horses
{
    public class CreateNewHorseCommandHandler : ICommandHandler<CreateModelCommand<HorseModel>>
    {
        private readonly IRepository<HorseEntity> horseRepository;

        public CreateNewHorseCommandHandler(
            IRepository<HorseEntity> horseRepository)
        {
            this.horseRepository = horseRepository;
        }

        public void Handle(CreateModelCommand<HorseModel> command)
        {
            var horse = new HorseEntity
            {
                Name = command.Model.Name,
            };

            this.horseRepository.Add(horse);
        }
    }
}

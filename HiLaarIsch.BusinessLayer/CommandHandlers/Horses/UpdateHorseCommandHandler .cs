using HiLaarIsch.Contract.Commands;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Domain;
using QuantumHive.Core;

namespace HiLaarIsch.BusinessLayer.CommandHandlers.Horses
{
    public class UpdateHorseCommandHandler : ICommandHandler<UpdateModelCommand<HorseModel>>
    {
        private readonly IRepository<HorseEntity> horseRepository;

        public UpdateHorseCommandHandler(
            IRepository<HorseEntity> horseRepository)
        {
            this.horseRepository = horseRepository;
        }

        public void Handle(UpdateModelCommand<HorseModel> command)
        {
            var horse = this.horseRepository.GetById(command.Id);
            horse.Name = command.Model.Name;
        }
    }
}

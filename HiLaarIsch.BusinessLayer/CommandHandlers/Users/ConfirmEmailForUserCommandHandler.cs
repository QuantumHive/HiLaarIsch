using HiLaarIsch.Contract.Commands;
using HiLaarIsch.Domain;
using QuantumHive.Core;

namespace HiLaarIsch.BusinessLayer.CommandHandlers.Users
{
    public class ConfirmEmailForUserCommandHandler : ICommandHandler<ConfirmEmailForUserCommand>
    {
        private readonly IRepository<UserEntity> userRepository;

        public ConfirmEmailForUserCommandHandler(
            IRepository<UserEntity> userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Handle(ConfirmEmailForUserCommand command)
        {
            var user = this.userRepository.GetById(command.UserId);
            user.EmailConfirmed = true;
        }
    }
}

using HiLaarIsch.Contract.Commands;
using HiLaarIsch.Domain;
using QuantumHive.Core;

namespace HiLaarIsch.BusinessLayer.CommandHandlers.Users
{
    public class SetPasswordHashForUserCommandHandler : ICommandHandler<SetPasswordHashForUserCommand>
    {
        private readonly IRepository<UserEntity> userRepository;

        public SetPasswordHashForUserCommandHandler(
            IRepository<UserEntity> userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Handle(SetPasswordHashForUserCommand command)
        {
            var user = this.userRepository.GetById(command.UserId);
            user.PasswordHash = command.PasswordHash;
        }
    }
}

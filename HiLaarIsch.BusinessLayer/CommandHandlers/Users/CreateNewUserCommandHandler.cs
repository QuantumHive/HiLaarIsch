using System;
using HiLaarIsch.Contract.Commands;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Domain;
using QuantumHive.Core;

namespace HiLaarIsch.BusinessLayer.CommandHandlers.Users
{
    public class CreateNewUserCommandHandler : ICommandHandler<CreateModelCommand<UserModel>>
    {
        private readonly IRepository<UserEntity> userRepository;

        public CreateNewUserCommandHandler(
            IRepository<UserEntity> userRepository)
        {
            this.userRepository = userRepository;
        }

        public void Handle(CreateModelCommand<UserModel> command)
        {
            var user = UserEntity.CreateNewUser(command.Model.Email);
            this.userRepository.Add(user);
        }
    }
}

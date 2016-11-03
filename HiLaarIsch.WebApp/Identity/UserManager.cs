using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HiLaarIsch.Contract.Commands;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Contract.Queries;
using Microsoft.AspNet.Identity;
using QuantumHive.Core;

namespace HiLaarIsch.Identity
{
    public class UserManager
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly IPasswordHasher passwordHasher;
        private readonly ICommandHandler<CreateModelCommand<UserModel>> createUserCommand;

        public UserManager(
            IQueryProcessor queryProcessor,
            IPasswordHasher passwordHasher,
            ICommandHandler<CreateModelCommand<UserModel>> createUserCommand)
        {
            this.queryProcessor = queryProcessor;
            this.passwordHasher = passwordHasher;
            this.createUserCommand = createUserCommand;
        }

        public HilaarischUser FindByEmail(string email)
        {
            var user = this.queryProcessor.Process(new GetUserByEmailQuery(email, throwIfNotExists: false));
            return user == null ? null : this.MapUser(user);
        }

        public bool CheckPassword(HilaarischUser user, string password)
        {
            var hash = this.queryProcessor.Process(new GetPasswordHashByUserIdQuery(user.Id));
            return this.passwordHasher.VerifyHashedPassword(hash, password) != PasswordVerificationResult.Failed;
        }

        public void CreateUser(string email)
        {
            var model = new UserModel { Email = email };
            this.createUserCommand.Handle(new CreateModelCommand<UserModel>(model));
        }

        private HilaarischUser MapUser(UserView source)
        {
            return new HilaarischUser
            {
                Id = source.Id,
                Email = source.Email,
            };
        }
    }
}
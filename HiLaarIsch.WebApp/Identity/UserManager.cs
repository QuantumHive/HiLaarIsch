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
        private const string EmailConfirmationPurpose = nameof(UserManager.EmailConfirmationPurpose);

        private readonly IQueryProcessor queryProcessor;
        private readonly IPasswordHasher passwordHasher;
        private readonly ICommandHandler<CreateModelCommand<UserModel>> createUserCommand;
        private readonly DataProtectorTokenProvider userTokenProvider;

        public UserManager(
            IQueryProcessor queryProcessor,
            IPasswordHasher passwordHasher,
            ICommandHandler<CreateModelCommand<UserModel>> createUserCommand,
            DataProtectorTokenProvider userTokenProvider)
        {
            this.queryProcessor = queryProcessor;
            this.passwordHasher = passwordHasher;
            this.createUserCommand = createUserCommand;
            this.userTokenProvider = userTokenProvider;
        }

        public UserView FindByEmail(string email)
        {
            return this.queryProcessor.Process(new GetUserByEmailQuery(email, throwIfNotExists: false));
        }

        public bool CheckPassword(Guid userId, string password)
        {
            var hash = this.queryProcessor.Process(new GetPasswordHashByUserIdQuery(userId));
            return this.passwordHasher.VerifyHashedPassword(hash, password) != PasswordVerificationResult.Failed;
        }

        public void CreateUser(string email)
        {
            var model = new UserModel { Email = email };
            this.createUserCommand.Handle(new CreateModelCommand<UserModel>(model));
        }

        public string GenerateEmailConfirmationToken(Guid userId)
        {
            return this.userTokenProvider.Generate(EmailConfirmationPurpose, userId);
        }

        public void ConfirmEmail(Guid userId, string token)
        {
            //TODO: commandhandler that confirms email
            throw new NotImplementedException();
        }

        public bool VerifyEmailToken(Guid userId, string token)
        {
            return this.userTokenProvider.Validate(EmailConfirmationPurpose, token, userId);
        }

        public void ResetPassword(Guid userId, string password)
        {
            var hash = this.passwordHasher.HashPassword(password);
            //TODO: commandhandler to set passwordhash
            throw new NotImplementedException();
        }
    }
}
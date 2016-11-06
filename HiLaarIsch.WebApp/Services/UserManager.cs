using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HiLaarIsch.Contract.Commands;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Contract.Queries;
using Microsoft.AspNet.Identity;
using QuantumHive.Core;

namespace HiLaarIsch.Services
{
    public class UserManager
    {
        private const string EmailConfirmationPurpose = nameof(UserManager.EmailConfirmationPurpose);

        private readonly IQueryProcessor queryProcessor;
        private readonly IPasswordHasher passwordHasher;
        private readonly DataProtectorTokenProvider userTokenProvider;
        private readonly IMessageService messageService;
        private readonly CommandHandlers commandHandlers;

        public UserManager(
            IQueryProcessor queryProcessor,
            IPasswordHasher passwordHasher,
            DataProtectorTokenProvider userTokenProvider,
            IMessageService messageService,
            CommandHandlers commandHandlers)
        {
            this.queryProcessor = queryProcessor;
            this.passwordHasher = passwordHasher;
            this.userTokenProvider = userTokenProvider;
            this.messageService = messageService;
            this.commandHandlers = commandHandlers;
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
            this.commandHandlers.CreateUser.Handle(new CreateModelCommand<UserModel>(model));
        }

        public string GenerateEmailConfirmationToken(Guid userId)
        {
            return this.userTokenProvider.Generate(EmailConfirmationPurpose, userId);
        }

        public void ConfirmEmail(Guid userId)
        {
            this.commandHandlers.ConfirmEmail.Handle(new ConfirmEmailForUserCommand(userId));
        }

        public bool VerifyEmailToken(Guid userId, string token)
        {
            return this.userTokenProvider.Validate(EmailConfirmationPurpose, token, userId);
        }

        public void ResetPassword(Guid userId, string password)
        {
            var hash = this.passwordHasher.HashPassword(password);
            this.commandHandlers.SetPasswordHash.Handle(new SetPasswordHashForUserCommand(userId, hash));
        }

        public void SendEmail(string email, string subject, string body)
        {
            var message = new Message
            {
                Destination = email,
                Subject = subject,
                Body = body,
            };
            this.messageService.SendMessage(message);
        }

        public class CommandHandlers
        {
            public readonly ICommandHandler<CreateModelCommand<UserModel>> CreateUser;
            public readonly ICommandHandler<ConfirmEmailForUserCommand> ConfirmEmail;
            public readonly ICommandHandler<SetPasswordHashForUserCommand> SetPasswordHash;

            public CommandHandlers(
                ICommandHandler<CreateModelCommand<UserModel>> createUserCommandHandler,
                ICommandHandler<ConfirmEmailForUserCommand> confirmEmailCommandHandler,
                ICommandHandler<SetPasswordHashForUserCommand> setPasswordHashCommandHandler)
            {
                this.CreateUser = createUserCommandHandler;
                this.ConfirmEmail = confirmEmailCommandHandler;
                this.SetPasswordHash = setPasswordHashCommandHandler;
            }
        }
    }
}
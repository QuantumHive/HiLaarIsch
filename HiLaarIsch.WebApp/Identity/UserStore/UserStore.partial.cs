using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HiLaarIsch.Contract.Commands;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Contract.Queries;
using Microsoft.AspNet.Identity;
using QuantumHive.Core;

namespace HiLaarIsch.Identity
{
    public partial class UserStore : IUserStore<IdentityUser, Guid>
    {
        private readonly IQueryProcessor queryProcessor;
        private readonly ICommandHandler<CreateModelCommand<UserModel>> createUserCommand;

        public UserStore(
            IQueryProcessor queryProcessor,
            ICommandHandler<CreateModelCommand<UserModel>> createUserCommand)
        {
            this.queryProcessor = queryProcessor;
            this.createUserCommand = createUserCommand;
        }

        public Task<IdentityUser> FindByIdAsync(Guid userId)
        {
            var user = this.queryProcessor.Process(new GetModelByIdQuery<UserView>(userId));
            return Task.FromResult(new IdentityUser(user.Id, user.Email));
        }

        // Same as IUserEmailStore.FindByEmailAsync(string email).
        // Since this application doesn't support usernames, the username is actually the user's email.
        // Param userName is user's email
        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            var emailStore = this as IUserEmailStore<IdentityUser, Guid>;
            return emailStore.FindByEmailAsync(userName);
        }

        public Task CreateAsync(IdentityUser user)
        {
            var model = new UserModel { Email = user.UserName };
            this.createUserCommand.Handle(new CreateModelCommand<UserModel>(model));
            return Task.CompletedTask;
        }

        public Task UpdateAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            //nothing to dispose thanks to our great architecture!
        }
    }
}
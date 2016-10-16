using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Contract.Queries;
using Microsoft.AspNet.Identity;
using QuantumHive.Core;

namespace HiLaarIsch.Identity
{
    public partial class UserStore : IUserEmailStore<IdentityUser, Guid>
    {
        // Returns null if the user with the associated email does not exists.
        public Task<IdentityUser> FindByEmailAsync(string email)
        {
            var user = this.queryProcessor.Process(new GetUserByEmailQuery(email, throwIfNotExists: false));
            return Task.FromResult(user == null ? null : new IdentityUser(user.Id, email));
        }

        public Task<string> GetEmailAsync(IdentityUser user)
        {
            var email = string.Empty;

            //if user id is empty, a new user needs to be created
            if(user.Id == Guid.Empty)
            {
                //we will return the email provided, because Identity validates on non-empty email on creation
                email = user.UserName;
            }
            else
            {
                //[assumption] for all other calls, we actually need to retrieve the user
                var result = this.queryProcessor.Process(new GetModelByIdQuery<UserView>(user.Id));
                email = result.Email;
            }

            return Task.FromResult(email);
        }

        public Task<bool> GetEmailConfirmedAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailAsync(IdentityUser user, string email)
        {
            throw new NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed)
        {
            throw new NotImplementedException();
        }
    }
}
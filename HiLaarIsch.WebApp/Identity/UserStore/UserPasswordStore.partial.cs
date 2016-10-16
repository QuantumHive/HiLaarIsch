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
    public partial class UserStore : IUserPasswordStore<IdentityUser, Guid>
    {
        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            var hash = this.queryProcessor.Process(new GetPasswordHashByUserIdQuery(user.Id));
            return Task.FromResult(hash);
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            //TODO: next
            throw new NotImplementedException();
        }
    }
}
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
    public partial class UserStore : IUserStore<IdentityUser, Guid>
    {
        private readonly IQueryProcessor queryProcessor;

        public UserStore(IQueryProcessor queryProcessor)
        {
            this.queryProcessor = queryProcessor;
        }

        public Task<IdentityUser> FindByIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(IdentityUser user)
        {
            throw new NotImplementedException();
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

        private Task<TResult> StartNewTask<TResult>(Func<TResult> function)
            => Task<TResult>.Factory.StartNew(function);
    }
}
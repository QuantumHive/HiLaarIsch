using HiLaarIsch.Contract.DTOs;
using QuantumHive.Core;

namespace HiLaarIsch.Contract.Queries
{
    public class GetUserByEmailQuery : IQuery<UserView>
    {
        public GetUserByEmailQuery(string email, bool throwIfNotExists)
        {
            this.Email = email;
            this.ThrowIfNotExistis = throwIfNotExists;
        }

        public string Email { get; }
        public bool ThrowIfNotExistis { get; }
    }
}

using HiLaarIsch.Contract.DTOs;
using QuantumHive.Core;

namespace HiLaarIsch.Contract.Queries
{
    public class GetUserByEmailQuery : IQuery<UserView>
    {
        public GetUserByEmailQuery(string email)
        {
            this.Email = email;
        }

        public string Email { get; }
    }
}

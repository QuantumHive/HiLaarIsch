using System;
using QuantumHive.Core;

namespace HiLaarIsch.Contract.Queries
{
    public class UserExistsQuery : IQuery<bool>
    {
        public UserExistsQuery(Guid userId)
        {
            this.UserId = userId;
        }

        public Guid UserId { get; }
    }
}

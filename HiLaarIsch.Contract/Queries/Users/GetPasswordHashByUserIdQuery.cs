using System;
using QuantumHive.Core;

namespace HiLaarIsch.Contract.Queries
{
    public class GetPasswordHashByUserIdQuery : IQuery<string>
    {
        public GetPasswordHashByUserIdQuery(Guid userId)
        {
            this.UserId = userId;
        }

        public Guid UserId { get; }
    }
}

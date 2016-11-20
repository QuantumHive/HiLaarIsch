using QuantumHive.Core;

namespace HiLaarIsch.Contract.Queries
{
    public class GetPasswordHashByUserIdQuery : IQuery<string>
    {
        public GetPasswordHashByUserIdQuery(int userId)
        {
            this.UserId = userId;
        }

        public int UserId { get; }
    }
}

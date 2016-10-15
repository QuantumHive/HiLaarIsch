using HiLaarIsch.Contract.Queries;
using HiLaarIsch.Domain;
using QuantumHive.Core;

namespace HiLaarIsch.BusinessLayer.QueryHandlers.Users
{
    public class GetPasswordHashByUserIdQueryHandler :
        IQueryHandler<GetPasswordHashByUserIdQuery, string>
    {
        private readonly IRepository<UserEntity> userRepository;

        public GetPasswordHashByUserIdQueryHandler(
            IRepository<UserEntity> userRepository)
        {
            this.userRepository = userRepository;
        }

        public string Handle(GetPasswordHashByUserIdQuery query)
        {
            var user = this.userRepository.GetById(query.UserId);
            return user.PasswordHash;
        }
    }
}

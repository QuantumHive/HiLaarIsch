using System.Linq;
using HiLaarIsch.Contract.Queries;
using HiLaarIsch.Domain;
using QuantumHive.Core;

namespace HiLaarIsch.BusinessLayer.QueryHandlers.Users
{
    public class UserExistsQueryHandler : IQueryHandler<UserExistsQuery, bool>
    {
        private readonly IRepository<UserEntity> userRepository;

        public UserExistsQueryHandler(
            IRepository<UserEntity> userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool Handle(UserExistsQuery query)
        {
            var userExists =
                from user in this.userRepository.Entities
                where user.Id == query.UserId
                select true;

            return userExists.SingleOrDefault();
        }
    }
}

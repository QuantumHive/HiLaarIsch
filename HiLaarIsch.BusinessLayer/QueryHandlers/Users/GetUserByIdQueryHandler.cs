using System.Linq;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Contract.Queries;
using HiLaarIsch.Domain;
using QuantumHive.Core;

namespace HiLaarIsch.BusinessLayer.QueryHandlers.Users
{
    public class GetUserByIdQueryHandler : IQueryHandler<GetModelByIdQuery<UserView>, UserView>
    {
        private readonly IRepository<UserEntity> userRepository;

        public GetUserByIdQueryHandler(
            IRepository<UserEntity> userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserView Handle(GetModelByIdQuery<UserView> query)
        {
            var users =
                from user in this.userRepository.Entities
                where user.Id == query.Id
                select user;

            var result = query.ThrowIfNotExistis ? users.Single() : users.SingleOrDefault();
            return result == null ? null : result.Map();
        }
    }
}

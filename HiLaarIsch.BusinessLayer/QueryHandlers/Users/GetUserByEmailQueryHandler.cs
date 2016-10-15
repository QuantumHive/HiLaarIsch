using System.Linq;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Contract.Queries;
using HiLaarIsch.Domain;
using QuantumHive.Core;

namespace HiLaarIsch.BusinessLayer.QueryHandlers.Users
{
    public class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailQuery, UserView>
    {
        private readonly IRepository<UserEntity> userRepository;

        public GetUserByEmailQueryHandler(
            IRepository<UserEntity> userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserView Handle(GetUserByEmailQuery query)
        {
            var result =
                from user in this.userRepository.Entities
                where user.Email == query.Email
                select new UserView
                {
                    Id = user.Id,
                };

            return result.SingleOrDefault();
        }
    }
}

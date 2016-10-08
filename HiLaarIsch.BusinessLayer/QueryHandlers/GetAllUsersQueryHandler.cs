using System.Linq;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Contract.Queries;
using HiLaarIsch.Domain;
using QuantumHive.Core;

namespace HiLaarIsch.BusinessLayer.QueryHandlers
{
    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, UserView[]>
    {
        private readonly IRepository<UserEntity> userRepository;

        public GetAllUsersQueryHandler(IRepository<UserEntity> userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserView[] Handle(GetAllUsersQuery query)
        {
            var users =
                from user in this.userRepository.Entities
                select new UserView
                {
                    Id = user.Id,
                    Username = user.Username,
                };

            return users.ToArray();
        }
    }
}

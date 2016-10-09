using System.Linq;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Contract.Queries;
using HiLaarIsch.Domain;
using QuantumHive.Core;

namespace HiLaarIsch.BusinessLayer.QueryHandlers
{
    public class GetUserQueryHandlers :
        IQueryHandler<GetUserByEmailQuery, UserView>,
        IQueryHandler<GetPasswordHashByUserIdQuery, string>
    {
        private readonly IRepository<UserEntity> userRepository;

        public GetUserQueryHandlers(IRepository<UserEntity> userRepository)
        {
            this.userRepository = userRepository;
        }

        public string Handle(GetPasswordHashByUserIdQuery query)
        {
            var user = this.userRepository.GetById(query.UserId);
            return user.PasswordHash;
        }

        public UserView Handle(GetUserByEmailQuery query)
        {
            var result =
                from user in this.userRepository.Entities
                where user.Email == query.Email
                select new UserView
                {
                    Id = user.Id,
                    Username = user.Username,
                };

            return result.SingleOrDefault();
        }
    }
}

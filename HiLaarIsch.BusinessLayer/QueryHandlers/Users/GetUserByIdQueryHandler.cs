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
            var user = this.userRepository.GetById(query.Id);
            return new UserView
            {
                Id = user.Id,
                Email = user.Email,
                EmailConfirmed = user.EmailConfirmed,
            };
        }
    }
}

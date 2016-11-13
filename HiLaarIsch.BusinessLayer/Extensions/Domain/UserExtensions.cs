using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Domain;

namespace HiLaarIsch.BusinessLayer
{
    public static class UserExtensions
    {
        public static UserView Map(this UserEntity source)
        {
            return new UserView
            {
                Id = source.Id,
                Email = source.Email,
                EmailConfirmed = source.EmailConfirmed,
                Role = source.Role,
            };
        }
    }
}

using HiLaarIsch.Components;

namespace HiLaarIsch.Domain
{
    public partial class UserEntity
    {
        public static UserEntity CreateNewUser(string email)
        {
            var user = new UserEntity
            {
                Email = email,
                EmailConfirmed = false,
                PasswordHash = null,
                Role = Role.Customer,
            };

            return user;
        }

        public CustomerEntity CreateNewCustomer()
        {
            var customer = new CustomerEntity
            {
                User = this,
            };

            return customer;
        }
    }
}

using System;
using HiLaarIsch.Components;

namespace HiLaarIsch.Domain
{
    public partial class UserEntity
    {
        public static UserEntity CreateNewUser(string email)
        {
            var user = new UserEntity
            {
                Id = Guid.NewGuid(), //TODO: CCC
                Email = email,
                EmailConfirmed = false,
                PasswordHash = "ADvVwRre5wxBC3PiOAl78Je3qdGzaOhod+IEqd0FBY8oql10ELc4ESJmmJtJ3R0LTQ==", //temporary admin password
                Role = Role.Customer,
            };

            return user;
        }

        public CustomerEntity CreateNewCustomer()
        {
            var customer = new CustomerEntity
            {
                Id = Guid.NewGuid(), //TODO: CCC
                User = this,
            };
            return customer;
        }
    }
}

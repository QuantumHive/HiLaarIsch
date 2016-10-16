﻿using System;
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
                PasswordHash = null,
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

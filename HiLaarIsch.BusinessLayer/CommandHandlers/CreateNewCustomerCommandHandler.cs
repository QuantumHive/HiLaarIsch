﻿using System;
using HiLaarIsch.Contract.Commands;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Domain;
using QuantumHive.Core;

namespace HiLaarIsch.BusinessLayer.CommandHandlers
{
    public class CreateNewCustomerCommandHandler : ICommandHandler<CreateModelCommand<CustomerModel>>
    {
        private readonly IRepository<CustomerEntity> customerRepository;
        private readonly IRepository<UserEntity> userRepository;

        public CreateNewCustomerCommandHandler(
            IRepository<CustomerEntity> customerRepository,
            IRepository<UserEntity> userRepository)
        {
            this.customerRepository = customerRepository;
            this.userRepository = userRepository;
        }

        public void Handle(CreateModelCommand<CustomerModel> command)
        {
            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                Email = command.Model.Email,
                Username = "",
                EmailConfirmed = false,
                PasswordHash = "AGZG5lrJspk34rI/vq/I/0r1NDNdKwpNrVsB2DF9rwmVlInUY8MBbsaktHJsu2uiAg==", //temporary empty password
                Role = 0,
            };

            var customer = new CustomerEntity
            {
                Id = Guid.NewGuid(),
                Firstname = command.Model.Firstname,
                Surname = command.Model.Surname,
                DateOfBirth = DateTime.Now.AddYears(-20),
                Users = user,
                Gender = false,
            };

            this.customerRepository.Add(customer);
        }
    }
}

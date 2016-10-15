using System;
using HiLaarIsch.Components;
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
                EmailConfirmed = false,
                PasswordHash = "ADvVwRre5wxBC3PiOAl78Je3qdGzaOhod+IEqd0FBY8oql10ELc4ESJmmJtJ3R0LTQ==", //temporary admin password
                Role = Role.Customer,
            };

            var customer = new CustomerEntity
            {
                Id = Guid.NewGuid(),
                Firstname = command.Model.Firstname,
                Surname = command.Model.Surname,
                GroupLevel = command.Model.GroupLevel,
                User = user,
            };

            this.customerRepository.Add(customer);
        }
    }
}

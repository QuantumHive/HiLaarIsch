using System.Linq;
using HiLaarIsch.Contract.Commands;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Domain;
using QuantumHive.Core;

namespace HiLaarIsch.BusinessLayer.CommandHandlers.Customers
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
            var newUser =
                from user in this.userRepository.Entities
                where user.Email == command.Model.Email
                where !user.EmailConfirmed
                select user;

            var customer = newUser.Single().CreateNewCustomer();

            command.Model.Map(customer);

            this.customerRepository.Add(customer);
        }
    }
}

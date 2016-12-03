using HiLaarIsch.Contract.Commands;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Domain;
using QuantumHive.Core;

namespace HiLaarIsch.BusinessLayer.CommandHandlers.Customers
{
    public class UpdateCustomerCommandHandler : ICommandHandler<UpdateModelCommand<CustomerModel>>
    {
        private readonly IRepository<CustomerEntity> customerRepository;

        public UpdateCustomerCommandHandler(
            IRepository<CustomerEntity> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public void Handle(UpdateModelCommand<CustomerModel> command)
        {
            var customer = this.customerRepository.GetById(command.Id);
            customer.User.Email = command.Model.Email;
            command.Model.Map(customer);
        }
    }
}

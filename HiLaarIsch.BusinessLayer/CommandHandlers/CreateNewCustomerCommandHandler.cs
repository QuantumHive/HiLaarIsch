using System;
using HiLaarIsch.Contract.Commands;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Domain;
using QuantumHive.Core;

namespace HiLaarIsch.BusinessLayer.CommandHandlers
{
    public class CreateNewCustomerCommandHandler : ICommandHandler<CreateModelCommand<CustomerModel>>
    {
        private readonly IRepository<CustomerEntity> customerRepository;

        public CreateNewCustomerCommandHandler(
            IRepository<CustomerEntity> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public void Handle(CreateModelCommand<CustomerModel> command)
        {
            throw new NotImplementedException();
        }
    }
}

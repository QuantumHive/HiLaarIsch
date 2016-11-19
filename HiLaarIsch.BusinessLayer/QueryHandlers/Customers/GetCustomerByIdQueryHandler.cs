using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Contract.Queries;
using HiLaarIsch.Domain;
using QuantumHive.Core;

namespace HiLaarIsch.BusinessLayer.QueryHandlers.Customers
{
    public class GetCustomerByIdQueryHandler : IQueryHandler<GetModelByIdQuery<CustomerModel>, CustomerModel>
    {
        private readonly IRepository<CustomerEntity> customerRepository;

        public GetCustomerByIdQueryHandler(
            IRepository<CustomerEntity> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public CustomerModel Handle(GetModelByIdQuery<CustomerModel> query)
        {
            var customer = this.customerRepository.GetById(query.Id);
            return new CustomerModel
            {
                Id = customer.Id,
                Email = customer.User.Email,
                Firstname = customer.Firstname,
                Surname = customer.Surname,
                Level = customer.Level,
                PhoneNumber = customer.PhoneNumber,
                EmergencyNumber = customer.EmergencyNumber,
                Address = customer.Address,
            };
        }
    }
}

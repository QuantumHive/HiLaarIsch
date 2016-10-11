using System.Linq;
using HiLaarIsch.Contract.DTOs;
using HiLaarIsch.Contract.Queries;
using HiLaarIsch.Domain;
using QuantumHive.Core;

namespace HiLaarIsch.BusinessLayer.QueryHandlers
{
    public class GetAllCustomersQueryHandler : IQueryHandler<GetAllModelsQuery<CustomerView>, CustomerView[]>
    {
        private readonly IRepository<CustomerEntity> customerRepository;

        public GetAllCustomersQueryHandler(IRepository<CustomerEntity> customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public CustomerView[] Handle(GetAllModelsQuery<CustomerView> query)
        {
            var customers =
                from customer in this.customerRepository.Entities
                select new CustomerView
                {
                    Id = customer.Id,
                    FirstName = customer.Firstname,
                    SurName = customer.Surname,
                };

            return customers.ToArray();
        }
    }
}

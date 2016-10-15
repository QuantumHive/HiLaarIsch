using System;
using HiLaarIsch.Components;

namespace HiLaarIsch.Contract.DTOs
{
    public class CustomerModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Firstname { get; set; }
        public string Surname { get; set; }
        public GroupLevel GroupLevel { get; set; }

        public string PhoneNumber { get; set; }
        public string EmergencyNumber { get; set; }
        public string Address { get; set; }
    }
}

using System;

namespace HiLaarIsch.Contract.DTOs
{
    public class CustomerModel
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}

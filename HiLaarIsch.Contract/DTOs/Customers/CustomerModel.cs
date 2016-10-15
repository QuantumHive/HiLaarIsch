using System;
using HiLaarIsch.Components;

namespace HiLaarIsch.Contract.DTOs
{
    public class CustomerModel
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public GroupLevel GroupLevel { get; set; }
    }
}

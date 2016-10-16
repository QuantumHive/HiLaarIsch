using System;

namespace HiLaarIsch.Contract.DTOs
{
    public class UserView
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}

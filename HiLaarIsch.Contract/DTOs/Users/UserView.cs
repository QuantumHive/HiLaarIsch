﻿using System;
using HiLaarIsch.Components;

namespace HiLaarIsch.Contract.DTOs
{
    public class UserView
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public Role Role { get; set; }
    }
}

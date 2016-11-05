using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HiLaarIsch.Contract.DTOs;
using Microsoft.AspNet.Identity;

namespace HiLaarIsch.Identity
{
    public class HilaarischUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}
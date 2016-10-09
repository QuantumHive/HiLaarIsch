using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HiLaarIsch.Contract.DTOs;
using Microsoft.AspNet.Identity;

namespace HiLaarIsch.Identity
{
    public class IdentityUser : IUser<Guid>
    {
        public IdentityUser(UserView user)
        {
            this.Id = user.Id;
            this.UserName = user.Username;
        }

        public Guid Id { get; }

        public string UserName { get; set; }
    }
}
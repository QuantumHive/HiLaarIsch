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
            //we don't have usernames in our application, but the interfaces forces us to implement it
            this.UserName = string.Empty;
        }

        public Guid Id { get; }

        public string UserName { get; set; }
    }
}
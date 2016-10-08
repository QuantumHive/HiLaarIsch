using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace HiLaarIsch.Identity
{
    public class IdentityUser : IUser<Guid>
    {
        public IdentityUser(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }

        public string UserName { get; set; }
    }
}
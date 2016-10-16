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
        public IdentityUser(Guid id, string email)
        {
            this.Id = id;
            this.UserName = email;
        }

        private IdentityUser(string email)
        {
            this.Id = Guid.Empty;
            this.UserName = email;
        }

        public Guid Id { get; }

        /// <summary>
        /// Usernames not supported in application.
        /// However the Identity interface forces us to implement it.
        /// So the username is actually the user's email.
        /// </summary>
        public string UserName { get; set; }

        public static IdentityUser CreateNewUser(string email)
        {
            return new IdentityUser(email);
        }
    }
}
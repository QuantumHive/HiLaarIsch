using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using HiLaarIsch.Components;

namespace HiLaarIsch
{
    public static class SecurityExtensions
    {
        public static bool IsInRole(this IPrincipal userPrincipal, Role role)
        {
            return userPrincipal.IsInRole(role.ToString());
        }
    }
}
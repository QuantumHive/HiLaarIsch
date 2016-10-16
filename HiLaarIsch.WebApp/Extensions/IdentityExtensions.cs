using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HiLaarIsch.Contract.Queries;
using HiLaarIsch.Identity;
using Microsoft.AspNet.Identity;
using QuantumHive.Core;

namespace HiLaarIsch
{
    public static class IdentityExtensions
    {
        public static bool UserExists(this IQueryProcessor queryProcessor, Guid userId)
        {
            return userId != null && queryProcessor.Process(new UserExistsQuery(userId));
        }
    }
}
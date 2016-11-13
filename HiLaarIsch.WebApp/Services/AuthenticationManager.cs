using System.Security.Claims;
using HiLaarIsch.Contract.DTOs;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using QuantumHive.Core;

namespace HiLaarIsch.Services
{
    public class AuthenticationManager : IAuthenticationManager<UserView>
    {
        private readonly IAuthenticationManager authenticationManager;

        public AuthenticationManager(
            IAuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;
        }

        public void SignIn(UserView user)
        {
            var userIdentity = ClaimsIdentityFactory.Create(user);

            this.authenticationManager.SignOut();
            this.authenticationManager.SignIn(userIdentity);
        }

        public void SignOut()
        {
            this.authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        private class ClaimsIdentityFactory
        {
            private const string IdentityProviderClaimType = @"http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider";
            private const string DefaultIdentityProviderClaimValue = @"ASP.NET Identity";

            public static ClaimsIdentity Create(UserView user)
            {
                var id = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                id.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
                id.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email, ClaimValueTypes.String));
                id.AddClaim(new Claim(IdentityProviderClaimType, DefaultIdentityProviderClaimValue, ClaimValueTypes.String));
                id.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString(), ClaimValueTypes.String));

                return id;
            }
        }
    }
}
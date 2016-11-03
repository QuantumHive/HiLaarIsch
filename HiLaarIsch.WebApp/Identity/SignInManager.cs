using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace HiLaarIsch.Identity
{
    public class SignInManager
    {
        private readonly IAuthenticationManager authenticationManager;

        public SignInManager(
            IAuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;
        }

        public void SignIn(HilaarischUser user)
        {
            var userIdentity = ClaimsIdentityFactory.Create(user);

            this.authenticationManager.SignOut();
            this.authenticationManager.SignIn(userIdentity);
        }

        private class ClaimsIdentityFactory
        {
            private const string IdentityProviderClaimType = @"http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider";
            private const string DefaultIdentityProviderClaimValue = @"ASP.NET Identity";

            public static ClaimsIdentity Create(HilaarischUser user)
            {
                var id = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

                id.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
                id.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email, ClaimValueTypes.String));
                id.AddClaim(new Claim(IdentityProviderClaimType, DefaultIdentityProviderClaimValue, ClaimValueTypes.String));

                return id;
            }
        }
    }
}
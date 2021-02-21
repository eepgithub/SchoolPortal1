using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolPortal.Data
{
    public class ApplicationUserClaims : UserClaimsPrincipalFactory<ApplicationUser>
    {
        public ApplicationUserClaims(
            UserManager<ApplicationUser> userManager,
            IOptions<IdentityOptions> optionsAccessor) 
            : base(userManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var roles = await UserManager.GetRolesAsync(user);
            var identity = await base.GenerateClaimsAsync(user);

            identity.AddClaim(new Claim("DisplayName", user.DisplayName));
            identity.AddClaim(new Claim(ClaimTypes.Role, roles.FirstOrDefault()));

            return identity;
        }
    }
}

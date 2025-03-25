using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RealEstateApp.Infrastructure.Identity
{
    /// <summary>
    /// Maps the Azure B2C custom attribute "extension_UserRole" to the .NET Role claim,
    /// so that [Authorize(Roles = "...")] works as expected.
    /// </summary>
    public class B2CCustomRoleMapper : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var identity = principal.Identity as ClaimsIdentity;

            if (identity != null)
            {
                var roleClaim = identity.FindFirst("extension_UserRole");

                // Prevent duplicate roles
                if (roleClaim != null && !identity.HasClaim(c => c.Type == ClaimTypes.Role))
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, roleClaim.Value));
                }
            }

            return Task.FromResult(principal);
        }
    }
}

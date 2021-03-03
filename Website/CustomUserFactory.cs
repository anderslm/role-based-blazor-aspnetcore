using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;

namespace Website
{
    public class CustomUserFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
    {
        public CustomUserFactory(IAccessTokenProviderAccessor accessor)
            : base(accessor)
        {
        }

        public override async ValueTask<ClaimsPrincipal> CreateUserAsync(
            RemoteUserAccount account,
            RemoteAuthenticationUserOptions options)
        {
            var user = await base.CreateUserAsync(account, options);

            if (user.Identity.IsAuthenticated)
            {
                var identity = (ClaimsIdentity)user.Identity;

                var rolesClaim = identity.Claims.Single(c => c.Type == "roles").Value;
                var roles = JsonSerializer.Deserialize<string[]>(rolesClaim);

                foreach (var role in roles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }
            }

            return user;
        }
    }
}
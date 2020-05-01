using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BasicClientServerApp.Server.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace BasicClientServerApp.Server.Authorization
{
    public class OneOrMoreRolesAuthorizationHandler : AuthorizationHandler<OneOrMoreRoleAuthorizationRequirenment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            OneOrMoreRoleAuthorizationRequirenment requirement)
        {
            if (context.User.HasClaim(c => c.Type == ClaimTypes.Role && requirement.Roles.Contains(c.Value)))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            else
            {
                context.Fail();
                return Task.CompletedTask;
            }
        }
    }

    public class ClaimS : IClaimsTransformation
    {
        private readonly EmployeePermissionStore store;

        public ClaimS(EmployeePermissionStore store)
        {
            this.store = store;
        }
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var claim = principal.Claims.FirstOrDefault(x => x.Type == "BCSA.EmployeeNumber");
            var id = int.Parse(claim.Value);
            var permissions  = store.GetEmployeePermissions(id);
            var ci = (principal.Identities as ClaimsIdentity);
            foreach (var permmission in permissions)
            {
                ci.AddClaim(new Claim("BCSA.CustomPermission", permmission.Name));
            }
            return principal;
        }
    }
}

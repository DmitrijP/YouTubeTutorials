using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
}

using Microsoft.AspNetCore.Authorization;

namespace BasicClientServerApp.Server.Authorization
{
    public class OneOrMoreRoleAuthorizationRequirenment : IAuthorizationRequirement
    {
        public OneOrMoreRoleAuthorizationRequirenment(string[] roles)
        {
            Roles = roles;
        }

        public string[] Roles { get; }
    }
}

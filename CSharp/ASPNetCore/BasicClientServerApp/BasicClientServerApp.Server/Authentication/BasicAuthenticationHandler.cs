using BasicClientServerApp.Server.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BasicClientServerApp.Server.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly UserStore userStore;

        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> optionsMonitor, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock systemClock, UserStore userStore)
             : base(optionsMonitor, logger, encoder, systemClock)
        {
            this.userStore = userStore;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("No authorization header found!");

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentialString = Encoding.UTF8.GetString(credentialBytes);

            var credentialParts = credentialString.Split(':');
            var userName = credentialParts[0];
            var password = credentialParts[1];

            var userEntity = await userStore.FindUserProfile(userName);
            if (userEntity == null)
                return AuthenticateResult.Fail("Benutzer existiert nicht!");

            if (userEntity.Password != password)
                return AuthenticateResult.Fail("Password ist falsch!");

            var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, userEntity.UserName),
                    new Claim(ClaimTypes.Locality, userEntity.City),
                    new Claim(ClaimTypes.DateOfBirth, userEntity.Birthday.ToString("yyyy-MM-ddTHH:mm:ss")),
                };

            foreach (var item in userEntity.Roles.Split(':'))
            {
                claims.Add(new Claim(ClaimTypes.Role, item));
            }

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return AuthenticateResult.Success(ticket);

        }
    }
}

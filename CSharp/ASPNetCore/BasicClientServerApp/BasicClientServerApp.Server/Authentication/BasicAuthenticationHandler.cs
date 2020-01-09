using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BasicClientServerApp.Server.Authentication
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> optionsMonitor, ILoggerFactory logger, 
            UrlEncoder encoder, ISystemClock systemClock)
             : base(optionsMonitor, logger, encoder, systemClock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("No authorization header found!");

            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentialString = Encoding.UTF8.GetString(credentialBytes);

            var credentialParts = credentialString.Split(':');
            if (credentialParts[0] == "DmitrijP" && credentialParts[1] == "SuperPasswort")
            {
                var claims = new[] { 
                    new Claim(ClaimTypes.Name, "Patuk")                
                };

                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            else
            {
                return AuthenticateResult.Fail("Benutzername oder Passwort falsch");
            }
        }
    }
}

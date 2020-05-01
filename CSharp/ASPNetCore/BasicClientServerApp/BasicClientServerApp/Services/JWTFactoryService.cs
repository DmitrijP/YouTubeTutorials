using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BasicClientServerApp.Entities;
using BasicClientServerApp.Models.JWTModels;

namespace BasicClientServerApp.Services
{
    public class JWTFactoryService : IJWTFactoryService
    {
        private string _hashKey = "aldijasödfjsd28973645slkjgasukz762##.---2873246safd<62";
        private TimeSpan _timeToLife = TimeSpan.FromHours(12);

        public async Task<string> CreateToken(UserProfileEntity userProfileEntity)
        {
            await Task.Delay(100);

            var header = new Header { alg = "HS256", typ = "JWT" };
            var payload = new Payload
            {
                //audience
                aud = "myCoolWebApp",
                //subject
                sub = userProfileEntity.UserName,
                roles = userProfileEntity.Roles.Split(':'),
                //expirationTime
                exp = ToUnixTime(DateTime.UtcNow + _timeToLife),
                //IssuedAT
                iat = ToUnixTime(DateTime.UtcNow),
                //issuer
                iss = "my cool web auth server",
                //Id des Tokens
                jit = userProfileEntity.Id,
                name =  $"{userProfileEntity.FirstName} {userProfileEntity.LastName}",
                claims = new System.Collections.Generic.Dictionary<string, string>
                {
                    { "Position", userProfileEntity.Position },
                    { "Birthday", userProfileEntity.Birthday.ToLongDateString() },
                    { "CompanyName", userProfileEntity.CompanyName },
                    { "City", userProfileEntity.City }
                }
            };

            var headerString = Newtonsoft.Json.JsonConvert.SerializeObject(header);
            var payloadString = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

            var headerStringAsBase64Url = Base64UrlEncode(headerString);
            var payloadStringStringAsBase64Url= Base64UrlEncode(payloadString);
            var headAndPayLoad = $"{headerStringAsBase64Url}.{payloadStringStringAsBase64Url}";
            var hashAsBase64URL = CalculateHashSignature(headAndPayLoad);
            return $"{headerStringAsBase64Url}.{payloadStringStringAsBase64Url}.{hashAsBase64URL}";

        }

        public string Base64UrlEncode(string text)
        {
            var encoding = new UTF8Encoding();
            var bytes = encoding.GetBytes(text);
            var base64 = Convert.ToBase64String(bytes);
            return base64.Replace('+', '-').Replace('/', '_').Replace("=", "");
        }

        public string CalculateHashSignature(string headerAndPayload)
        {
            var encoding = new UTF8Encoding();
            var keyBytes = encoding.GetBytes(_hashKey);
            var headerAndPayloadBytes = encoding.GetBytes(headerAndPayload);

            using var hasher = new HMACSHA256(keyBytes);
            var hashBytes = hasher.ComputeHash(headerAndPayloadBytes);

            var hashAsString = BitConverter.ToString(hashBytes);
            var base64UrlHashString = Base64UrlEncode(hashAsString);
            return base64UrlHashString;
        }



        public DateTime FromUnixTime(double time)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return origin.AddSeconds(time);
        }

        public double ToUnixTime(DateTime time)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var unixTime = time - origin;
            return unixTime.TotalSeconds;
        }
    }
}

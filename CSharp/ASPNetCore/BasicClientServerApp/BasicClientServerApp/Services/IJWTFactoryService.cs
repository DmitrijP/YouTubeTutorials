using System;
using System.Threading.Tasks;
using BasicClientServerApp.Entities;

namespace BasicClientServerApp.Services
{
    public interface IJWTFactoryService
    {
        Task<string> CreateToken(UserProfileEntity userProfileEntity);
        string CalculateHashSignature(string headerAndPayload);
        string Base64UrlEncode(string text);
        DateTime FromUnixTime(double time);
        double ToUnixTime(DateTime time);
    }
}

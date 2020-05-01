using BasicClientServerApp.Models;
using System.Threading.Tasks;

namespace BasicClientServerApp.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponseModel> Authenticate(AuthenticationRequestModel model);
    }
}

using System.Threading.Tasks;
using BasicClientServerApp.Models;
using BasicClientServerApp.DataStores;

namespace BasicClientServerApp.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserStore userStore;
        private readonly IJWTFactoryService jWTFactoryService;

        public AuthenticationService(IJWTFactoryService jWTFactoryService, IUserStore userStore)
        {
            this.jWTFactoryService = jWTFactoryService;
            this.userStore = userStore;
        }

        public async Task<AuthenticationResponseModel> Authenticate(AuthenticationRequestModel model)
        {
            if(model.grant_type == "password")
            {
                if(model.scope == "my cool app")
                {
                    var userEntity = await userStore.FindUserProfile(model.client_id);
                    if (userEntity == null)
                        return null;

                    if(userEntity.Password == model.client_secret)
                    {
                        var token = await jWTFactoryService.CreateToken(userEntity);
                        return new AuthenticationResponseModel
                        {
                            access_token = token,
                            token_type = "Bearer"
                        };
                    }
                }
            }
            return null;
        }
    }
}

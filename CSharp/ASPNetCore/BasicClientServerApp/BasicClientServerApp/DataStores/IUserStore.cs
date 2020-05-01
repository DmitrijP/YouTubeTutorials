using System.Threading.Tasks;
using BasicClientServerApp.Entities;

namespace BasicClientServerApp.DataStores
{
    public interface IUserStore
    {
        Task<UserProfileEntity> FindUserProfile(string userName);
    }
}

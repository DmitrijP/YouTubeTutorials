using EmployeeManagementSystem.ADLibs.Interfaces.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ServerApp.Services
{
    public class ADUserService : ServiceBase, IADUserService
    {
        private readonly HttpClient _httpClient;

        public ADUserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ADUser> ChangePassword(int id, string password)
        {
            var result = await _httpClient.PostAsync($"ActiveDirectoryUser/change-password?id={id}&password={password}", null);
            result.EnsureSuccessStatusCode();
            return await Deserialize<ADUser>(result);
        }

        public async Task Create(int id)
        {
            var result = await _httpClient.PostAsync($"ActiveDirectoryUser/create?id={id}", null);
            result.EnsureSuccessStatusCode();
        }

        public async Task Delete(int id)
        {
            var result = await _httpClient.DeleteAsync($"ActiveDirectoryUser/delete?id={id}");
            result.EnsureSuccessStatusCode();
        }

        public async Task<ADUser> Disable(int id)
        {
            var result = await _httpClient.PostAsync($"ActiveDirectoryUser/disable?id={id}", null);
            result.EnsureSuccessStatusCode();
            return await Deserialize<ADUser>(result);
        }

        public async Task<ADUser> Enable(int id)
        {
            var result = await _httpClient.PostAsync($"ActiveDirectoryUser/enable?id={id}", null);
            result.EnsureSuccessStatusCode();
            return await Deserialize<ADUser>(result);
        }

        public async Task<ADUser> ExpirePassword(int id)
        {
            var result = await _httpClient.PostAsync($"ActiveDirectoryUser/expire-password?id={id}", null);
            result.EnsureSuccessStatusCode();
            return await Deserialize<ADUser>(result);
        }

        public async Task<ADUser> GetDetails(int id)
        {
            var result = await _httpClient.GetAsync($"ActiveDirectoryUser/get-details?id={id}");
            result.EnsureSuccessStatusCode();
            return await Deserialize<ADUser>(result);
        }

        public async Task<ADUser> RefreshPassword(int id)
        {
            var result = await _httpClient.PostAsync($"ActiveDirectoryUser/refresh-expired-password?id={id}", null);
            result.EnsureSuccessStatusCode();
            return await Deserialize<ADUser>(result);
        }

        public async Task<ADUser> Unlock(int id)
        {
            var result = await _httpClient.PostAsync($"ActiveDirectoryUser/unlock?id={id}", null);
            result.EnsureSuccessStatusCode();
            return await Deserialize<ADUser>(result);
        }
    }
}

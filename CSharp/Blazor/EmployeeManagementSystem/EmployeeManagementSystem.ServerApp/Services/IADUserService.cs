using EmployeeManagementSystem.ADLibs.Interfaces.Models;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ServerApp.Services
{
    public interface IADUserService
    {
        Task<ADUser> GetDetails(int id);
        Task Create(int id);
        Task Delete(int id);
        Task<ADUser> Disable(int id);
        Task<ADUser> ExpirePassword(int id);
        Task<ADUser> RefreshPassword(int id);
        Task<ADUser> Unlock(int id);
        Task<ADUser> Enable(int id);
        Task<ADUser> ChangePassword(int id, string password);
    }
}

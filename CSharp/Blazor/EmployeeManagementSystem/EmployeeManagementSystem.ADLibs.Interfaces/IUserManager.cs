using EmployeeManagementSystem.ADLibs.Interfaces.Models;

namespace EmployeeManagementSystem.ADLibs.Interfaces
{
    public interface IUserManager
    {
        void Create(string username, string password, string firstName, string lastName, string mail, string phone);
        void Delete(string name);
        void Unlock(string searchValue);
        void Disable(string searchValue);
        void Enable(string searchValue);
        void ExpirePassword(string searchValue);
        void RefreshExpiredPassword(string searchValue);
        void ChangePassword(string searchValue, string newPassword);
        ADUser GetDetails(string username);
    }
}

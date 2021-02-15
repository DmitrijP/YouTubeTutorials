using EmployeeManagementSystem.ADLibs.Interfaces.Models;

namespace EmployeeManagementSystem.ADLibs.Interfaces
{
    public interface IGroupManager
    {
        void Create(string name, string description);
        bool Delete(string name);
        bool AddToGroup(string userName, string groupName);
        bool RemoveFromGroup(string userName, string groupName);
        ADGroup GetDetails(string name);
    }
}

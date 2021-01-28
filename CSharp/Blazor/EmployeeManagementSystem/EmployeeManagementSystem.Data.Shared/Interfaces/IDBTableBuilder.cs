using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.Shared.Interfaces
{
    public interface IDBTableBuilder
    {
        Task CreateTables();
    }
}

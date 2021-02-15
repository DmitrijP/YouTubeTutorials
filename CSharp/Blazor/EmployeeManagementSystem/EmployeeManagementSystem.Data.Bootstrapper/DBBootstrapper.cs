using EmployeeManagementSystem.Data.Bootstrapper.Interfaces;
using EmployeeManagementSystem.Data.Shared.Interfaces;
using EmployeeManagementSystem.Data.Shared.Interfaces.Commands;
using EmployeeManagementSystem.Data.Shared.Interfaces.Creation;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.Bootstrapper
{
    public class DBBootstrapper : IDBBootstrapper
    {
        private readonly IDBTableBuilder tableBuilder;
        private readonly IEmployeeCommands commands;
        private readonly IEmployeeSource source;

        public DBBootstrapper(IDBTableBuilder factory, IEmployeeCommands commands, IEmployeeSource source)
        {
            this.tableBuilder = factory;
            this.commands = commands;
            this.source = source;
        }

        public async Task Initialize()
        {
            await tableBuilder.CreateTables();
        }
    }
}

using EmployeeManagementSystem.Shared;
using EmployeeManagementSystem.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.App.Pages
{
    public partial class EmployeeDetails
    {
        [Parameter]
        public string Id { get; set; }
        //[Inject]
        //public EmployeeStore EmployeeStore { get; set; }

        public Employee Employee { get; set; } = new Employee();

        protected override async Task OnInitializedAsync()
        {
            //Employee = (await EmployeeStore.SelectEmployeeList())
            //    .FirstOrDefault(e => e.Login.Uuid == Id);
            await base.OnInitializedAsync();
        }
    }
}

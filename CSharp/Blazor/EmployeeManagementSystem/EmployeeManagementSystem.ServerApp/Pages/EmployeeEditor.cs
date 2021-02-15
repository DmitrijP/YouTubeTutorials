using EmployeeManagementSystem.ServerApp.Services;
using EmployeeManagementSystem.Shared;
using EmployeeManagementSystem.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ServerApp.Pages
{
    public partial class EmployeeEditor
    {
        [Parameter]
        public int? Id { get; set; }
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        private Employee Employee { get; set; }
        private bool NoEmployeeFound { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (Id.HasValue)
            {
                Employee = (await EmployeeService.GetAll())
                    .FirstOrDefault(e => e.Id == Id);
                if(Employee == null)
                {
                    NoEmployeeFound = true;
                }
            }
            else
            {
                Employee = new Employee();
            }
            await base.OnInitializedAsync();
        }

        private async Task HandleValidSubmit()
        {
            if (Id.HasValue)
            {
                //await EmployeeService.UpdateEmployee(Employee);
            }
            else
            {
                await EmployeeService.CreateEmployee(Employee);
                //Navigate to Details
            }
        }

        private void HandleInvalidSubmit()
        {
        }


    }
}

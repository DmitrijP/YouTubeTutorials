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
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        
        private Employee Employee { get; set; }
        public bool NoEmployeeFound { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            if (Id.HasValue)
            {
                Employee = await EmployeeService.Get(Id.Value);
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
            Employee e;
            if (Id.HasValue)
            {
                e = await EmployeeService.Update(Employee);
            }
            else
            {
                e = await EmployeeService.Create(Employee);
            }
            if(e == null)
                NavigationManager.NavigateTo($"/error");
            else
                NavigationManager.NavigateTo($"/employee-details/{e.Id}");
        }

        private void HandleInvalidSubmit()
        {
        }


    }
}

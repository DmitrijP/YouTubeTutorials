using EmployeeManagementSystem.ServerApp.Services;
using EmployeeManagementSystem.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ServerApp.Pages
{
    public partial class EmployeeManagementOverview
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        public IEnumerable<Employee> EmployeeList { get; set; }

        protected override async Task OnInitializedAsync()
        {
            EmployeeList = await EmployeeService.GetAll();
            await base.OnInitializedAsync();
        }
    }
}

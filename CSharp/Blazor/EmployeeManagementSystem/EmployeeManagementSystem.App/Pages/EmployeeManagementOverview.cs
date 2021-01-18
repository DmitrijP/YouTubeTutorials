using EmployeeManagementSystem.Shared;
using EmployeeManagementSystem.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.App.Pages
{
    public partial class EmployeeManagementOverview
    {
        [Inject]
        public EmployeeStore EmployeeStore { get; set; }

        public IEnumerable<Employee> EmployeeList { get; set; }

        protected override async Task OnInitializedAsync()
        {
            EmployeeList = await EmployeeStore.SelectEmployeeList();
            await base.OnInitializedAsync();
        }
    }
}

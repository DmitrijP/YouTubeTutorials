using EmployeeManagementSystem.ADLibs.Interfaces.Models;
using EmployeeManagementSystem.ServerApp.Services;
using EmployeeManagementSystem.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ServerApp.Pages
{
    public partial class EmployeeDetails
    {
        [Parameter]
        public int Id { get; set; }
        public string NewPassword { get; set; }
        [Inject]
        public IEmployeeService EmployeeService { get; set; }
        [Inject]
        public IADUserService AdUserService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }


        private ADUser AdUser { get; set; } = new ADUser();
        public Employee Employee { get; set; } = new Employee();

        protected override async Task OnInitializedAsync()
        {
            Employee = await EmployeeService.Get(Id);
            AdUser = await AdUserService.GetDetails(Id);
            await base.OnInitializedAsync();
        }

        private async Task Enable()
        {
            AdUser = await AdUserService.Enable(Id);
        }

        private async Task Disable()
        {
            AdUser = await AdUserService.Disable(Id);
        }

        private async Task ChangePassword()
        {
            AdUser = await AdUserService.ChangePassword(Id, NewPassword);
        }

        private async Task RefreshPassword()
        {
            AdUser = await AdUserService.RefreshPassword(Id);
        }

        private async Task UnlockPassword()
        {
            AdUser = await AdUserService.Unlock(Id);
        }

        private async Task ExpirePassword()
        {
            AdUser = await AdUserService.ExpirePassword(Id);
        }

        private async Task Create()
        {
            await AdUserService.Create(Id);
            AdUser = await AdUserService.GetDetails(Id);
        }

        private async Task Delete()
        {
            await AdUserService.Delete(Id);
            NavigationManager.NavigateTo($"/employee-management-overview");
        }
    }
}

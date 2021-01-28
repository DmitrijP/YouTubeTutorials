using EmployeeManagementSystem.Shared;
using EmployeeManagementSystem.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.App.Pages
{
    public partial class EmployeeEditor
    {
        [Parameter]
        public string Id { get; set; }
        //[Inject]
        //public EmployeeStore EmployeeStore { get; set; }

        public Employee Employee { get; set; } = InitializeNullEmployee();

        public string InvalidValidationMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //Employee = (await EmployeeStore.SelectEmployeeList())
            //    .FirstOrDefault(e => e.Login.Uuid == Id);
            await base.OnInitializedAsync();
        }

        private void HandleValidSubmit()
        {
            InvalidValidationMessage = "Alles Richtig!!!";
        }

        private void HandleInvalidSubmit()
        {
            InvalidValidationMessage = "Prüfe dein gedöns";
        }


        private static Employee InitializeNullEmployee()
        {
            return new Employee
            {
                Name = new Name { },
            };
        }
    }
}

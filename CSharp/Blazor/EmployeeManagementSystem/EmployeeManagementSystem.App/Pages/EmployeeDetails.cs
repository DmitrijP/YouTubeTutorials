using EmployeeManagementSystem.Shared;
using EmployeeManagementSystem.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.App.Pages
{
    public partial class EmployeeDetails
    {
        [Parameter]
        public string Id { get; set; }

        public Employee Employee { get; set; } = new Employee();

        protected override Task OnInitializedAsync()
        {
            var e = new EmployeeFactory();
            e.InitializeEmployeeList();
            Employee = e.EmployeeList.FirstOrDefault(e => e.EmployeeId == int.Parse(Id));
            return base.OnInitializedAsync();
        }
    }
}

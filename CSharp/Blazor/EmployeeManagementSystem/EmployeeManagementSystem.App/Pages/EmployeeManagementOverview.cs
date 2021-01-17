using EmployeeManagementSystem.Shared;
using EmployeeManagementSystem.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.App.Pages
{
    public partial class EmployeeManagementOverview
    {
        public IEnumerable<Employee> EmployeeList { get; set; }

        protected override Task OnInitializedAsync()
        {
            var e = new EmployeeFactory();
            e.InitializeEmployeeList();
            EmployeeList = e.EmployeeList;
            return base.OnInitializedAsync();
        }

       
    }
}

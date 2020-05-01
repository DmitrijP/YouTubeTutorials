using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebsite.Web.Stores;

namespace MyFirstWebsite.Web.Controllers
{
    public class ProfileViewComponent : ViewComponent
    {
        private readonly IEmployeeStore employeeStore;

        public ProfileViewComponent(IEmployeeStore employeeStore)
        {
            this.employeeStore = employeeStore;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var employee = await employeeStore.Find(id);
            if(id == 12)
            {
                return View("XYZ", employee);
            }
            return View(employee);
        }

       

    }
}

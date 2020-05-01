using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebsite.Web.Models;
using MyFirstWebsite.Web.Stores;
using MyFirstWebsite.Web.Models.Home;

namespace MyFirstWebsite.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeStore _employeeStore;

        public HomeController(IEmployeeStore employeeStore)
        {
            _employeeStore = employeeStore;
        }

        public async Task<IActionResult> Index()
        {
            var employeeList = await _employeeStore.GetAll();
            return View(new IndexModel{ HelloWorld = "Hallo Welt. Wie lauft es?", EmployeeList = employeeList });
        }

        public async Task<IActionResult> Find([FromQuery]string searchString)
        {
            var employeeList = await _employeeStore.Find(searchString);
            return Json(employeeList);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery]int idToDelete)
        {
            var result = await _employeeStore.Delete(idToDelete);
            return Json(new { EmployeeId = idToDelete, Result = result });
        }
         
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]EmployeeCreationModel model)
        {
            var createdEmployee = await _employeeStore.Insert(new Entitties.EmployeeEntity
            {
                Birthday = model.Birthday,
                City = model.City,
                CompanyName = model.CompanyName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Position = model.Position
            });
            return Json(createdEmployee);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


}

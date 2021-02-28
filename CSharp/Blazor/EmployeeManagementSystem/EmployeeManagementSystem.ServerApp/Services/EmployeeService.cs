using EmployeeManagementSystem.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ServerApp.Services
{
    public class EmployeeService : ServiceBase, IEmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Employee> Create(Employee e)
        {
            var result = await _httpClient.PostAsync($"/Employee/create", GenerateJsonContent(e));
            result.EnsureSuccessStatusCode();
            return await Deserialize<Employee>(result);
        }

        public async Task Delete(int id)
        {
            var result = await _httpClient.DeleteAsync($"/Employee/delete?id={id}");
            result.EnsureSuccessStatusCode();
        }

        public async Task<Employee> Get(int id)
        {
            var result = await _httpClient.GetAsync($"/Employee/get-one?id={id}");
            result.EnsureSuccessStatusCode();
            return await Deserialize<Employee>(result);
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            var result = await _httpClient.GetAsync($"/Employee/get-all");
            result.EnsureSuccessStatusCode();
            return await Deserialize<IEnumerable<Employee>>(result);
        }

        public async Task<Employee> Update(Employee e)
        {
            var result = await _httpClient.PutAsync($"/Employee/update", GenerateJsonContent(e));
            result.EnsureSuccessStatusCode();
            return await Deserialize<Employee>(result);
        }
    }
}

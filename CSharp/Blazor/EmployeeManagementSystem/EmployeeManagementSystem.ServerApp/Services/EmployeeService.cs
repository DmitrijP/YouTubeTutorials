using EmployeeManagementSystem.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ServerApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _httpClient;

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            var result = await _httpClient.GetStreamAsync($"Employee");
            return await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>(result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}

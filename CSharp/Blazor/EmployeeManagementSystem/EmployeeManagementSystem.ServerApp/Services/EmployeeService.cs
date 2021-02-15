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

        public async Task CreateEmployee(Employee e)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(e);
            var body = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync($"Create", body);
            result.EnsureSuccessStatusCode();
        }

        public async Task GenerateADProfile(Employee e)
        {
            var result = await _httpClient.PostAsync($"GenerateADProfile/{e.Id}", null);
            result.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            var result = await _httpClient.GetStreamAsync($"Employee");
            return await JsonSerializer.DeserializeAsync<IEnumerable<Employee>>(result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }
    }
}

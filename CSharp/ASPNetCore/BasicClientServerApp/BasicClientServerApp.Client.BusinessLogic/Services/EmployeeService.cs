using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using BasicClientServerApp.Client.BusinessLogic.Models;

namespace BasicClientServerApp.Client.BusinessLogic.Services
{
    public class EmployeeService
    {

        private static readonly HttpClient _httpClient = new HttpClient();
        public EmployeeService(string baseUri)
        {
            _httpClient.BaseAddress = new Uri(baseUri);
        }

        public async Task<string> CreateEmployeeAsync(EmployeeCreationModel model)
        {
            var modelAsJson = JsonSerializer.Serialize(model);
            var content = new StringContent(modelAsJson, System.Text.Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync($"Employee/Create",content);
            return await CheckAndProcessResultAsync(result);
        }

        public async Task<IEnumerable<EmployeeModel>> GetAllEmployeeAsync()
        {
            var result = await _httpClient.GetAsync($"Employee/GetAll");
            var stream = await result.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<IEnumerable<EmployeeModel>>(stream, new JsonSerializerOptions {PropertyNameCaseInsensitive = true });
        }

        public async Task<string> GetEmployeeByNameAsync(string name)
        {
            var result = await _httpClient.GetAsync($"Employee/Find/{name}");
            return await CheckAndProcessResultAsync(result);
        }

        public async Task<string> GetEmployeeByIdAsync(int id)
        {
            var result = await _httpClient.GetAsync($"Employee/Find/{id}");
            return await CheckAndProcessResultAsync(result);
        }

        public async Task<string> DeleteEmployeeAsync(int id)
        {
            var result = await _httpClient.DeleteAsync($"Employee/Delete/{id}");
            return await CheckAndProcessResultAsync(result);
        }

        private static async Task<string> CheckAndProcessResultAsync(HttpResponseMessage result)
        {
            try
            {
                var responseMessage = result.EnsureSuccessStatusCode();
                var stringResult = await responseMessage.Content.ReadAsStringAsync();
                return stringResult;

            }
            catch (Exception e)
            {
                return await result.Content.ReadAsStringAsync();
            }
        }
    }
}

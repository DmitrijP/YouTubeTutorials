using EmployeeManagementSystem.Data.Bootstrapper.Interfaces;
using EmployeeManagementSystem.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Data.Bootstrapper
{
    public class EmployeeSource : IEmployeeSource
    {
        public static string RequestPath = "/api/?seed=D_lkasdf&results=100&nat=us,dk,fr,gb";
        private readonly HttpClient _httpClient;
        private IEnumerable<Employee> _internalStorage;

        public EmployeeSource(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Employee>> SelectEmployeeList()
        {
            if (_internalStorage == null)
            {
                var result = await _httpClient.GetAsync(RequestPath);
                if (result.IsSuccessStatusCode)
                {
                    //var stringResult = await result.Content.ReadAsStringAsync();
                    //var deserializationResult = JsonConvert.DeserializeObject<EmployeeRequestResult>(stringResult);
                    //_internalStorage = deserializationResult.Results;
                }
                else
                {
                    _internalStorage = new List<Employee>();
                }
            }
            return _internalStorage;
        }
    }
}

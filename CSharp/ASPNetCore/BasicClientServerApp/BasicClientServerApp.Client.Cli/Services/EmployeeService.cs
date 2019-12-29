using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BasicClientServerApp.Client.Cli.Services
{
    class EmployeeService
    {

        private static readonly HttpClient _httpClient = new HttpClient();
        //private readonly string _baseUri;

        //public EmployeeService(string baseUri)
        //{
        //    _baseUri = baseUri;
        //}

        public string GetEmployee(string id)
        {
             var result = _httpClient
                .GetAsync($"https://localhost:44308/Employee/Find/{id}")
                .GetAwaiter()
                .GetResult();
            if(result.StatusCode == System.Net.HttpStatusCode.OK)
            {
               var stringResult = result.Content.ReadAsStringAsync()
                    .GetAwaiter()
                    .GetResult();
                return stringResult;
            }
            else
            {
                return result.StatusCode.ToString();
            }
        }

    }
}

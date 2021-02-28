using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.ServerApp.Services
{
    public class ServiceBase
    {
        protected async Task<T> Deserialize<T>(HttpResponseMessage httpContent)
        {
            var content = await httpContent.Content.ReadAsStringAsync();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(content);
        }

        protected StringContent GenerateJsonContent(object data)
        {
            var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            return new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
        }
    }
}

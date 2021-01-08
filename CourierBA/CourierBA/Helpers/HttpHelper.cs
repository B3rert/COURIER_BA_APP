using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CourierBA.Helpers
{
    public class HttpHelper<T>
    {
        public async Task<T> GetRestServiceDataAsync(string ServiceAdress)
        {
            var client = new HttpClient();
            client.BaseAddress = Global.GlobalVariables.Servidor;
            string url = string.Format(ServiceAdress);
            var response =
                await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResult =
                await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<T>(jsonResult);
            return result;
        }
    }
}

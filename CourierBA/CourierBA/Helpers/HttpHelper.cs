using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
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

        private byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer,0,buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private async void UploadFile(string baseurl, Stream file, string name)
        {
            var client = new RestClient(baseurl);
            var request = new RestRequest(Method.POST);

            var byteArray = ReadFully(file);
            


            request.AddFile(name, byteArray, name);
            var response = client.ExecuteTaskAsync(request);
        }
    }
}

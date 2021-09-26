using JYORMCommon.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JYORMClient.Helpers
{
    public class HttpClientHelper
    {
        private readonly static HttpClient Client = new HttpClient();

        public static async Task<T> Get<T>(string url)
        {
            var response = await Client.GetAsync( url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result>(responseBody, new JsonSerializerSettings());
            if (result.Code == (int)ResultCode.Success)
            {
                return JsonConvert.DeserializeObject<T>(result.Data.ToString(), new JsonSerializerSettings());
            }
            return default;
        }
    }
    }

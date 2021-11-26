using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApp.Manager
{
    public class DataBaseController : IDataBaseControler
    {
        private BaseApiOptions _options;
        private WebClient _client;
        public DataBaseController(BaseApiOptions options)
        {
            _options = options;
            _client = new WebClient();
        }

        public async Task<IEnumerable<T>> GetItems<T>(string pointName, Dictionary<string, string> getParams = null)
        {
            try
            {
                string urlService = _options.GetUrlApiService(pointName);
                var paramString = getParams.ToGetParameters();
                var url = new Uri($"{urlService}{paramString}");
                var responseData = await _client.DownloadDataTaskAsync(url);
                var jsonStr = System.Text.Encoding.UTF8.GetString(responseData);
                var result = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<T>>(jsonStr);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<string> SendCommand(string pointName, Dictionary<string, string> getParams = null)
        {
            try
            {
                string urlService = _options.GetUrlApiService(pointName);
                var paramString = getParams.ToGetParameters();
                var url = new Uri($"{urlService}{paramString}");

                var formData = new MultipartFormDataContent();
                var client = new HttpClient();
                var response = await client.PostAsync(url, formData);

                return response.StatusCode.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public class BaseApiOptions
        {
            public string BaseUrl { get; set; }
            public string GetUrlApiService(string controllerName)
            {
                return $"{BaseUrl}/{controllerName}";
            }
        }
    }
}

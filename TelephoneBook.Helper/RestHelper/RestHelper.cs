using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace TelephoneBook.Helper.RestHelper
{
    public class RestHelper
    {
        public static T Get<T>(string apiUrl)
        {
            var client = new HttpClient();
            var uri = new Uri(apiUrl);
            var response = client.GetAsync(uri).Result;
            var responseData = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<T>(responseData);
        }

        public static T Post<T, TK>(string apiUrl, TK model)
        {
            var requestData = JsonConvert.SerializeObject(model);

            var client = new HttpClient();
            var uri = new Uri(apiUrl);
            var content = new StringContent(requestData, Encoding.UTF8, "application/json");

            var response = client.PostAsync(uri, content).Result;
            var responseData = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<T>(responseData);
        }
    }
}

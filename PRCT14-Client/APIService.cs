using System.Text;
using Newtonsoft.Json;

namespace PRCT14_Client.Services
{
    public class APIService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly string _apiBaseUrl = "http://192.168.1.167:5174/";

        public static T Get<T>(string endPoint)
        {
            var response = _httpClient.GetAsync(_apiBaseUrl + endPoint).Result;

            if (!response.IsSuccessStatusCode)
            {
                return default(T);
            }

            var content = response.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<T>(content);
            return data;
        }

        public static string Post<T>(T body, string endpoint)
        {
            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = _httpClient.PostAsync(_apiBaseUrl + endpoint, content).Result;

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException(result.ReasonPhrase);
            }

            return result.ToString();
        }


        public static string Put<T>(T body, int id, string endpoint)
        {
            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = _httpClient.PutAsync(_apiBaseUrl + endpoint + "/" + id.ToString(), content).Result;

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException(result.ReasonPhrase);
            }

            return result.ToString();
        }

        public static string Delete(int id, string endpoint)
        {
            var result = _httpClient.DeleteAsync(_apiBaseUrl + endpoint + "/" + id.ToString()).Result;

            return result.ToString();
        }
    }
}

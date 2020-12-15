using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Restaurant.Services
{
    public static class HttpService
    {
        public static async Task<T> SendAsync<T>(string url, HttpMethod method, HttpContent content = null) where T : new()
        {
            try
            {
                var request = new HttpRequestMessage(method, url);

                if (content != null)
                    request.Content = content;
                var token = Preferences.Get("token", null);
                if (!string.IsNullOrEmpty(token))
                    request.Headers.Authorization =
                        new AuthenticationHeaderValue("Bearer", token);

                using (var client = DefaultHttpClient())
                {
                    var response = await client.SendAsync(request);
                    var body = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(body);
                }
            }
            catch (Exception e)
            {
                return new T();
            }
        }

        public static Task<T> PostApiAsync<T>(string url, object body) where T : new()
        {
            return PostAsync<T>(url, body);
        }

        public static Task<T> PostAsync<T>(string url, object body) where T : new()
        {
            var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");

            return SendAsync<T>(url, HttpMethod.Post, content);
        }

        public static Task<T> PostAsync<T>(string url, Dictionary<string, string> form) where T : new()
        {
            var content = new FormUrlEncodedContent(form);

            return SendAsync<T>(url, HttpMethod.Post, content);
        }

        public static HttpClient DefaultHttpClient()
        {
            return new HttpClient();
        }

        public static Task<T> GetAsync<T>(string url) where T : new()
        {
            return SendAsync<T>(url, HttpMethod.Get);
        }
    }
}

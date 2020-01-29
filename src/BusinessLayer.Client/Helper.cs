using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using Newtonsoft.Json;

namespace BusinessLayer.Client
{
    internal static class SHelper
    {
        static SHelper()
        {
            Url = ConfigurationManager.AppSettings["MainServiceUrl"];
        }

        public static String Url { get; set; }

        public static T Get<T>(String url)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(Url + url).Result;
                String result = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<T>(result);
            }
        }

        public static Boolean Post<TBody>(String url, TBody data)
        {
            using (var client = new HttpClient())
            {
                var content = new ObjectContent<TBody>(data, new JsonMediaTypeFormatter());
                HttpResponseMessage response = client.PostAsync(Url + url, content).Result;
                return response.IsSuccessStatusCode;
            }
        }

        public static HttpResponseMessage PostRaw<TBody>(String url, TBody data)
        {
            using (var client = new HttpClient())
            {

                var content = new ObjectContent<TBody>(data, new JsonMediaTypeFormatter());
                HttpResponseMessage response = client.PostAsync(Url + url, content).Result;
                return response;
            }
        }
    }
}
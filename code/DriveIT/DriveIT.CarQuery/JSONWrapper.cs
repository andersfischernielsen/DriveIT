using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DriveIT.CarQuery
{
    // ReSharper disable once InconsistentNaming
    public static class JSONWrapper
    {
        private const string Uri = @"http://www.carqueryapi.com/api/0.3/";
        private const string GetTrimsParameter = "?&cmd=getTrims&";

        public static async Task<T> Read<T>(string request)
        {
            T received;

            using (var httpClient = new HttpClient())
            {
                try
                {
                    SetUpHttpClient(httpClient);
                    var response = await GetAndEnsureResponse(request, httpClient);
                    received = await response.Content.ReadAsAsync<T>();
                }
                catch (Exception)
                {
                    received = default(T);
                }
            }
            return received;
        }

        private static void SetUpHttpClient(HttpClient client)
        {
            client.BaseAddress = new Uri(Uri);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static async Task<HttpResponseMessage> GetAndEnsureResponse(string request, HttpClient client)
        {
            var response = await client.GetAsync(GetTrimsParameter + request);
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
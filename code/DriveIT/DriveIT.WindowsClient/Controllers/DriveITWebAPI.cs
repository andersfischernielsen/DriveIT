using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DriveIT.WindowsClient.Controllers
{
    // ReSharper disable once InconsistentNaming
    public class DriveITWebAPI
    {
        static private HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5552/api/") };

        public static async Task Login(string username, string password)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5552/api/")};

            var dict = new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"username", username},
                {"password", password}
            };

            var result = await _httpClient.PostAsync("Token", new FormUrlEncodedContent(dict));
            result.EnsureSuccessStatusCode();

            try
            {
                // If these calls throws an exception, a customer has logged in (which is not allowed).
                result = await _httpClient.GetAsync("customers");
                result.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                throw new ArgumentException(string.Format("{0} is not allowed to login to this client", username), "username");
            }

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async static Task Create<T>(string uri, T objectToCreate)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(uri, objectToCreate);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
        }

        public async static Task<IList<T>> ReadList<T>(string uri)
        {
            T[] objects;
            try
            {
                var response = await _httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                objects = await response.Content.ReadAsAsync<T[]>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new NotImplementedException();
            }
            return objects.ToList();
        }

        public async static Task<T> Read<T>(string uri)
        {
            T objectToRead;
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                objectToRead = await response.Content.ReadAsAsync<T>();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
            return objectToRead;
        }


        public async static Task Update<T>(string uri, T objectToUpdate)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(uri, objectToUpdate);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
        public async static Task Delete<T>(string uri)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(uri);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                throw new NotImplementedException();
            }
        }
    }
}

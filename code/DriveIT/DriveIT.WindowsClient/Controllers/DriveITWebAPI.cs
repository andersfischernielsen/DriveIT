using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DriveIT.WindowsClient.Controllers
{
    public class DriveITWebAPI
    {
        static string apiUrl = @"http://localhost:5552/api/";
        static private HttpClient _httpClient = new HttpClient();
        // todo REMOVE BOOL WHEN LOGIN SCREEN IS DONE
        static private bool test = false;

        public static async Task Login(string username, string password)
        {
            var dict = new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"username", username},
                {"password", password}
            };
            var result = await _httpClient.PostAsync(@"http://localhost:5552/" + "Token", new FormUrlEncodedContent(dict));
            _httpClient.BaseAddress = new Uri(apiUrl);
            result.EnsureSuccessStatusCode();
        }

        public async static Task Create<T>(string uri, T objectToCreate)
        {
                try
                {
                    _httpClient.BaseAddress = new Uri(apiUrl);
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await _httpClient.PostAsJsonAsync(uri, objectToCreate);
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
            if (!test)
            {
                // todo REMOVE WHEN LOGIN SCREEN IS DONE
                _httpClient.BaseAddress = new Uri(apiUrl);
                test = true;
            }
            T[] objects = null;
                    try
                    {
                        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage response = _httpClient.GetAsync(uri).Result;
                        response.EnsureSuccessStatusCode();
                        objects = await response.Content.ReadAsAsync<T[]>();
                    }
                    catch (Exception)
                    {
                        throw new NotImplementedException();
                    } 
            return objects.ToList();
        }

        public async static Task<T> Read<T>(string uri)
        {
            T objectToRead;
                try
                {
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = _httpClient.GetAsync(uri).Result;
                    response.EnsureSuccessStatusCode();
                    objectToRead = await response.Content.ReadAsAsync<T>();
                }
                catch (Exception)
                {
                    throw new NotImplementedException();
                }
            return objectToRead;
        }


        public async static Task Update<T>(string uri, T objectToUpdate, int id)
        {
                try
                {
                    HttpResponseMessage response = await _httpClient.PutAsJsonAsync(uri + "/" + id, objectToUpdate);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception)
                {

                    throw new NotImplementedException();
                }
        }
        public async static Task Delete<T>(string uri, int id)
        {
                try
                {
                    HttpResponseMessage response = await _httpClient.DeleteAsync(uri + "/" + id);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception)
                {

                    throw new NotImplementedException();
                }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DriveIT.Models;
using DriveIT_Windows_Client.ViewModels;
using Newtonsoft.Json;

namespace DriveIT_Windows_Client.Controllers
{
    public class DriveITWebAPI
    {
        static string apiUrl = @"http://localhost:5552";

        public async static Task Create<T>(string uri, T objectToCreate)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    httpClient.BaseAddress = new Uri(apiUrl);
                    HttpResponseMessage response = await httpClient.PostAsJsonAsync("/api/" + uri, objectToCreate);
                    response.EnsureSuccessStatusCode();
                    var objects = await response.Content.ReadAsAsync<T[]>();
                    objects.ToList().ForEach(i => Console.WriteLine(i));
                }
                catch (Exception)
                {
                    throw new Exception();
                }

            }
        }

        public async static Task<IList<T>> Read<T>(string uri)
        {
            T[] objects;
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(apiUrl);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
                    HttpResponseMessage response = await httpClient.GetAsync("/api/" + uri);
                    response.EnsureSuccessStatusCode();
                    objects = await response.Content.ReadAsAsync<T[]>();
                    objects.ToList().ForEach(i => Console.WriteLine(i));
                    httpClient.Dispose();
                }
            return objects.ToList();
        }
        public async static Task Update<T>(string uri, T objectToUpdate)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    httpClient.BaseAddress = new Uri(apiUrl);
                    HttpResponseMessage response = await httpClient.PutAsJsonAsync("/api/" + uri, objectToUpdate);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception)
                {

                    throw new Exception();
                }

            }
        }
        public async static Task Delete<T>(string uri)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    httpClient.BaseAddress = new Uri(apiUrl);
                    HttpResponseMessage response = await httpClient.DeleteAsync("/api/" + uri);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception)
                {

                    throw new Exception();
                }

            }
        }

    }
}

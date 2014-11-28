using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        public static void Create(string uri, CarDetailDto car)
        {
            using (WebClient webClient = new WebClient())
            {
                string carSeri = JsonConvert.SerializeObject(car);
                
                webClient.UploadString(new Uri(apiUrl + uri), carSeri);
            }
        }

        public async static Task<IList<T>> Read<T>(string uri)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(apiUrl);
                HttpResponseMessage response = await httpClient.GetAsync("/api/" + uri);
                response.EnsureSuccessStatusCode();
                var objects = await response.Content.ReadAsAsync<T[]>();
                objects.ToList().ForEach(i => Console.WriteLine(i));
                return objects;
            }
        }



    }
}

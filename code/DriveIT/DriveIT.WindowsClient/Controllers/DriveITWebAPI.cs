﻿using System;
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
        static string apiUrl = @"http://localhost:5552/api/";

        public async static Task Create<T>(string uri, T objectToCreate)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    httpClient.BaseAddress = new Uri(apiUrl);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await httpClient.PostAsJsonAsync(uri, objectToCreate);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception)
                {
                    throw new NotImplementedException();
                }

            }
        }

        public async static Task<IList<T>> ReadList<T>(string uri)
        {
            T[] objects = null;
                using (HttpClient httpClient = new HttpClient())
                {
                    try
                    {
                        httpClient.BaseAddress = new Uri(apiUrl);
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage response = httpClient.GetAsync(uri).Result;
                        response.EnsureSuccessStatusCode();
                        objects = await response.Content.ReadAsAsync<T[]>();
                        objects.ToList().ForEach(i => Console.WriteLine(i));
                    }
                    catch (Exception)
                    {
                        throw new NotImplementedException();
                    } 
                    
                }
            return objects.ToList();
        }

        public async static Task<T> Read<T>(string uri)
        {
            T objectToRead;
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    httpClient.BaseAddress = new Uri(apiUrl);
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.GetAsync(uri).Result;
                    response.EnsureSuccessStatusCode();
                    objectToRead = await response.Content.ReadAsAsync<T>();
                    Console.WriteLine(objectToRead);
                }
                catch (Exception)
                {
                    throw new NotImplementedException();
                }

            }
            return objectToRead;
        }


        public async static Task Update<T>(string uri, T objectToUpdate, int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    httpClient.BaseAddress = new Uri(apiUrl);
                    HttpResponseMessage response = await httpClient.PutAsJsonAsync(uri + "/" + id, objectToUpdate);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception)
                {

                    throw new NotImplementedException();
                }

            }
        }
        public async static Task Delete<T>(string uri, int id)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    httpClient.BaseAddress = new Uri(apiUrl);
                    HttpResponseMessage response = await httpClient.DeleteAsync(uri + "/" + id);
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception)
                {

                    throw new NotImplementedException();
                }

            }
        }

    }
}
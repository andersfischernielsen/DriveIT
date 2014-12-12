using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using DriveIT.Models;
using DriveIT.WindowsClient.ViewModels;

namespace DriveIT.WindowsClient.Controllers
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// A class which creates HTTP requests calls: POST, PUT, READ and DELETE.
    /// The methods uses generics, and strings in parameters and thereby allowing the code to be reused.
    /// </summary>
    public class DriveITWebAPI
    {
        static private HttpClient _httpClient;

        /// <summary>
        /// Logs into the windows client if user is of role Employee or Administrator. This sets the header of the ongoing http requests.
        /// When logged in the role of the username is checked and if its customer profile an exception occurs.
        /// </summary>
        /// <param name="username">The username of the user</param>
        /// <param name="password">The password of the user</param>
        /// <returns>Returns the Task indicating whether it is completed or not</returns>
        public static async Task Login(string username, string password)
        {
            #if DEBUG
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:36774/api/") };
            #else
            _httpClient = new HttpClient { BaseAddress = new Uri("http://driveit.azurewebsites.net/api/") };
            #endif

            var dict = new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"username", username},
                {"password", password}
            };

            var result = await _httpClient.PostAsync("Token", new FormUrlEncodedContent(dict));
            result.EnsureSuccessStatusCode();

            var token = await result.Content.ReadAsAsync<Token>();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.access_token);
            var role = await GetRole();
            if (role == null || role == Role.Customer)
            {
                throw new Exception("An error occurred while logging into the client...");
            }
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Creates a Post httprequest with the generic T to the webAPI at the url BaseAddress + uri. 
        /// T and the uri string must match.
        /// </summary>
        /// <typeparam name="T"> An object matching the expected object in the API at url (BaseAddress+Uri)</typeparam>
        /// <param name="uri">The uri of the api where T objects are stored</param>
        /// <param name="objectToCreate"> the object to create at the APi</param>
        /// <returns>The object which was created at the API</returns>
        public async static Task<T> Create<T>(string uri, T objectToCreate)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(uri, objectToCreate);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsAsync<T>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Reads all the objects of type T at the webAPI on the string BaseAddress + uri. 
        /// T and the uri string must match.
        /// </summary>
        /// <typeparam name="T"> An object matching the expected object in the API at url (BaseAddress+Uri)</typeparam>
        /// <param name="uri">The uri of the api where T objects are stored</param>
        /// <returns>All T objects in the API using the URI</returns>
        public async static Task<IList<T>> ReadList<T>(string uri)
        {
            T[] objects;
            try
            {
                var response = await _httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                objects = await response.Content.ReadAsAsync<T[]>();
            }
            catch (Exception)
            {
                throw;
            }
            return objects.ToList();
        }
        /// <summary>
        /// Reads the object of type T at the webAPI on the string BaseAddress + uri. 
        /// T and the uri string must match.
        /// </summary>
        /// <typeparam name="T"> An object matching the expected object in the API at url (BaseAddress+Uri)</typeparam>
        /// <param name="uri">The uri of the api where a single T object is stored</param>
        /// <returns>An T object in the API using the URI</returns>
        public async static Task<T> Read<T>(string uri)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                T objectToRead = await response.Content.ReadAsAsync<T>();
                return objectToRead;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Creates a Put httprequest with the generic T to the webAPI at the url BaseAddress + uri. 
        /// T and the uri string must match.
        /// </summary>
        /// <typeparam name="T"> An object matching the expected object in the API at url (BaseAddress+Uri)</typeparam>
        /// <param name="uri">The uri of the api where T objects are stored</param>
        /// <param name="objectToCreate"> the object to update at the APi with an ID</param>
        /// <returns>A Task to await</returns>
        public async static Task Update<T>(string uri, T objectToUpdate)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(uri, objectToUpdate);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Creates a Delete httprequest to the webAPI at the url BaseAddress + uri. 
        /// </summary>
        /// <param name="uri">The uri of the API indicating a single object</param>
        /// <returns>A Task to await</returns>
        public async static Task Delete<T>(string uri)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(uri);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Calls the API and finds out what role the logged in user is.
        /// This must be called after the Login method.  
        /// </summary>
        /// <returns>The role which is matches the logged in user</returns>
        private static async Task<Role?> GetRole()
        {
            try
            {
                var result = await _httpClient.GetAsync("account/isadministrator");
                if (result.IsSuccessStatusCode)
                {
                    EmployeeDetailsViewModel.LoggedInRole = Role.Administrator;
                    return Role.Administrator;
                }
                result = await _httpClient.GetAsync("account/isemployee");
                if (result.IsSuccessStatusCode)
                {
                    EmployeeDetailsViewModel.LoggedInRole = Role.Employee;
                    return Role.Employee;
                }
                result = await _httpClient.GetAsync("account/iscustomer");
                if (result.IsSuccessStatusCode) return Role.Customer;
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

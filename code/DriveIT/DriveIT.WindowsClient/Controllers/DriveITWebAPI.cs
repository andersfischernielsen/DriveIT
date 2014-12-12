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
    public class DriveITWebAPI
    {
        static private HttpClient _httpClient;

        /// <summary>
        /// Logs into the windows client if user is of role Employee or Administrator.
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
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="objectToCreate"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="objectToCreate"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async static Task Create(string uri, CustomerDto objectToCreate, string password)
        {
            try
            {
                var registerModel = new RegisterViewModel
                {
                    Email = objectToCreate.Email,
                    FirstName = objectToCreate.FirstName,
                    LastName = objectToCreate.LastName,
                    PhoneNumber = objectToCreate.Phone,
                    ConfirmPhoneNumber = objectToCreate.Phone,
                    Password = password,
                    ConfirmPassword = password,
                    Role = Role.Customer
                };
                var response = await _httpClient.PostAsJsonAsync(uri, registerModel);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="objectToCreate"></param>
        /// <param name="password"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async static Task Create(string uri, EmployeeDto objectToCreate, string password, Role role)
        {
            if (role == Role.Customer) throw new ArgumentException("Cannot be Customer.", "role");
            try
            {
                var registerModel = new RegisterViewModel
                {
                    Email = objectToCreate.Email,
                    FirstName = objectToCreate.FirstName,
                    LastName = objectToCreate.LastName,
                    PhoneNumber = objectToCreate.Phone,
                    ConfirmPhoneNumber = objectToCreate.Phone,
                    Password = password,
                    ConfirmPassword = password,
                    Role = role
                };
                var response = await _httpClient.PostAsJsonAsync(uri, registerModel);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <returns></returns>
        public async static Task<T> Read<T>(string uri)
        {
            T objectToRead;
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                objectToRead = await response.Content.ReadAsAsync<T>();
                return objectToRead;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="objectToUpdate"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <param name="phone"></param>
        /// <param name="confirmPhone"></param>
        /// <returns></returns>
        public async static Task CreateCustomer(string email, string firstName, string lastName, string password,
            string confirmPassword, string phone, string confirmPhone)
        {
            try
            {
                await CreateUser(email, firstName, lastName, password, confirmPassword, phone, confirmPhone, Role.Customer);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <param name="phone"></param>
        /// <param name="confirmPhone"></param>
        /// <returns></returns>
        public async static Task CreateEmployee(string email, string firstName, string lastName, string password,
            string confirmPassword, string phone, string confirmPhone)
        {
            try
            {
                if (await GetRole() != Role.Administrator) throw new Exception("Access denied");
                await CreateUser(email, firstName, lastName, password, confirmPassword, phone, confirmPhone, Role.Employee);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <param name="phone"></param>
        /// <param name="confirmPhone"></param>
        /// <returns></returns>
        public async static Task CreateAdministrator(string email, string firstName, string lastName, string password,
            string confirmPassword, string phone, string confirmPhone)
        {
            try
            {
                if (await GetRole() != Role.Administrator) throw new Exception("Access denied");
                await CreateUser(email, firstName, lastName, password, confirmPassword, phone, confirmPhone, Role.Administrator);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="password"></param>
        /// <param name="confirmPassword"></param>
        /// <param name="phone"></param>
        /// <param name="confirmPhone"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        private async static Task CreateUser(string email, string firstName, string lastName, string password, string confirmPassword, string phone, string confirmPhone, Role? role)
        {
            HttpResponseMessage result;
            var model = new RegisterViewModel
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                ConfirmPassword = confirmPassword,
                PhoneNumber = phone,
                ConfirmPhoneNumber = confirmPhone,
                Role = role
            };
            result = await _httpClient.PostAsJsonAsync("account/register", model);

            try
            {
                result.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException)
            {
                if (result.StatusCode == HttpStatusCode.Unauthorized)
                {
                    //Maybe throw some special exception
                    throw;
                }
                //else
                throw;
            }
        }
    }
}

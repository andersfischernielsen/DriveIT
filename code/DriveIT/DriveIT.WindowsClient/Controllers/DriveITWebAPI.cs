using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using DriveIT.Models;

namespace DriveIT.WindowsClient.Controllers
{
// ReSharper disable once InconsistentNaming
    public class DriveITWebAPI
    {
        static private HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5552/api/") };


        public static async Task Login(string username, string password)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5552/api/") };

            var dict = new Dictionary<string, string>
            {
                {"grant_type", "password"},
                {"username", username},
                {"password", password}
            };

            var result = await _httpClient.PostAsync("Token", new FormUrlEncodedContent(dict));
            result.EnsureSuccessStatusCode();

            if (await GetRole() == Role.Customer)
            {
                throw new Exception("An error occurred while logging into the client...");
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
            catch (Exception)
            {
                //Console.WriteLine(ex.Message);
                //ErrorMessagePopUp();
                throw;
            }
        }

        public async static Task<IList<T>> ReadList<T>(string uri)
        {
            // TODO FJERN INITIERINGEN new T[0]
            T[] objects = new T[0];
            try
            {
                var response = await _httpClient.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                objects = await response.Content.ReadAsAsync<T[]>();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                // TODO Håndter dette
                //ErrorMessagePopUp();
                throw;
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
                return objectToRead;
            }
            catch (Exception)
            {
                //ErrorMessagePopUp();
                throw;
            }
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
                //ErrorMessagePopUp();
                throw;
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
                //ErrorMessagePopUp();
                throw;
            }
        }

        public async static Task CreateCustomer(string email, string firstName, string lastName, string password,
            string confirmPassword, string phone, string confirmPhone)
        {
            await CreateUser(email, firstName, lastName, password, confirmPassword, phone, confirmPhone, Role.Customer);
        }

        public async static Task CreateEmployee(string email, string firstName, string lastName, string password,
            string confirmPassword, string phone, string confirmPhone)
        {
            if (await GetRole() != Role.Administrator) throw new Exception("Access denied");
            await CreateUser(email, firstName, lastName, password, confirmPassword, phone, confirmPhone, Role.Employee);
        }

        public async static Task CreateAdministrator(string email, string firstName, string lastName, string password,
            string confirmPassword, string phone, string confirmPhone)
        {
            if (await GetRole() != Role.Administrator) throw new Exception("Access denied");
            await CreateUser(email, firstName, lastName, password, confirmPassword, phone, confirmPhone, Role.Administrator);
        }

        private static async Task<Role?> GetRole()
        {
            var result = await _httpClient.GetAsync("account/isadministrator");
            if (result.IsSuccessStatusCode) return Role.Administrator;
            result = await _httpClient.GetAsync("account/isemployee");
            if (result.IsSuccessStatusCode) return Role.Employee;
            result = await _httpClient.GetAsync("account/iscustomer");
            if (result.IsSuccessStatusCode) return Role.Customer;
            return null;
        }

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
        private static void ErrorMessagePopUp()
        {

            var response = MessageBox.Show("There was an error processing your request...", "Error!",
                MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public static async Task<String> UploadImage(byte[] imageData, MediaTypeHeaderValue type)
        {
            var content = new MultipartFormDataContent();
            var b = new ByteArrayContent(imageData);
            b.Headers.ContentType = type;
            content.Add(b);
            
            var message = await _httpClient.PostAsync("upload", content);
            message.EnsureSuccessStatusCode();
            return (await message.Content.ReadAsAsync<List<String>>()).SingleOrDefault();
        }
    }
}

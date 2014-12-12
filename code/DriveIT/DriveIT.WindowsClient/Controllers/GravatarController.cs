using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveIT.WindowsClient.Controllers
{
    public static class GravatarController
    {
        /// <summary>
        /// Creates a link to the gravatar of a user from the given email.
        /// </summary>
        /// <param name="email">The email for creating a link to the belonging persons gravatar</param>
        /// <returns>Returns a link to the given users gravatar</returns>
        public static string CreateGravatarLink(string email)
        {
            return "http://www.gravatar.com/avatar/" + CreateMD5Hash(email) + "?s=250";
        }
        /// <summary>
        /// Creates the MD5 hash from a given email.
        /// </summary>
        /// <param name="input">A gravatar users email to be hashed</param>
        /// <returns>returns the MD5 from the given email, later to be used for creating a link</returns>
        private static string CreateMD5Hash(string input)
        {
            // Use input string to calculate MD5 hash
            var md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}

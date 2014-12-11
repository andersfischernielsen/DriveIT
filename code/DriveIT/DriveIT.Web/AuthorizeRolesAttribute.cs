using System.Linq;
using System.Web.Http;
using DriveIT.Models;

namespace DriveIT.Web
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params Role[] roles)
        {
            Roles = string.Join(",", roles.Select(role => role.ToString()));
        }
    }
}
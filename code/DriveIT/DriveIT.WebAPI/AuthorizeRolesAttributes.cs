using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using DriveIT.Entities;

namespace DriveIT.WebAPI
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params Role[] roles)
        {
            Roles = string.Join(",", roles.Select(role => role.ToString()));
        }
    }
}
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using DriveIT.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DriveIT.EntityFramework
{
    // ReSharper disable once InconsistentNaming
    public class DriveITUser : IdentityUser
    {
        private int _customerId;
        private int _employeeId;

        public int CustomerId
        {
            get { return _customerId; }
            set
            {
                if (IsEmployee()) throw new InvalidOperationException("Can't be both customer and employee");
                _customerId = value;
            }
        }
        public virtual Customer Customer { get; set; }
        public int EmployeeId
        {
            get { return _employeeId; }
            set
            {
                if (IsCustomer()) throw new InvalidOperationException("Can't be both customer and employee");
                _customerId = value;
            }
        }
        public virtual Employee Employee { get; set; }

        public bool IsCustomer()
        {
            return CustomerId > 0;
        }

        public bool IsEmployee()
        {
            return EmployeeId > 0;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<DriveITUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}

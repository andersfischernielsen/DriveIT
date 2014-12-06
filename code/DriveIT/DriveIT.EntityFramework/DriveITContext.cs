using System.Data.Entity;
using System.Linq;
using DriveIT.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DriveIT.EntityFramework
{
// ReSharper disable once InconsistentNaming
    public class DriveITContext : IdentityDbContext<DriveITUser>
    {
        public DriveITContext() : base("DriveIT.EntityFramework.DriveITContext")
        {
            Database.CommandTimeout = 60;
        }

        public virtual DbSet<Car> Cars { get; set; }

        public IQueryable<Customer> Customers
        {
            get
            {
                return from customer in Users.OfType<Customer>()
                    select customer;
            }
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<ContactRequest> ContactRequests { get; set; }

        public IQueryable<Employee> Employees
        {
            get
            {
                return from employee in Users.OfType<Employee>()
                    select employee;
            }
        }

        public virtual DbSet<Sale> Sales { get; set; }
    }
}

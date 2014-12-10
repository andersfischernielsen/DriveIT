using System.Data.Entity;
using System.Linq;
using DriveIT.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DriveIT.EntityFramework
{
// ReSharper disable once InconsistentNaming
    public class DriveITContext : IdentityDbContext<DriveITUser>
    {
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<ContactRequest> ContactRequests { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<ImagePath> ImagePaths { get; set; }
        public IQueryable<Employee> Employees
        {
            get
            {
                return from employee in Users.OfType<Employee>()
                    select employee;
            }
        }
        public IQueryable<Customer> Customers
        {
            get
            {
                return from customer in Users.OfType<Customer>()
                       select customer;
            }
        }
        public DriveITContext()
            : base("DriveIT.EntityFramework.DriveITContext")
        {
            Database.CommandTimeout = 60;
        }
    }
}

using System.Data.Entity;
using DriveIT.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DriveIT.EntityFramework
{
// ReSharper disable once InconsistentNaming
    public class DriveITContext : IdentityDbContext<DriveITUser>
    {
        public DriveITContext() : base("DriveIT.EntityFramework.DriveITContext") { }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<ContactRequest> ContactRequests { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
    }
}

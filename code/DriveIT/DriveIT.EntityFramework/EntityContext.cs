using System.Data.Entity;
using DriveIT.Entities;

namespace DriveIT.EntityFramework
{
// ReSharper disable once InconsistentNaming
    public class EntityContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Sale> Sales { get; set; }
    }
}

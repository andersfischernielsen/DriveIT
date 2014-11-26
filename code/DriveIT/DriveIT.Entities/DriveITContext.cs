using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveIT.Entities
{
// ReSharper disable once InconsistentNaming
    class DriveITContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Sale> Sales { get; set; }
    }
}

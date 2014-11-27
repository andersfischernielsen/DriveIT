using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DriveIT.Entities;

namespace DriveIT.EntityFramework
{
// ReSharper disable once InconsistentNaming
    public interface DriveITContext : IDisposable
    {
        DbSet<Car> Cars { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<ContactRequest> ContactRequests { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<Sale> Sales { get; set; }

        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

using System.Data.Entity;
using System.Linq;
using DriveIT.EntityFramework.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DriveIT.EntityFramework
{
// ReSharper disable once InconsistentNaming
    public class DriveITContext : IdentityDbContext<DriveITUser>
    {
        /// <summary>
        /// This makes sure that the relation between Car and ImagePath entities is set correctly.
        /// One Car can have many ImagePaths, one ImagePath must have a Car, and the Car Id is a 
        /// foreign key.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Car>()
                .HasMany(car => car.ImagePaths)
                .WithRequired(imagePath => imagePath.Car)
                .HasForeignKey(imagePath => imagePath.CarId);
        }

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<ContactRequest> ContactRequests { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<ImagePath> ImagePaths { get; set; }
        public IQueryable<Employee> Employees
        {
            get
            {
                //Check the inherited Users IDbSet in the IdentityDbContext for Employees. Return these.
                return from employee in Users.OfType<Employee>()
                    select employee;
            }
        }
        public IQueryable<Customer> Customers
        {
            get
            {
                //Check the inherited Users IDbSet in the IdentityDbContext for Customers. Return these.
                return from customer in Users.OfType<Customer>()
                       select customer;
            }
        }
        public DriveITContext()
            : base("DriveIT.EntityFramework.DriveITContext")
        {
            //Set the timeout to one minute. This is to avoid timeouts during migrations.
            Database.CommandTimeout = 60;
        }
    }
}

using DriveIT.Entities;

namespace DriveIT.EntityFramework.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<DriveITContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DriveITContext context)
        {
            context.Cars.AddOrUpdate(
                c => c.Id, 
                new Car
                {
                    Id = 1,
                    Make = "Ford"
                });
        }
    }
}

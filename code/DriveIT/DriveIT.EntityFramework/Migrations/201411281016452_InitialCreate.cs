namespace DriveIT.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Model = c.String(),
                        Make = c.String(),
                        Year = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Created = c.DateTime(nullable: false),
                        Sold = c.Boolean(nullable: false),
                        Mileage = c.Single(nullable: false),
                        Color = c.String(),
                        DistanceDriven = c.Int(nullable: false),
                        Fuel = c.String(),
                        Drive = c.String(),
                        Transmission = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Car_Id = c.Int(),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.Car_Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.Long(nullable: false),
                        Email = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ContactRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        Car_Id = c.Int(),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.Car_Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateOfSale = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Car_Id = c.Int(),
                        Customer_Id = c.Int(),
                        Employee_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.Car_Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.Customers", t => t.Employee_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "Employee_Id", "dbo.Customers");
            DropForeignKey("dbo.Sales", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Sales", "Car_Id", "dbo.Cars");
            DropForeignKey("dbo.ContactRequests", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.ContactRequests", "Car_Id", "dbo.Cars");
            DropForeignKey("dbo.Comments", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Comments", "Car_Id", "dbo.Cars");
            DropTable("dbo.Sales");
            DropTable("dbo.ContactRequests");
            DropTable("dbo.Customers");
            DropTable("dbo.Comments");
            DropTable("dbo.Cars");
        }
    }
}

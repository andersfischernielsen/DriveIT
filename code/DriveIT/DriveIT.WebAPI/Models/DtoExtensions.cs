using DriveIT.Entities;
using DriveIT.EntityFramework;
using DriveIT.Models;

namespace DriveIT.WebAPI.Models
{
    public static class DtoExtensions
    {
        public static Car ToCar(this CarDto dto)
        {
            return new Car
            {
                Color = dto.Color,
                Created = dto.Created,
                DistanceDriven = dto.DistanceDriven,
                Fuel = dto.Fuel.ToString(),
                Id = dto.Id,
                Make = dto.Make,
                Model = dto.Model,
                Price = dto.Price,
                Sold = dto.Sold,
                Transmission = dto.Transmission,
                Year = dto.Year
            };
        }

        public static Car ToCar(this CarDetailDto dto)
        {
            return new Car
            {
                Color = dto.Color,
                Created = dto.Created,
                DistanceDriven = dto.DistanceDriven,
                Fuel = dto.Fuel.ToString(),
                Id = dto.Id,
                Make = dto.Make,
                Model = dto.Model,
                Price = dto.Price,
                Sold = dto.Sold,
                Transmission = dto.Transmission,
                Year = dto.Year,
                Drive = dto.Drive,
                Mileage = dto.Mileage
            };
        }

        public static Customer ToCustomer(this CustomerDto dto)
        {
            return new Customer
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Id = dto.Id,
                PhoneNumber = dto.Phone,
                Username = dto.Username
            };
        }

        public static Sale ToSale(this SaleDto dto, IPersistentStorage repo)
        {
            return new Sale
            {
                //TODO: Inform Fischer that ID isn't necessary for a Sale.
                DateOfSale = dto.Sold,
                Price = dto.Price,
                Car = repo.GetCarWithId(dto.CarId),
                Customer = repo.GetCustomerWithId(dto.CustomerId),
                Employee = repo.GetEmployeeWithId(dto.EmployeeId)
            };
        }
    }
}
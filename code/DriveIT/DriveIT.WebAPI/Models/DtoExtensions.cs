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
                Id = dto.Id.HasValue ? dto.Id.Value : 0,
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
                Id = dto.Id.HasValue ? dto.Id.Value : 0,
                //Todo fix.
                PhoneNumber = long.Parse(dto.Phone),
                Username = dto.Username
            };
        }

        public static Sale ToSale(this SaleDto dto, IPersistentStorage repo)
        {
            return new Sale
            {
                Id = dto.Id.HasValue ? dto.Id.Value : 0,
                DateOfSale = dto.Sold,
                Price = dto.Price,
                Car = repo.GetCarWithId(dto.CarId),
                Customer = repo.GetCustomerWithId(dto.CustomerId),
                Employee = repo.GetEmployeeWithId(dto.EmployeeId)
            };
        }

        public static Comment ToComment(this CommentDto dto, IPersistentStorage repo)
        {
            return new Comment
            {
                Id = dto.Id.HasValue ? dto.Id.Value : 0,
                Car = repo.GetCarWithId(dto.CarId),
                Customer = repo.GetCustomerWithId(dto.CustomerId),
                Description = dto.Description,
                Title = dto.Title,
                DateCreated = dto.Date
            };
        }
    }
}
using System;
using System.Threading.Tasks;
using DriveIT.Entities;
using DriveIT.EntityFramework;
using DriveIT.Models;

namespace DriveIT.WebAPI.Models
{
    public static class DtoExtensions
    {

        public static Car ToEntity(this CarDto dto)
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

        public static CarDto ToDto(this Car car)
        {
            return new CarDto
            {
                Color = car.Color,
                Created = car.Created,
                DistanceDriven = car.DistanceDriven,
                Id = car.Id,
                Make = car.Make,
                Model = car.Model,
                Price = car.Price,
                Sold = car.Sold,
                Transmission = car.Transmission,
                Year = car.Year,
                Fuel = (FuelType)Enum.Parse(typeof(FuelType), car.Fuel),
                Drive = car.Drive,
                Mileage = car.Mileage
            };
        }

        public static Customer ToEntity(this CustomerDto dto)
        {
            return new Customer
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Id = dto.Id.HasValue ? dto.Id.Value : 0,
                //Todo fix.
                PhoneNumber = dto.Phone,
                Username = dto.Username
            };
        }

        public static CustomerDto ToDto(this Customer customer)
        {
            return new CustomerDto
            {
                Id = customer.Id,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Phone = string.Format("{0}", customer.PhoneNumber),
                Username = customer.Username
            };
        }

        public static async Task<Sale> ToEntity(this SaleDto dto, IPersistentStorage repo)
        {
            return new Sale
            {
                Id = dto.Id.HasValue ? dto.Id.Value : 0,
                DateOfSale = dto.Sold,
                Price = dto.Price,
                Car = await repo.GetCarWithId(dto.CarId),
                Customer = await repo.GetCustomerWithId(dto.CustomerId),
                Employee = await repo.GetEmployeeWithId(dto.EmployeeId)
            };
        }

        public static SaleDto ToDto(this Sale sale)
        {
            return new SaleDto
            {
                CarId = sale.Car.Id,
                CustomerId = sale.Customer.Id,
                EmployeeId = sale.Employee.Id,
                Id = sale.Id,
                Price = sale.Price,
                Sold = sale.DateOfSale
            };
        }

        public static async Task<Comment> ToEntity(this CommentDto dto, IPersistentStorage repo)
        {
            return new Comment
            {
                Id = dto.Id.HasValue ? dto.Id.Value : 0,
                Car = await repo.GetCarWithId(dto.CarId),
                Customer = await repo.GetCustomerWithId(dto.CustomerId),
                Description = dto.Description,
                Title = dto.Title,
                DateCreated = dto.Date
            };
        }

        public static CommentDto ToDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                CarId = comment.Car.Id,
                CustomerId = comment.Customer.Id,
                Date = comment.DateCreated,
                Description = comment.Description,
                Title = comment.Title
            };
        }

        public static async Task<ContactRequest> ToEntity(this ContactRequestDto dto, IPersistentStorage repo)
        {
            return new ContactRequest
            {
                Id = dto.Id.HasValue ? dto.Id.Value : 0,
                Car = await repo.GetCarWithId(dto.CarId),
                Customer = await repo.GetCustomerWithId(dto.CustomerId),
                Created = dto.Requested
            };
        }

        public static ContactRequestDto ToDto(this ContactRequest contactRequest)
        {
            return new ContactRequestDto
            {
                Id = contactRequest.Id,
                CarId = contactRequest.Car.Id,
                CustomerId = contactRequest.Customer.Id,
                Requested = contactRequest.Created,
                //TODO: employee / handled.
            };
        }
    }
}
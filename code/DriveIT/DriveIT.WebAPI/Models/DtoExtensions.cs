﻿using System;
using DriveIT.Entities;
using DriveIT.Models;
using _repo = DriveIT.EntityFramework.EntityStorage;

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
                Id = dto.Id ?? 0,
                Make = dto.Make,
                Model = dto.Model,
                Price = dto.Price,
                Sold = dto.Sold,
                Transmission = dto.Transmission,
                Year = dto.Year,
                Drive = dto.Drive,
                Mileage = dto.Mileage,
                ImagePaths = dto.ImagePaths,
                NoughtTo100 = dto.NoughtTo100,
                TopSpeed = dto.TopSpeed
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
                Mileage = car.Mileage,
                ImagePaths = car.ImagePaths,
                TopSpeed = car.TopSpeed,
                NoughtTo100 = car.NoughtTo100
            };
        }

        public static Customer ToEntity(this CustomerDto dto)
        {
            return new Customer
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Id = dto.Id ?? 0,
                PhoneNumber = dto.Phone,
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
            };
        }

        public static Sale ToEntity(this SaleDto dto)
        {
            return new Sale
            {
                Id = dto.Id ?? 0,
                DateOfSale = dto.Sold,
                Price = dto.Price,
                CarId = dto.CarId,
                CustomerId = dto.CustomerId,
                EmployeeId = dto.EmployeeId
            };
        }

        public static SaleDto ToDto(this Sale sale)
        {
            return new SaleDto
            {
                CarId = sale.CarId,
                CustomerId = sale.CustomerId,
                EmployeeId = sale.EmployeeId,
                Id = sale.Id,
                Price = sale.Price,
                Sold = sale.DateOfSale
            };
        }

        public static Comment ToEntity(this CommentDto dto)
        {
            return new Comment
            {
                Id = dto.Id.HasValue ? dto.Id.Value : 0,
                CarId = dto.CarId,
                CustomerId = dto.CustomerId,
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
                CarId = comment.CarId,
                CustomerId = comment.CustomerId,
                Date = comment.DateCreated,
                Description = comment.Description,
                Title = comment.Title
            };
        }

        public static ContactRequest ToEntity(this ContactRequestDto dto)
        {
            return new ContactRequest
            {
                Id = dto.Id.HasValue ? dto.Id.Value : 0,
                CarId = dto.CarId,
                CustomerId = dto.CustomerId,
                Created = dto.Requested,
                EmployeeId = dto.EmployeeId ?? 0
            };
        }

        public static ContactRequestDto ToDto(this ContactRequest contactRequest)
        {
            return new ContactRequestDto
            {
                Id = contactRequest.Id,
                CarId = contactRequest.CarId,
                CustomerId = contactRequest.CustomerId,
                Requested = contactRequest.Created,
                EmployeeId = contactRequest.EmployeeId == 0 ? (int?) null : contactRequest.EmployeeId
            };
        }

        public static Employee ToEntity(this EmployeeDto dto)
        {
            return new Employee
            {
                Id = dto.Id ?? 0,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.Phone,
                Email = dto.Email
            };
        }

        public static EmployeeDto ToDto(this Employee employee)
        {
            return new EmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Phone = employee.PhoneNumber,
                Email = employee.Email
            };
        }
    }
}
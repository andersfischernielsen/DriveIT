using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using DriveIT.EntityFramework.Entities;
using Moq;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace DriveIT.EntityFramework.Tests
{
    [TestFixture]
    public class EntityStorageTests
    {
        private IPersistentStorage _toTest;
        private Mock<DbSet<Sale>> _mockSet;
        private Mock<DriveITContext> _mockContext;

        [TestFixtureSetUp]
        public void SetUp()
        {
            _toTest = new EntityStorage();

            var sales = new List<Sale> 
            { 
                new Sale
                {
                    Id = 1,
                    Price = 20000,
                    CarId = 1, 
                    CustomerId = "customer",
                    DateOfSale = DateTime.Now,
                    EmployeeId = "employee"
                }, 
                new Sale
                {
                    Id = 2,
                    Price = 30000,
                    CarId = 2, 
                    CustomerId = "customer",
                    DateOfSale = DateTime.Now,
                    EmployeeId = "employee"
                }, 
            }.AsQueryable();

            _mockSet = new Mock<DbSet<Sale>>();
            _mockSet.As<IDbAsyncEnumerable<Sale>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<Sale>(sales.GetEnumerator()));

            _mockSet.As<IQueryable<Sale>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<Sale>(sales.Provider));

            _mockSet.As<IQueryable<Sale>>().Setup(m => m.Expression).Returns(sales.Expression);
            _mockSet.As<IQueryable<Sale>>().Setup(m => m.ElementType).Returns(sales.ElementType);
            _mockSet.As<IQueryable<Sale>>().Setup(m => m.GetEnumerator()).Returns(sales.GetEnumerator());

            _mockSet.Setup(m => m.FindAsync(It.IsAny<int>())).Returns(_mockSet.Object.FindAsync(It.IsAny<int>()));

            _mockContext = new Mock<DriveITContext>();
            _mockContext.Setup(c => c.Sales).Returns(_mockSet.Object);
        }

        [Test]
        public async void CreateSaleTest()
        {
            await _toTest.CreateSale(new Sale
            {
                Id = 3,
                Price = 40000,
                CarId = 3,
                CustomerId = "customer",
                DateOfSale = DateTime.Now,
                EmployeeId = "employee"
            },
            _mockContext.Object);

            _mockSet.Verify(m => m.Add(It.IsAny<Sale>()), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync());
        }

        [Test]
        public async void GetAllCarsTest()
        {
            var result = await _toTest.GetAllSales(_mockContext.Object);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("customer", result[0].CustomerId);
            Assert.AreEqual("employee", result[1].EmployeeId);
        }

        [Test]
        public async void DeleteCarTest()
        {
            await _toTest.DeleteSale(1, _mockContext.Object);

            _mockSet.Verify(m => m.Remove(It.IsAny<Sale>()), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce);
        }

        //[Test]
        //public async void UpdateSaleTest()
        //{
        //    await _toTest.UpdateSale(2, new Sale { Price = 200000 }, _mockContext.Object);

        //    var result = _mockSet.Object.FirstOrDefault(x => x.Price == 200000);
        //    Assert.AreEqual(200000, result.Price);
        //    Assert.IsNull(result.Employee);
        //    _mockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce);
        //}

        public static void Main(string[] args)
        {
            
        }
    }
}

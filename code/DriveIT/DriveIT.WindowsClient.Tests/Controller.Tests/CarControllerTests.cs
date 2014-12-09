using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;
using NUnit.Framework;

namespace DriveIT.WindowsClient.Tests.Controller.Tests
{
    [TestFixture]
    public class CarControllerTests
    {
        private CarController _carController;
        [SetUp]
        public void Setup()
        {
            _carController = new CarController();
        }

        [Test]
        public async Task TestAllMethods()
        {
            var t = _carController.ReadCarList().Result;
            Console.WriteLine(t.Count);
                await _carController.CreateCar(new CarDto()
                {
                    Color = "Red",
                    Created = DateTime.Now,
                    DistanceDriven = 10000,
                    Model = "A8",
                    Make = "Audi",
                    Price = 200000
                });
            Thread.Sleep(2000);
            t = _carController.ReadCarList().Result;
            Console.WriteLine(t.Count);


            Console.WriteLine("Before update: " + _carController.ReadCar(t[t.Count - 1].Id.Value).Result.Color);
            await _carController.UpdateCar(new CarDto()
            {
                Color = "Silver",
                Created = DateTime.Now.AddDays(1),
                DistanceDriven = 12000,
                Model = "Swhifts",
                Make = "Suzuki",
                Price = 10000,
                Id = t[t.Count - 1].Id.Value
            });
            Thread.Sleep(2000);
            t = _carController.ReadCarList().Result;
            Console.WriteLine(t.Count);
            Console.WriteLine("After update: " + _carController.ReadCar(t[t.Count - 1].Id.Value).Result.Color);

            await _carController.DeleteCar(t[t.Count - 1].Id.Value);
            Thread.Sleep(2000);
            t = _carController.ReadCarList().Result;
            Console.WriteLine(t.Count);
        }
    }
}

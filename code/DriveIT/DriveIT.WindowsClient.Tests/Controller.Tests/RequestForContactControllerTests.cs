using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;
using NUnit.Framework;

namespace DriveIT.WindowsClient.Tests.Controller.Tests
{
    [TestFixture]
    public class RequestForContactControllerTests
    {
        private ContactRequestController _requestForContactController;
        [SetUp]
        public void Setup()
        {
            _requestForContactController = new ContactRequestController();
        }

        [Test]
        public void TestAllMethods()
        {
            var t = _requestForContactController.ReadContactRequest().Result;
            Console.WriteLine(t.Count);
                _requestForContactController.CreateContactRequest(new ContactRequestDto()
                {
                    Requested = DateTime.Now,
                    CarId = 1,
                    CustomerId = 1,
                    EmployeeId = 1,
                });
            Thread.Sleep(5000);
            t = _requestForContactController.ReadContactRequest().Result;
            Console.WriteLine(t.Count);


            Console.WriteLine("Before update: " + _requestForContactController.ReadContactRequest(t[t.Count - 1].Id.Value).Result.Requested);
            _requestForContactController.UpdateContactRequest(new ContactRequestDto()
            {
                EmployeeId = 1,
                Requested = DateTime.Now.AddDays(1),
                CarId = 1,
                CustomerId = 1,
                Id = t[t.Count - 1].Id.Value
            });
            Thread.Sleep(5000);
            t = _requestForContactController.ReadContactRequest().Result;
            Console.WriteLine(t.Count);
            Console.WriteLine("After update: " + _requestForContactController.ReadContactRequest(t[t.Count - 1].Id.Value).Result.Requested);

            _requestForContactController.DeleteContactRequest(t[t.Count - 1].Id.Value);
            Thread.Sleep(5000);
            t = _requestForContactController.ReadContactRequest().Result;
            Console.WriteLine(t.Count);
        }
    }
}

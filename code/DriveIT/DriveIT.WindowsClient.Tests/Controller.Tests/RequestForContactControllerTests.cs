using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DriveIT.Models;
using DriveIT_Windows_Client.Controllers;
using NUnit.Framework;

namespace DriveIT.WindowsClient.Tests.Controller.Tests
{
    [TestFixture]
    public class RequestForContactControllerTests
    {
        private RequestForContactController _requestForContactController;
        [SetUp]
        public void Setup()
        {
            _requestForContactController = new RequestForContactController();
        }

        [Test]
        public void TestAllMethods()
        {
            var t = _requestForContactController.ReadRequestForContactList().Result;
            Console.WriteLine(t.Count);
                _requestForContactController.CreateRequestForContact(new ContactRequestDto()
                {
                    Handled = false,
                    Requested = DateTime.Now,
                    CarId = 4,
                    CustomerId = 3,
                });
            Thread.Sleep(5000);
            t = _requestForContactController.ReadRequestForContactList().Result;
            Console.WriteLine(t.Count);


            Console.WriteLine("Before update: " + _requestForContactController.ReadRequestForContact(t[t.Count - 1].Id.Value).Result.Handled);
            _requestForContactController.UpdateRequestForContact(new ContactRequestDto()
            {
                Handled = true,
                Requested = DateTime.Now,
                CarId = 4,
                CustomerId = 4,

            });
            Thread.Sleep(5000);
            t = _requestForContactController.ReadRequestForContactList().Result;
            Console.WriteLine(t.Count);
            Console.WriteLine("After update: " + _requestForContactController.ReadRequestForContact(t[t.Count - 1].Id.Value).Result.Handled);

            _requestForContactController.DeleteRequestForContact(t[0].Id.Value);
            Thread.Sleep(5000);
            t = _requestForContactController.ReadRequestForContactList().Result;
            Console.WriteLine(t.Count);
        }
    }
}

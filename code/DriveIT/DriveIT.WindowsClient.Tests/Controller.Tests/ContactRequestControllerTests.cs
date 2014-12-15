using System;
using System.Threading.Tasks;
using DriveIT.Models;
using DriveIT.WindowsClient.Controllers;
using NUnit.Framework;

namespace DriveIT.WindowsClient.Tests.Controller.Tests
{
    [TestFixture]
    public class ContactRequestControllerTests
    {
        private ContactRequestController _requestForContactController;
        [SetUp]
        public async Task Setup()
        {
            _requestForContactController = new ContactRequestController();
            await DriveITWebAPI.Login("mlin@itu.dk", "N0t_Really_a_password");
        }

        [Test]
        public async Task TestAllMethods()
        {
            var t = _requestForContactController.ReadContactRequests().Result;
            Console.WriteLine(t.Count);
                await _requestForContactController.CreateContactRequest(new ContactRequestDto
                {
                    Requested = DateTime.Now,
                    CarId = 1,
                    CustomerId = "cust@driveit.dk",
                    EmployeeId = "mlin@itu.dk",
                });
            t = _requestForContactController.ReadContactRequests().Result;
            Console.WriteLine(t.Count);


            Console.WriteLine("Before update: " + _requestForContactController.ReadContactRequest(t[t.Count - 1].Id.Value).Result.Requested);
            await _requestForContactController.UpdateContactRequest(new ContactRequestDto()
            {
                Requested = DateTime.Now.AddDays(1),
                CarId = 1,
                CustomerId = "cust@driveit.dk",
                EmployeeId = "mlin@itu.dk",
                Id = t[t.Count - 1].Id.Value
            });
            t = _requestForContactController.ReadContactRequests().Result;
            Console.WriteLine(t.Count);
            Console.WriteLine("After update: " + _requestForContactController.ReadContactRequest(t[t.Count - 1].Id.Value).Result.Requested);

            await _requestForContactController.DeleteContactRequest(t[t.Count - 1].Id.Value);
            t = _requestForContactController.ReadContactRequests().Result;
            Console.WriteLine(t.Count);
        }
    }
}

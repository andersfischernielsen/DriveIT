using System.Collections.Generic;
using System.Threading.Tasks;
using DriveIT.Models;

namespace DriveIT.WindowsClient.Controllers
{
    public class RequestForContactController
    {
        public RequestForContactController()
        {
        }
        public async void CreateRequestForContact(ContactRequestDto contactRequest)
        {
            await DriveITWebAPI.Create("contactRequests", contactRequest);
        }
        public async Task<ContactRequestDto> ReadRequestForContact(int id)
        {
            var contactRequestToReturn = await DriveITWebAPI.Read<ContactRequestDto>("contactRequests/" + id);
            return contactRequestToReturn;
        }
        public async Task<IList<ContactRequestDto>> ReadRequestForContactList()
        {
            var contactRequests = await DriveITWebAPI.ReadList<ContactRequestDto>("contactRequests");
            return contactRequests;
        }
        public async void UpdateRequestForContact(ContactRequestDto contactRequest)
        {
            await DriveITWebAPI.Update("contactRequests", contactRequest, contactRequest.Id.Value);
        }
        public async void DeleteRequestForContact(ContactRequestDto contactRequest)
        {
            await DriveITWebAPI.Delete<ContactRequestDto>("contactRequests", contactRequest.Id.Value);
        }
        public async void DeleteRequestForContact(int id)
        {
            await DriveITWebAPI.Delete<ContactRequestDto>("contactRequests", id);
        }
    }
}

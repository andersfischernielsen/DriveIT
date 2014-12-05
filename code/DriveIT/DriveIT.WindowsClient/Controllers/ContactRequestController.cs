using System.Collections.Generic;
using System.Threading.Tasks;
using DriveIT.Models;

namespace DriveIT.WindowsClient.Controllers
{
    public class ContactRequestController
    {
        public ContactRequestController()
        {
        }
        public async Task CreateContactRequest(ContactRequestDto contactRequest)
        {
            await DriveITWebAPI.Create("contactRequests", contactRequest);
        }
        public async Task<ContactRequestDto> ReadContactRequest(int id)
        {
            var contactRequestToReturn = await DriveITWebAPI.Read<ContactRequestDto>("contactRequests/" + id);
            return contactRequestToReturn;
        }
        public async Task<IList<ContactRequestDto>> ReadContactRequests()
        {
            var contactRequests = await DriveITWebAPI.ReadList<ContactRequestDto>("contactRequests");
            return contactRequests;
        }
        public async Task UpdateContactRequest(ContactRequestDto contactRequest)
        {
            await DriveITWebAPI.Update("contactRequests", contactRequest, contactRequest.Id.Value);
        }
        public async Task DeleteContactRequest(ContactRequestDto contactRequest)
        {
            await DriveITWebAPI.Delete<ContactRequestDto>("contactRequests", contactRequest.Id.Value);
        }
        public async Task DeleteContactRequest(int id)
        {
            await DriveITWebAPI.Delete<ContactRequestDto>("contactRequests", id);
        }
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using DriveIT.Models;

namespace DriveIT.WindowsClient.Controllers
{
    /// <summary>
    /// A controller which creates the strings to CRUD ContactRequests in the DriveITWebAPI class
    /// </summary>
    public class ContactRequestController
    {
        /// <summary>
        /// An empty constructor for the ContactRequestController.
        /// </summary>
        public ContactRequestController()
        {
        }
        /// <summary>
        /// Creates a ContactRequest DTO object in the API.
        /// </summary>
        /// <param name="contactRequest">A cContactRequestar DTO</param>
        /// <returns>Returns the newly created ContactRequest DTO from the database</returns>
        public async Task<ContactRequestDto> CreateContactRequest(ContactRequestDto contactRequest)
        {
            return await DriveITWebAPI.Create("contactRequests", contactRequest);
        }
        /// <summary>
        /// Reads a specific ContactRequest DTO object from the API.
        /// </summary>
        /// <param name="id">The id of the desired ContactRequest DTO</param>
        /// <returns>Returns the ContactRequest with the respective id from the database</returns>
        public async Task<ContactRequestDto> ReadContactRequest(int id)
        {
            var contactRequestToReturn = await DriveITWebAPI.Read<ContactRequestDto>("contactRequests/" + id);
            return contactRequestToReturn;
        }
        /// <summary>
        /// Reads the list of ContactRequest DTO objects from the API.
        /// </summary>
        /// <returns>Returns the list of ContactRequest DTO's from the database</returns>
        public async Task<IList<ContactRequestDto>> ReadContactRequests()
        {
            var contactRequests = await DriveITWebAPI.ReadList<ContactRequestDto>("contactRequests");
            return contactRequests;
        }
        /// <summary>
        /// Updates the ContactRequest DTO sent to the API.
        /// </summary>
        /// <param name="contactRequest">The ContactRequest DTO to be updated</param>
        /// <returns>Returns the Task indicating whether it is completed or not</returns>
        public async Task UpdateContactRequest(ContactRequestDto contactRequest)
        {
            await DriveITWebAPI.Update("contactRequests/" + contactRequest.Id, contactRequest);
        }
        /// <summary>
        /// Deletes the selected ContactRequest DTO from the API.
        /// </summary>
        /// <param name="contactRequest">The CContactRequestar DTO to be deleted</param>
        /// <returns>Returns the Task indicating whether it is completed or not</returns>
        public async Task DeleteContactRequest(ContactRequestDto contactRequest)
        {
            await DriveITWebAPI.Delete<ContactRequestDto>("contactRequests/" + contactRequest.Id);
        }
        /// <summary>
        /// Deletes the selected ContactRequest DTO from the API with the given id.
        /// </summary>
        /// <param name="id">The id of the ContactRequest DTO to be deleted</param>
        /// <returns>Returns the Task indicating whether it is completed or not</returns>
        public async Task DeleteContactRequest(int id)
        {
            await DriveITWebAPI.Delete<ContactRequestDto>("contactRequests/" + id);
        }
    }
}

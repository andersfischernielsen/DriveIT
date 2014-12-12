using System.Collections.Generic;
using System.Threading.Tasks;
using DriveIT.Models;

namespace DriveIT.WindowsClient.Controllers
{
    /// <summary>
    /// A controller which creates the strings to CRUD sales in the DriveITWebAPI class
    /// </summary>
    public class SaleController
    {
        /// <summary>
        /// An empty constructor for the SaleController.
        /// </summary>
        public SaleController()
        {
        }
        /// <summary>
        /// Creates a Sale DTO object in the API.
        /// </summary>
        /// <param name="sale">A Sale DTO</param>
        /// <returns>Returns the newly created Sale DTO from the database</returns>
        public async Task<SaleDto> CreateSale(SaleDto sale)
        {
            return await DriveITWebAPI.Create("sales", sale);
        }
        /// <summary>
        /// Reads a specific Sale DTO object from the API.
        /// </summary>
        /// <param name="id">The id of the desired Sale DTO</param>
        /// <returns>Returns the Sale with the respective id from the database</returns>
        public async Task<SaleDto> ReadSale(int id)
        {
            var saleToReturn = await DriveITWebAPI.Read<SaleDto>("sales/" + id);
            return saleToReturn;
        }
        /// <summary>
        /// Reads the list of Sale DTO objects from the API.
        /// </summary>
        /// <returns>Returns the list of Sale DTO's from the database</returns>
        public async Task<IList<SaleDto>> ReadSaleList()
        {
            var sales = await DriveITWebAPI.ReadList<SaleDto>("sales");
            return sales;
        }
        /// <summary>
        /// Updates the Sale DTO sent to the API.
        /// </summary>
        /// <param name="sale">The Sale DTO to be updated</param>
        /// <returns>Returns the Task indicating whether it is completed or not</returns>
        public async Task UpdateSale(SaleDto sale)
        {
            await DriveITWebAPI.Update("sales/" + sale.Id, sale);
        }
        /// <summary>
        /// Deletes the selected Sale DTO from the API.
        /// </summary>
        /// <param name="sale">The Sale DTO to be deleted</param>
        /// <returns>Returns the Task indicating whether it is completed or not</returns>
        public async Task DeleteSale(SaleDto sale)
        {
            await DriveITWebAPI.Delete<SaleDto>("sales/" + sale.Id);
        }
        /// <summary>
        /// Deletes the selected Sale DTO from the API with the given id.
        /// </summary>
        /// <param name="id">The id of the Sale DTO to be deleted</param>
        /// <returns>Returns the Task indicating whether it is completed or not</returns>
        public async Task DeleteSale(int id)
        {
            await DriveITWebAPI.Delete<SaleDto>("sales/" + id);
        }
    }
}

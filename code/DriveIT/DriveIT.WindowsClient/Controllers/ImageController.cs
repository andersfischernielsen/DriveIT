using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace DriveIT.WindowsClient.Controllers
{
    /// <summary>
    /// A controller for uploading images at a given uri to the server at driveit.blob.core.windows.net
    /// </summary>
    public class ImageController
    {
        /// <summary>
        /// Uploads the image to Azure, and then proceeds to return the imagepath.
        /// </summary>
        /// <param name="id">The id of the car to be associated with the image</param>
        /// <param name="filepath">The filepath of the image to be uploaded</param>
        /// <returns>Returns the imagepath for the image</returns>
        public static async Task<String> UploadImage(int id, string filepath)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("car" + id);

            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();

            container.SetPermissions(
                new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });

            var guid = Guid.NewGuid().ToString();
            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(guid + Path.GetFileName(filepath));

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = File.OpenRead(filepath))
            {
                await blockBlob.UploadFromStreamAsync(fileStream);
            }

            return string.Format("https://driveit.blob.core.windows.net/car{0}/{1}{2}", id, guid, Path.GetFileName(filepath));
        }
    }
}

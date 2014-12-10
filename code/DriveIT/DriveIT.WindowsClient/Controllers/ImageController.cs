using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DriveIT.Models;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace DriveIT.WindowsClient.Controllers
{
    class ImageController
    {
        public static async Task<List<String>> UploadImages(CarDto dto)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("car" + dto.Id);

            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();

            container.SetPermissions(
                new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });

            var blobPaths = new List<string>();

            foreach (var imagePath in dto.ImagePaths)
            {
                // Retrieve reference to a blob named "myblob".
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(Path.GetFileName(imagePath));

                // Create or overwrite the "myblob" blob with contents from a local file.
                using (var fileStream = File.OpenRead(imagePath))
                {
                    await blockBlob.UploadFromStreamAsync(fileStream);
                }
                blobPaths.Add(string.Format("https://driveit.blob.core.windows.net/{0}/{1}", dto.Id, Path.GetFileName(imagePath)));
            }
            return blobPaths;
        }
    }
}

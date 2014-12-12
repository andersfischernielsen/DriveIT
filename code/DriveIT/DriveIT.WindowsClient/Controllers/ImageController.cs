using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace DriveIT.WindowsClient.Controllers
{
    public class ImageController
    {
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


            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(Path.GetFileName(filepath));

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = File.OpenRead(filepath))
            {
                await blockBlob.UploadFromStreamAsync(fileStream);
            }

            return string.Format("https://driveit.blob.core.windows.net/car{0}/{1}", id, Path.GetFileName(filepath));
        }
    }
}

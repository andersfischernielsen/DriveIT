using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DriveIT.WindowsClient.ViewModels;

namespace DriveIT.WindowsClient.Controllers
{
    class ImageController
    {
        public async Task<string> UploadImage(string image)
        {
            var data = File.ReadAllBytes(image);

            MediaTypeHeaderValue type;

            var extension = Path.GetExtension(image).ToLower();
            switch (extension)
            {
                case ".png":
                    type = new MediaTypeHeaderValue("image/png");
                    break;
                case ".bmp":
                    type = new MediaTypeHeaderValue("image/bmp");
                    break;
                case ".jpg":
                    type = new MediaTypeHeaderValue("image/jpeg");
                    break;
                case ".gif":
                    type = new MediaTypeHeaderValue("image/gif");
                    break;
                default:
                    throw new Exception();
            }
            
            var s = await DriveITWebAPI.UploadImage(data, type);

            return s;
        }
    }
}

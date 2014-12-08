using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriveIT.WindowsClient.ViewModels;

namespace DriveIT.WindowsClient.Controllers
{
    class ImageController
    {
        public async Task<string> UploadImage(ImageViewModel image)
        {
            var data = File.ReadAllBytes(image.ImagePath);
            
            var s = await DriveITWebAPI.UploadImage(data);

            return s;
        }
    }
}

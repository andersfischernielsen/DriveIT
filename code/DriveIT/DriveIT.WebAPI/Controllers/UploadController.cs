using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using DriveIT.Entities;

namespace DriveIT.WebAPI.Controllers
{
    public class UploadController : ApiController
    {

        [AuthorizeRoles(Role.Employee, Role.Administrator)]
        public async Task<IHttpActionResult> PostFormData()
        {
            //Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }


            var root = HostingEnvironment.MapPath("~/Upload");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                var files = new List<string>();

                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach (var multipartFileData in provider.FileData)
                {
                    string extension;
                    switch (multipartFileData.Headers.ContentType.MediaType)
                    {
                        case "image/jpeg":
                            extension = "jpg";
                            break;
                        case "image/gif":
                            extension = "gif";
                            break;
                        case "image/png":
                            extension = "png";
                            break;
                        default:
                            throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                    }
                    files.Add(string.Format("http://{0}/Upload/{1}.{2}", Request.RequestUri.Authority, Path.GetFileName(multipartFileData.LocalFileName), extension));
                    File.Move(multipartFileData.LocalFileName,
                        multipartFileData.LocalFileName + extension);
                }

                // Send OK Response along with saved file names to the client.
                return Ok(files);
            }
            catch (System.Exception e)
            {
                return InternalServerError(e);
            }
        }

    }
}
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ERP.FileManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        readonly string guacHomePath = (Environment.OSVersion.Platform == PlatformID.Unix ||
                   Environment.OSVersion.Platform == PlatformID.MacOSX)
                            ? Path.Combine(Environment.GetEnvironmentVariable("HOME"), "guac")
                            : Path.Combine(Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%"), "guac");

        // GET api/file/upload
        /// <summary>
        /// Receive form data containing a file, save file locally with a unique id as the name, and return the unique id
        /// </summary>
        /// <param name="file">Received IFormFile file</param>
        /// <returns></returns>
        [HttpPost("upload")]
        [EnableCors("MyPolicy")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            // create the new file name consisting of the current time plus a GUID
            string newFileName = DateTime.Now.Ticks + "_" + Guid.NewGuid().ToString();

            // Verify the home-guac directory exists, and combine the home-guac directory with the new file name
            Directory.CreateDirectory(guacHomePath);
            var filePath = Path.Combine(guacHomePath, newFileName);


            // Create a new file in the home-guac directory with the newly generated file name
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                //copy the contents of the received file to the newly created local file 
                await file.CopyToAsync(stream);
            }
            // return the file name for the locally stored file
            return Ok(newFileName);
        }

        // GET api/file/downlaod
        /// <summary>
        /// Return a locally stored image based on id to the requesting client
        /// </summary>
        /// <param name="id">unique identifier for the requested file</param>
        /// <returns></returns>
        [HttpGet("download/{id}")]
        public async Task<IActionResult> Download(string id)
        {
            string path = Path.Combine(guacHomePath, id);

            if (System.IO.File.Exists(path))
            {

                // Get all bytes of the file and return the file with the specified file contents 
                byte[] b = await System.IO.File.ReadAllBytesAsync(path);
                return File(b, "application/octet-stream");
            }

            else
            {
                // return error if file not found
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}

using Domain;
using Domain.Models.FileModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace Advertiser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ServiceManager _serviceManager;
        private readonly IWebHostEnvironment _environment;

        public FileController(ServiceManager serviceManager, IWebHostEnvironment environment)
        {
            _serviceManager = serviceManager;
            _environment = environment;
        }

        [HttpPost("uploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file != null)
            {
                try
                {
                    var result = await _serviceManager.Files
                        .CloudUploadFileAsync(file).ConfigureAwait(false);
                    //var uploadPath = _environment.WebRootPath;
                    //var result = await _serviceManager.Files.UploadFile(file, uploadPath).ConfigureAwait(false);

                    return CreatedAtAction(nameof(UploadFile),
                    new FileModel
                    {
                        LinkProps = new FileModel.Links { Download = result },
                        Name = Path.GetFileName(result),
                        Size = file.Length,
                        Status = FileModel.Response.Success.ToString().ToLower(),
                        Uid = Path.GetFileNameWithoutExtension(result)
                    });
                }
                catch
                {
                    return BadRequest("Что-то пошло не так");
                    throw;
                }

            }

            return StatusCode(StatusCodes.Status503ServiceUnavailable);
        }
        [HttpDelete("delete/{fileName}")]
        public async Task<IActionResult> DeleteFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable);
            }

            var result = await _serviceManager.Files
                .CloudDeleteFileAsync(fileName).ConfigureAwait(false);

            return result ? Ok(result) : (IActionResult)BadRequest(result);

        }
    }
}

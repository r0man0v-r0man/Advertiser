using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> UploadFile(IFormFileCollection fileList)
        {
            var uploadPath = _environment.WebRootPath + "\\Upload\\";

            var result = await _serviceManager.Files.UploadFile(fileList[0], uploadPath).ConfigureAwait(false);

            return CreatedAtAction(nameof(UploadFile), result);
        }
    }
}

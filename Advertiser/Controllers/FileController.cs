using System.Collections.Generic;
using System.Net.Mime;
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
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            var uploadPath = _environment.WebRootPath + "\\Upload\\";

            var result = await _serviceManager.Files.UploadFile(file, uploadPath).ConfigureAwait(false);

            return CreatedAtAction(nameof(UploadFile),
                new
                {
                    uid = "asdf", name = file.FileName, status = "done", response = new {status = "success"},
                    linkPros = new {download = "image"}
                });
        }
        //todo create file(ng zorro) class
    }
}

using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using Domain;
using Domain.Models.FileModels;
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
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var uploadPath = _environment.WebRootPath + "\\Upload\\";

            var result = await _serviceManager.Files.UploadFile(file, uploadPath).ConfigureAwait(false);
            
            return CreatedAtAction(nameof(UploadFile), 
                new FileModel
                {
                    LinkProps = new FileModel.Links { Download = result },
                    Name = file.FileName,
                    Size = file.Length,
                    Status = FileModel.Response.Success.ToString().ToLower(),
                    Uid = Guid.NewGuid()
                });        
        }
    }
}

﻿using System;
using System.Diagnostics;
using System.IO;
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
        public async Task<IActionResult> UploadFile(IFormFile uploadFile)
        {
            var uploadPath = _environment.WebRootPath + "\\Upload\\";

            var result = await _serviceManager.Files.UploadFile(uploadFile, uploadPath).ConfigureAwait(false);

            switch (result)
            {
                case true:
                    return CreatedAtAction(nameof(UploadFile), result);
                case false:
                    return BadRequest(result);
            }
        }
    }
}
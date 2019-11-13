using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Domain.Services
{
    public class FileService : IFileService
    {
        public async Task<bool> UploadFile(IFormFile uploadFile, string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return true;
        }
    }
}

using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Domain.Services
{
    public class FileService : IFileService
    {
        public async Task<string> UploadFile(IFormFile uploadFile, string webRootPath)
        {
            if (uploadFile != null)
            {
                var folderPath = "Upload";
                var subFolderYear = DateTime.Now.Year.ToString();

                var realPath = webRootPath + "\\" + folderPath + "\\" + subFolderYear + "\\";

                if (!Directory.Exists(realPath))
                    Directory.CreateDirectory(realPath);

                var uniqueName = Guid.NewGuid().ToString();
                var fileExtension = Path.GetExtension(uploadFile.FileName);
                var newFileName = uniqueName + fileExtension;

                var fileName = Path.Combine(webRootPath, folderPath, subFolderYear) + $@"\{newFileName}";

                var pathDb = folderPath + "/" + subFolderYear + "/" + newFileName;

                await using FileStream fs = File.Create(fileName);
                await uploadFile.CopyToAsync(fs).ConfigureAwait(false);

                return pathDb;
            }
            
            return String.Empty;
        }

    }
}

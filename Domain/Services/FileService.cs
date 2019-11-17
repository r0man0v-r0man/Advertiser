using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Domain.Services
{
    public class FileService : IFileService
    {
        const string failedUpload = "Something wrong! Try again later";
        public async Task<string> UploadFile(IFormFile uploadFile, string path)
        {
            try
            {
                if (uploadFile?.Length > 0)
                {
                    if (string.IsNullOrEmpty(path)) throw new NullReferenceException();
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    //new name for upload file
                    var extension = Path.GetExtension(uploadFile.FileName);
                    var newFileName = Guid.NewGuid();
                    var file = Path.Combine(path, string.Concat(newFileName, extension));
                    //copy file to folder
                    await using FileStream fs = File.Create(Path.Combine(path, file));
                    await uploadFile.CopyToAsync(fs).ConfigureAwait(false);

                    return file;

                }

                throw new Exception(failedUpload);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

        }
    }
}

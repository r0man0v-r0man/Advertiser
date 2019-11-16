using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Domain.Services
{
    public class FileService : IFileService
    {
        const string failedUpload = "Не получилось! А, давай еще раз";
        public async Task<IFormFile> UploadFile(IFormFile uploadFile, string path)
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

                    await using FileStream fs = File.Create(Path.Combine(path, string.Concat(newFileName, extension)));
                    await uploadFile.CopyToAsync(fs).ConfigureAwait(false);
                    var file = Path.Combine(path, string.Concat(newFileName, extension));
                    return uploadFile;

                }

                return uploadFile;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

        }
    }
}

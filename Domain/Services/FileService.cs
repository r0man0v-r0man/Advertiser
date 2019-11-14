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
        public async Task<string> UploadFile(IFormFile uploadFile, string path)
        {
            try
            {
                if (uploadFile?.Length > 0)
                {
                    if (string.IsNullOrEmpty(path)) throw new NullReferenceException();
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                    await using FileStream fs = File.Create(path + uploadFile?.FileName);
                    await uploadFile.CopyToAsync(fs).ConfigureAwait(false);
                    var result = path + uploadFile.FileName;
                    return result;

                }

                return failedUpload;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }

        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IFileService
    {
        Task<string> UploadFile(IFormFile uploadFile, string path);
        Task<string> CloudUploadFileAsync(IFormFile uploadFile);
        Task<bool> CloudDeleteFileAsync(string fileName);
    }
}

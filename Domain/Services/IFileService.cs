using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IFileService
    {
        Task<string> UploadFile(IFormFile uploadFile, string path);
    }
}

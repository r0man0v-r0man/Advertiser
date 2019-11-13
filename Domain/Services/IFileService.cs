using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IFileService
    {
        Task<bool> UploadFile(IFormFile uploadFile, string path);
    }
}

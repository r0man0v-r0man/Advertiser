using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IFileService
    {
        Task<IFormFile> UploadFile(IFormFile uploadFile, string path);
    }
}

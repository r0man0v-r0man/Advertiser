using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Domain.Services
{
    public class FileService : IFileService
    {
        private readonly IConfiguration _config;
        private string AzureConnectionString { get; }
        public FileService(IConfiguration configuration)
        {
            _config = configuration;
            AzureConnectionString = _config["AzureStorageConnectionString"];
        }
        public async Task<string> CloudUploadFile(IFormFile uploadFile)
        {
            var container = GetBlobContainer(AzureConnectionString, "files");

            var content = ContentDispositionHeaderValue.Parse(uploadFile.ContentDisposition);

            var fileName = content.FileName.Trim('"');

            var blockBlob = container.GetBlockBlobReference(fileName);

            await blockBlob.UploadFromStreamAsync(uploadFile.OpenReadStream()).ConfigureAwait(false);

            return blockBlob.Uri.ToString();

        }
        /// <summary>
        /// Загрузка файла на сервер
        /// </summary>
        /// <param name="uploadFile">Файл</param>
        /// <param name="webRootPath">Корневой каталог окружения</param>
        /// <returns></returns>
        public async Task<string> UploadFile(IFormFile uploadFile, string webRootPath)
        {
            if (uploadFile != null)
            {

                var folderPath = "Upload";
                var subFolderYear = DateTime.Now.Year.ToString();

                var realPath = webRootPath + "\\" + folderPath + "\\" + subFolderYear + "\\";

                CheckExistingFolder(realPath);

                var newFileName = SetUniqueName(uploadFile);

                var fileName = SetFileName(webRootPath, folderPath, subFolderYear, newFileName);

                await CopyFileAsync(fileName, uploadFile).ConfigureAwait(false);

                var pathDb = folderPath + "/" + subFolderYear + "/" + newFileName;
                
                return pathDb;
            }
            
            return string.Empty;
        }
        /// <summary>
        /// Копирование файла в файловую систему
        /// </summary>
        /// <param name="fileName">Полное имя файла(путь к файлу)</param>
        /// <param name="file">Загружаемый файл</param>
        /// <returns></returns>
        private async Task CopyFileAsync(string fileName, IFormFile file)
        {
            await using FileStream fs = File.Create(fileName);
            await file.CopyToAsync(fs).ConfigureAwait(false);
        }
        /// <summary>
        /// Проверка существования папки
        /// </summary>
        /// <param name="folderPath">Название папки</param>
        private void CheckExistingFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
        }
        /// <summary>
        /// Установка имени файла
        /// </summary>
        /// <param name="webRootPath"></param>
        /// <param name="folderPath"></param>
        /// <param name="subFolder"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string SetFileName(string webRootPath, string folderPath, string subFolder, string fileName)
        {
            return Path.Combine(webRootPath, folderPath, subFolder) + $@"\{fileName}";
        }
        /// <summary>
        /// Установка уникального имени для файла
        /// </summary>
        /// <param name="file">Файл для переименования</param>
        /// <returns></returns>
        private string SetUniqueName(IFormFile file)
        {
            var uniqueName = Guid.NewGuid().ToString();
            var fileExtension = Path.GetExtension(file.FileName);
            return string.Concat(uniqueName, fileExtension);
        }

        public CloudBlobContainer GetBlobContainer(string azureConnectionString, string containerName)
        {
            var storageAccount = CloudStorageAccount.Parse(azureConnectionString);

            var blobClient = storageAccount.CreateCloudBlobClient();

            return blobClient.GetContainerReference(containerName);

        }
    }
}

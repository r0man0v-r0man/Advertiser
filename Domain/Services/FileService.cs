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
        private string ContainerName { get; }
        public FileService(IConfiguration configuration)
        {
            _config = configuration;
            AzureConnectionString = _config["AzureStorageConnectionString"];
            ContainerName = "files";
        }
        public async Task<string> CloudUploadFileAsync(IFormFile uploadFile)
        {
            var container = GetBlobContainer(AzureConnectionString, ContainerName);
            if (await container.ExistsAsync().ConfigureAwait(false))
            {
                var blockBlob = container.GetBlockBlobReference(SetUniqueName(uploadFile));

                await blockBlob.UploadFromStreamAsync(uploadFile.OpenReadStream()).ConfigureAwait(false);

                return blockBlob.Uri.ToString();
            }
            else
            {
                return string.Empty;
            }



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
        /// <summary>
        /// Получаем облачный контейнер
        /// </summary>
        /// <param name="azureConnectionString">Строка подключения</param>
        /// <param name="containerName">Имя контейнера</param>
        /// <returns></returns>
        private CloudBlobContainer GetBlobContainer(string azureConnectionString, string containerName)
        {
            var storageAccount = CloudStorageAccount.Parse(azureConnectionString);

            var blobClient = storageAccount.CreateCloudBlobClient();

            return blobClient.GetContainerReference(containerName);
        }

        public async Task<bool> CloudDeleteFileAsync(string fileName)
        {
            try
            {
                var container = GetBlobContainer(AzureConnectionString, ContainerName);
                if (await container.ExistsAsync().ConfigureAwait(false))
                {
                    var file = container.GetBlobReference(fileName);
                    if (await file.ExistsAsync().ConfigureAwait(false))
                    {
                        await file.DeleteAsync().ConfigureAwait(false);
                    }
                }
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}

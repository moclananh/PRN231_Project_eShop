using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Common
{
    public class FileStorageService : IStorageService
    {
        private readonly string _userContentFolder;
        private const string USER_CONTENT_FOLDER_NAME = "Images";
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileStorageService(IWebHostEnvironment webHostEnvironment)
        {
            _userContentFolder = Path.Combine(webHostEnvironment.ContentRootPath, USER_CONTENT_FOLDER_NAME);
            _webHostEnvironment = webHostEnvironment;
        }

        public string GetFileUrl(string fileName)
        {
            return $"/{USER_CONTENT_FOLDER_NAME}/{fileName}";
        }

  /*      public async Task SaveFileAsync(Stream mediaBinaryStream, string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            using var output = new FileStream(filePath, FileMode.Create);
            await mediaBinaryStream.CopyToAsync(output);
        }*/

        public async Task DeleteFileAsync(string fileName)
        {
            var filePath = Path.Combine(_userContentFolder, fileName);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        public async Task<string> SaveImageAsync(IFormFile image)
        {
            var hostUrl = "https://localhost:5001";
            string folder = "Images";
            string filename = image.FileName;
            string filePath = Path.Combine(folder, Guid.NewGuid().ToString() + filename);
            string serverFolder = Path.Combine(_webHostEnvironment.ContentRootPath, filePath);

            using (var stream = new FileStream(serverFolder, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            string imagePath = hostUrl + "/" + filePath;
            return imagePath;
        }
    }
}


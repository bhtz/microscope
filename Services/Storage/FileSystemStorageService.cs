using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IronHasura.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace IronHasura.Services
{
    public class FileSystemStorageService : IStorageService
    {
        private IConfiguration Configuration { get; set; }
        public string UploadsFolder { get; set; }
        
        public FileSystemStorageService(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.UploadsFolder = configuration.GetValue<string>("IRONHASURA_STORAGE_CONTAINER");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public Task<bool> DeleteFile(string filename)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), this.UploadsFolder, filename);
            if (File.Exists(path))
            {
                File.Delete(path);
                return Task.FromResult(true);
            }
            else
            {
                throw new Exception("File not found");
                // return Task.FromResult(false);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public Task<byte[]> GetFileData(string filename)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), this.UploadsFolder, filename);
            if (File.Exists(path))
            {
                var fileBytes = File.ReadAllBytes(path);
                return Task.FromResult(fileBytes);
            }
            else
            {
                throw new Exception("File not found");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public Task<string> GetFileUri(string filename)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), this.UploadsFolder, filename);

            if (File.Exists(path))
            {
                return Task.FromResult(this.UploadsFolder + "/" + filename);
            }
            else
            {
                throw new Exception("File not found");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> SaveFile(string filename, IFormFile file)
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), this.UploadsFolder);

            bool uploadDirExist = Directory.Exists(uploadPath);
            if (!uploadDirExist)
            {
                Directory.CreateDirectory(uploadPath);
            }

            var path = Path.Combine(Directory.GetCurrentDirectory(), this.UploadsFolder, filename);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filename;
        }

        public Task<string[]> GetFiles()
        {
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), this.UploadsFolder);
            
            var files = Directory
                .GetFiles(uploadPath)
                .Select(x => Path.GetFileName(x))
                .ToArray();

            return Task.FromResult(files);
        }
    }
}
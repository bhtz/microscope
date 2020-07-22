using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using IronHasura.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Linq;

namespace IronHasura.Services
{
    public class BlobStorageService : IStorageService
    {
        private string ContainerName { get; set; }
        private IConfiguration Configuration { get; set; }
        private CloudBlobClient Client { get; set; }
        private CloudStorageAccount StorageAccount { get; set; }
        private CloudBlobContainer Container { get; set; }

        public BlobStorageService(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.InitStorageClient();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<string> SaveFile(string filename, IFormFile file)
        {
            await this.Container.CreateIfNotExistsAsync();
            CloudBlockBlob blob = this.Container.GetBlockBlobReference(filename);

            using (Stream stream = file.OpenReadStream())
            {
                await blob.UploadFromStreamAsync(stream);
            }

            return blob.Uri.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<bool> DeleteFile(string filename)
        {
            await this.Container.CreateIfNotExistsAsync();
            CloudBlockBlob blob = this.Container.GetBlockBlobReference(filename);

            return await blob.DeleteIfExistsAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<string> GetFileUri(string filename)
        {
            await this.Container.CreateIfNotExistsAsync();
            CloudBlockBlob blob = this.Container.GetBlockBlobReference(filename);

            return blob.Uri.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<byte[]> GetFileData(string filename)
        {
            await this.Container.CreateIfNotExistsAsync();
            CloudBlockBlob blob = this.Container.GetBlockBlobReference(filename);
            await blob.FetchAttributesAsync();
            byte[] data = new byte[blob.Properties.Length];
            await blob.DownloadToByteArrayAsync(data, 0);

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        private void InitStorageClient()
        {
            var azureWebJobsStorage = this.Configuration.GetValue<string>("MCSP_STORAGE_CS");
            var containerName = this.Configuration.GetValue<string>("MCSP_STORAGE_CONTAINER");

            this.StorageAccount = CloudStorageAccount.Parse(azureWebJobsStorage);
            this.Client = this.StorageAccount.CreateCloudBlobClient();
            this.Container = this.Client.GetContainerReference(containerName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<string[]> GetFiles()
        {
            var data = await this.Container.ListBlobsSegmentedAsync(null);
            return data.Results.Select(x => x.Uri.ToString().Split('/').Last()).ToArray();
        }
    }
}
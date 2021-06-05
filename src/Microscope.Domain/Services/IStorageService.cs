using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Microscope.Domain.Services
{
    public interface IStorageService
    {
        Task<IEnumerable<string>> ListBlobsAsync(string containerName);
        Task<Stream> GetBlobAsync(string containerName, string blobName);
        Task SaveBlobAsync(string containerName, string blobName, Stream data);
        Task DeleteBlobAsync(string containerName, string blobName);
        Task<IEnumerable<string>> ListContainersAsync();
        Task CreateContainerAsync(string containerName);
        Task DeleteContainerAsync(string containerName);
    }
}

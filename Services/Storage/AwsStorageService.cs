using System.Threading.Tasks;
using Microscope.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Microscope.Services
{
    public class AwsStorageService : IStorageService
    {
        public Task<bool> DeleteFile(string filename)
        {
            throw new System.NotImplementedException();
        }

        public Task<byte[]> GetFileData(string filename)
        {
            throw new System.NotImplementedException();
        }

        public Task<string[]> GetFiles()
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetFileUri(string filename)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> SaveFile(string filename, IFormFile file)
        {
            throw new System.NotImplementedException();
        }
    }
}
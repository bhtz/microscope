using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace IronHasura.Services.Interfaces
{
    public interface IStorageService
    {   
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<string> SaveFile(string filename, IFormFile file);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        Task<bool> DeleteFile(string filename);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        Task<string> GetFileUri(string filename);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        Task<byte[]> GetFileData(string filename);
    }
}
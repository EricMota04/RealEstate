using Microsoft.AspNetCore.Http;

namespace RealEstateApp.Application.Interfaces.Services
{
    public interface IImageService 
    {
        Task<string> UploadFileAsync(IFormFile file);
        Task DeleteFileAsync(string blobName);
        Task<string> GetFileUrlAsync(string blobName);
    }
}

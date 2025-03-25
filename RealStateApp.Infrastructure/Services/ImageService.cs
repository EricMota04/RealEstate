using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RealEstateApp.Application.Interfaces.Services;

namespace RealEstateApp.Infrastructure.Services
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration _configuration;
        private readonly string _containerName;
        private readonly BlobServiceClient _blobServiceClient;
        public ImageService(IConfiguration configuration, BlobServiceClient blobServiceClient)
        {
            _configuration = configuration;
            _blobServiceClient = blobServiceClient;
            _containerName = _configuration["AzureBlobStorage:ContainerName"];
        }

        public async Task DeleteFileAsync(string blobName)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(blobName);

            await blobClient.DeleteIfExistsAsync(); 
        }

        public Task<string> GetFileUrlAsync(string blobName)
        {
            string url = $"https://{_blobServiceClient.AccountName}.blob.core.windows.net/{_containerName}/{blobName}";
            return Task.FromResult(url);
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var blobContainerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
            await blobContainerClient.CreateIfNotExistsAsync();

            string blobName = $"{Guid.NewGuid()}_{file.FileName}";
            var blobClient = blobContainerClient.GetBlobClient(blobName);

            using(var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream);
            }

            return blobName;
        }
    }
}

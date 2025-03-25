using MediatR;
using RealEstateApp.Application.Features.Images.Commands;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Interfaces.Services;
using RealEstateApp.Application.Wrappers;
using Microsoft.Extensions.Logging;

namespace RealEstateApp.Application.Features.Images.Handlers
{
    public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, ServiceResult<bool>>
    {
        private readonly IImageRepository _imageRepository;
        private readonly IImageService _imageService;
        private readonly ILogger<DeleteImageCommandHandler> _logger;

        public DeleteImageCommandHandler(IImageRepository imageRepository, IImageService imageService, ILogger<DeleteImageCommandHandler> logger)
        {
            _imageRepository = imageRepository;
            _imageService = imageService;
            _logger = logger;
        }

        public async Task<ServiceResult<bool>> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var image = await _imageRepository.FindByIdAsync(request.ImageId);

                if (image == null)
                {
                    _logger.LogWarning($"Image with ID {request.ImageId} not found.");
                    return ServiceResult<bool>.Failure($"Image with ID {request.ImageId} not found.");
                }

                await Task.WhenAll(
                    _imageService.DeleteFileAsync(image.BlobName),
                    _imageRepository.DeleteAsync(image)
                );

                _logger.LogInformation($"Image with ID {request.ImageId} deleted successfully.");
                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting image with ID {request.ImageId}.");
                return ServiceResult<bool>.Failure("An error occurred while deleting the image.");
            }
        }
    }
}

using MediatR;
using RealEstateApp.Application.DTOs.Image;
using RealEstateApp.Application.Features.Images.Querys;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Interfaces.Services;
using RealEstateApp.Application.Wrappers;
using Microsoft.Extensions.Logging;

namespace RealEstateApp.Application.Features.Images.Handlers
{
    public class GetImagesByPropertyQueryHandler : IRequestHandler<GetImagesByPropertyQuery, ServiceResult<List<ImageDto>>>
    {
        private readonly IImageRepository _imageRepository;
        private readonly IImageService _imageService;
        private readonly ILogger<GetImagesByPropertyQueryHandler> _logger;

        public GetImagesByPropertyQueryHandler(IImageRepository imageRepository, IImageService imageService, ILogger<GetImagesByPropertyQueryHandler> logger)
        {
            _imageRepository = imageRepository;
            _imageService = imageService;
            _logger = logger;
        }

        public async Task<ServiceResult<List<ImageDto>>> Handle(GetImagesByPropertyQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var images = await _imageRepository.GetImagesByPropertyAsync(request.PropertyId);

                if (images == null || !images.Any())
                {
                    _logger.LogWarning($"No images found for property ID {request.PropertyId}.");
                    return ServiceResult<List<ImageDto>>.Failure($"No images found for property ID {request.PropertyId}.");
                }

                var imagesDto = new List<ImageDto>();
                var tasks = images.Select(async image =>
                {
                    var imageUrl = await _imageService.GetFileUrlAsync(image.BlobName);
                    return new ImageDto
                    {
                        Id = image.Id,
                        ImageUrl = imageUrl
                    };
                });

                imagesDto = (await Task.WhenAll(tasks)).ToList();

                return ServiceResult<List<ImageDto>>.Success(imagesDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving images for property ID {request.PropertyId}.");
                return ServiceResult<List<ImageDto>>.Failure("An error occurred while retrieving images.");
            }
        }
    }
}

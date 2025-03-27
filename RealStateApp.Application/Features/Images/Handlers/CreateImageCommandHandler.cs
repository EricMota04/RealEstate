using MediatR;
using RealEstateApp.Application.Features.Images.Commands;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Application.Interfaces.Services;
using RealEstateApp.Application.Wrappers;
using RealEstateApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Application.Features.Images.Handlers
{
    public class CreateImageCommandHandler : IRequestHandler<CreateImageCommand, ServiceResult<bool>>
    {
        private readonly IImageRepository _imageRepository;
        private readonly IImageService _imageService;

        public CreateImageCommandHandler(IImageRepository imageRepository, IImageService imageService)
        {
            _imageRepository = imageRepository;
            _imageService = imageService;
        }

        public async Task<ServiceResult<bool>> Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {
            var uploadTasks = request.Files.Select(async file =>
            {
                var blobName = await _imageService.UploadFileAsync(file);
                return new Image
                {
                    Id = Guid.NewGuid(),
                    PropertyId = request.PropertyId,
                    BlobName = blobName,
                };
            });

            var images = await Task.WhenAll(uploadTasks);

            if (images.Length == 1)
                await _imageRepository.AddAsync(images[0]); // Una sola imagen, usa AddAsync
            else
                await _imageRepository.AddImagesAsync(images.ToList()); // Varias imágenes, usa AddImagesAsync

            return ServiceResult<bool>.Success(true);
        }
    }
}

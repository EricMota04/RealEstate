using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RealEstateApp.Application.DTOs.Image;
using RealEstateApp.Application.Interfaces.Repositories;
using RealEstateApp.Domain.Entities;
using RealEstateApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RealEstateApp.Infrastructure.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly RealEstateDbContext _dbContext;
        private readonly ILogger<ImageRepository> _logger;

        public ImageRepository(RealEstateDbContext dbContext, ILogger<ImageRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task AddAsync(Image entity)
        {
            try
            {
                await _dbContext.Images.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Image added successfully with ID: {entity.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding image: {ex.Message}");
                throw;
            }
        }

        public async Task AddImagesAsync(IEnumerable<Image> images)
        {
            try
            {
                await _dbContext.Images.AddRangeAsync(images);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"{images.Count()} images added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding images: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(Image entity)
        {
            try
            {
                _dbContext.Images.Remove(entity);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Image deleted successfully with ID: {entity.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting image: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> ExistsAsync(Expression<Func<Image, bool>> predicate)
        {
            try
            {
                return await _dbContext.Images.AnyAsync(predicate);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error checking if image exists: {ex.Message}");
                throw;
            }
        }

        public async Task<Image?> FindByIdAsync(Guid id)
        {
            try
            {
                return await _dbContext.Images.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error finding image by ID: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Image>> GetImagesByPropertyAsync(Guid propertyId)
        {
            try
            {
                var images = await _dbContext.Images
                    .Where(i => i.PropertyId == propertyId)
                    .ToListAsync();

                return images;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching images for property {propertyId}: {ex.Message}");
                throw;
            }
        }
    }
}

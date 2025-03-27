using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Application.Features.Images.Commands;
using RealEstateApp.Application.Features.Images.Querys;

namespace RealEstate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ImageController> _logger;
        public ImageController(IMediator mediator, ILogger<ImageController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromBody] CreateImageCommand createImageCommand)
        {
            var result = await _mediator.Send(createImageCommand);

            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetImagesByProperty([FromQuery] GetImagesByPropertyQuery getImagesByPropertyQuery)
        {
            var result = await _mediator.Send(new GetImagesByPropertyQuery(getImagesByPropertyQuery.PropertyId));
            
            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> DeleteImage([FromBody] DeleteImageCommand deleteImageCommand)
        {
            var result = await _mediator.Send(deleteImageCommand);
            if (result.IsSuccess)
                return Ok(result);
            else
                return BadRequest(result);

        }
    }
}

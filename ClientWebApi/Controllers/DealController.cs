using ClientWebApi.Services.Deal;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ClientWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DealController : ControllerBase
    {       
        private readonly ILogger<DealController> _logger;
        private readonly IMediator _mediator;

        public DealController(ILogger<DealController> logger, IMediator mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        /// <summary>
        /// POST Best Deal
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Succesfull.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="500">Server error.</response>
        /// <returns></returns>
        [HttpPost()]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostBestDeal([FromBody] PostBestDealRequest request)
        {
            try
            {
                _logger.LogInformation($"Begin - {nameof(PostBestDeal)}");

                // request processing
                var response = await _mediator.Send(request);

                // no content
                if (response == null) return NoContent();

                // bad request
                if (response.ErrorResponse != null)
                {
                    _logger.LogError($"Validation Errors: {nameof(PostBestDeal)} {{@Details}}", response.ErrorResponse);
                    return BadRequest(response.ErrorResponse);
                }

                // success 200
                return StatusCode(StatusCodes.Status200OK, response);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unexpected error occurred trying to {nameof(PostBestDeal)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            finally
            {
                _logger.LogInformation($"End - {nameof(PostBestDeal)}");
            }
        }
    }
}

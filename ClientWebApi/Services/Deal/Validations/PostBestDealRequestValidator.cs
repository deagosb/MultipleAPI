using ClientWebApi.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ClientWebApi.Services.Deal.Validations
{
    public class PostBestDealRequestValidator : IRequestValidator<PostBestDealRequest>
    {
        public PostBestDealRequestValidator()
        {

        }

        public Task<List<ErrorResult>> ValidateAsync(PostBestDealRequest request,
            CancellationToken cancellationToken = default)
        {
            var errorResults = new List<ErrorResult>();

            if (string.IsNullOrEmpty(request.SourceAddress))
            {
                errorResults.Add(new ErrorResult("SourceAddress is required", StatusCodes.Status400BadRequest));
            }

            if (string.IsNullOrEmpty(request.DestinationAddress))
            {
                errorResults.Add(new ErrorResult("DestinationAddress is required", StatusCodes.Status400BadRequest));
            }

            return Task.FromResult(errorResults);
        }
    }
}

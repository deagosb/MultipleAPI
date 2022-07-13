using AutoMapper;
using ClientWebApi.Helpers;
using ClientWebApi.Models;
using MediatR;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientWebApi.Services.Deal
{

    public class PostBestDealRequest : IRequest<PostBestDealResponse> 
    {
        public string SourceAddress { get; set; }
        public string DestinationAddress { get; set; }
        public int[] CartonDimensions { get; set; }

    }
    public class PostBestDealResponse : IValidationResponse
    {
        public int Total { get; set; }
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class PostBestDealHandler : IRequestHandler<PostBestDealRequest, PostBestDealResponse>
    {
        private readonly IRequestValidator<PostBestDealRequest> _validator;
        private readonly IMapper _mapper;
        private readonly IApiService _apiService;

        private readonly string UriWebApi1 = "http://localhost:31568/value";
        private readonly string UriWebApi2 = "http://localhost:31530/value";
        private readonly string UriWebApi3 = "http://localhost:62222/value";

        public PostBestDealHandler(IRequestValidator<PostBestDealRequest> validator, IMapper mapper, IApiService apiService)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
        }

        public async Task<PostBestDealResponse> Handle(PostBestDealRequest request, CancellationToken cancellationToken)
        {
            // Validation
            var errorResults = await _validator.ValidateAsync(request, cancellationToken);

            if (errorResults.Any()) return new PostBestDealResponse
            {
                ErrorResponse = new ErrorResponse { ErrorResults = errorResults }
            };

            PostBestDealResponse response = new PostBestDealResponse();

            // Transform Data
            Input1 input1 = _mapper.Map<Input1>(request);
            Input2 input2 = _mapper.Map<Input2>(request);
            Input3 input3 = _mapper.Map<Input3>(request);
            List<Package> packages = new List<Package>();
            foreach (var item in input1.PackageDimensions)
            {
                packages.Add(new Package() { Value = item });
            }
            input3.Packages = packages;

            // Call API1            
            string bodyString1 = JsonConvert.SerializeObject(input1, Formatting.Indented);
            string result1 = await _apiService.Call(UriWebApi1, bodyString1, Encoding.UTF8, "application/json");
            Output1 output1 = JsonConvert.DeserializeObject<Output1>(result1);
            PostBestDealResponse x = _mapper.Map<PostBestDealResponse>(output1);

            // Call API2            
            string bodyString2 = JsonConvert.SerializeObject(input2, Formatting.Indented);
            string result2 = await _apiService.Call(UriWebApi2, bodyString2, Encoding.UTF8, "application/json");
            Output2 output2 = JsonConvert.DeserializeObject<Output2>(result2);
            PostBestDealResponse y = _mapper.Map<PostBestDealResponse>(output2);

            // Call API3            
            var bodyString3 = XmlHelper.Serialize(input3);
            string result3 = await _apiService.Call(UriWebApi3, bodyString3, Encoding.UTF8, "application/xml");
            Output3 output3 = XmlHelper.Deserialize<Output3>(result3);
            PostBestDealResponse z = _mapper.Map<PostBestDealResponse>(output3);

            response.Total = MathHelper.Min(x.Total, y.Total, z.Total);

            return response;
        }
     
        /// <summary>
        /// Set Error Response Message
        /// </summary>
        /// <param name="message"></param>
        /// <returns>ErrorResponse</returns>
        private ErrorResponse SetErrorResponse(string message)
        {
            return new ErrorResponse
            {
                ErrorResults = new List<ErrorResult>
                {
                    new ErrorResult(message, 400)
                }
            };
        }
    }

}

using AutoMapper;
using ClientWebApi.Mappers;
using ClientWebApi.Services;
using ClientWebApi.Services.Deal;
using ClientWebApi.Services.Deal.Validations;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ClientWebApi.Test.Services.Deal
{
    [Trait("Category", "Unit")]
    public class PostBestDealHandler_UT
    {
        private PostBestDealHandler _handler;
        private Mock<IApiService> _apiService;
        public PostBestDealHandler_UT()
        {
            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile(new MappingProfile()));
            var mapper = new Mapper(configuration);

            _apiService = new Mock<IApiService>();

            var requestValidator = new PostBestDealRequestValidator();
            _handler = new PostBestDealHandler(requestValidator, mapper, _apiService.Object);
        }

        [Fact]
        public async Task PostBestDealHandler_UT_WhenValidationFails_EmptyRequest()
        {
            // Arrange
            var request = new PostBestDealRequest() { };

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.ErrorResponse.ErrorResults.Exists(x => x.ErrorMessage == "SourceAddress is required" && x.ResultCode == StatusCodes.Status400BadRequest));
            Assert.True(result.ErrorResponse.ErrorResults.Exists(x => x.ErrorMessage == "DestinationAddress is required" && x.ResultCode == StatusCodes.Status400BadRequest));
        }
    }
}

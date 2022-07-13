using ClientWebApi.Controllers;
using ClientWebApi.Services.Deal;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ClientWebApi.Test.Controllers
{
    [Trait("Category", "Unit")]
    public class DealController_UT
    {
        Mock<IMediator> mockMediator;
        Mock<ILogger<DealController>> mockLogger;
        DealController controller;

        public DealController_UT()
        {
            mockMediator = new Mock<IMediator>();
            mockLogger = new Mock<ILogger<DealController>>();

            controller = new DealController(mockLogger.Object, mockMediator.Object);
        }

        [Fact]
        public async Task PostBestDeal_Return_Ok()
        {
            // Arrange
            PostBestDealRequest postBestDealRequest = new PostBestDealRequest() 
            {
                SourceAddress = "Source Address",
                DestinationAddress = "DestinationAddress",
                CartonDimensions = new int[] { 10, 15, 50 }
            };
            var response = new PostBestDealResponse
            {
                Total = 1
            };

            mockMediator.Setup(x => x.Send(It.IsAny<PostBestDealRequest>(), default(CancellationToken)))
                .Returns(Task.FromResult(response));

            // Act
            var result = await controller.PostBestDeal(postBestDealRequest);

            // Assert            
            var controllerResponse = result as ObjectResult;
            Assert.Equal(HttpStatusCode.OK, (HttpStatusCode)controllerResponse.StatusCode);
        }

        [Fact]
        public async Task PostBestDeal_Return_NoContent()
        {
            // Arrange
            PostBestDealRequest postBestDealRequest = new PostBestDealRequest()
            {
                SourceAddress = "Source Address",
                DestinationAddress = "DestinationAddress",
                CartonDimensions = new int[] { 10, 15, 50 }
            };            

            mockMediator.Setup(x => x.Send(It.IsAny<PostBestDealRequest>(), default(CancellationToken)))
                .Returns(Task.FromResult((PostBestDealResponse)null));

            // Act
            var result = await controller.PostBestDeal(postBestDealRequest);

            // Assert            
            var controllerResponse = result as NoContentResult;
            Assert.Equal(HttpStatusCode.NoContent, (HttpStatusCode)controllerResponse.StatusCode);
        }

        [Fact]
        public async Task PostBestDeal_Return_InternalServerError()
        {
            // Arrange
            PostBestDealRequest postBestDealRequest = new PostBestDealRequest()
            {
                SourceAddress = "Source Address",
                DestinationAddress = "DestinationAddress",
                CartonDimensions = new int[] { 10, 15, 50 }
            };

            mockMediator.Setup(x => x.Send(It.IsAny<PostBestDealRequest>(), default(CancellationToken)))
                .Throws(new Exception());

            // Act
            var result = await controller.PostBestDeal(postBestDealRequest);

            // Assert            
            var controllerResponse = (IStatusCodeActionResult)result;
            Assert.Equal(HttpStatusCode.InternalServerError, (HttpStatusCode)controllerResponse.StatusCode);
        }
    }
}

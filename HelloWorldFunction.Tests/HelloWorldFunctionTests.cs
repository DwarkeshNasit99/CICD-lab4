using System.Net;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System.Text.Json;
using HelloWorldFunction;

namespace HelloWorldFunction.Tests
{
    public class HelloWorldFunctionTests
    {
        [Fact]
        public void HelloWorld_ReturnsSuccessResponse()
        {
            // Arrange
            var function = new HelloWorldFunction();
            var mockRequest = new Mock<HttpRequestData>(MockBehavior.Strict, new Mock<FunctionContext>().Object);
            var mockResponse = new Mock<HttpResponseData>(MockBehavior.Strict, new Mock<FunctionContext>().Object);
            
            mockRequest.Setup(r => r.CreateResponse(It.IsAny<HttpStatusCode>())).Returns(mockResponse.Object);
            mockResponse.Setup(r => r.Headers).Returns(new HttpHeadersCollection());
            mockResponse.Setup(r => r.WriteString(It.IsAny<string>())).Returns(mockResponse.Object);

            // Act
            var result = function.Run(mockRequest.Object);

            // Assert
            Assert.NotNull(result);
            mockRequest.Verify(r => r.CreateResponse(HttpStatusCode.OK), Times.Once);
            mockResponse.Verify(r => r.WriteString(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void HelloWorld_ResponseContainsExpectedMessage()
        {
            // Arrange
            var function = new HelloWorldFunction();
            var mockRequest = new Mock<HttpRequestData>(MockBehavior.Strict, new Mock<FunctionContext>().Object);
            var mockResponse = new Mock<HttpResponseData>(MockBehavior.Strict, new Mock<FunctionContext>().Object);
            var headers = new HttpHeadersCollection();
            
            mockRequest.Setup(r => r.CreateResponse(It.IsAny<HttpStatusCode>())).Returns(mockResponse.Object);
            mockResponse.Setup(r => r.Headers).Returns(headers);
            mockResponse.Setup(r => r.WriteString(It.IsAny<string>())).Callback<string>(content =>
            {
                var responseBody = JsonSerializer.Deserialize<dynamic>(content);
                Assert.NotNull(responseBody);
            }).Returns(mockResponse.Object);

            // Act
            function.Run(mockRequest.Object);

            // Assert
            mockResponse.Verify(r => r.WriteString(It.Is<string>(s => s.Contains("Hello, World!"))), Times.Once);
        }
    }
} 
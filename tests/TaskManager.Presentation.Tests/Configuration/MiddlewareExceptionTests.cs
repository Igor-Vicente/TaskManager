using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using TaskManager.Presentation.Configuration;

namespace TaskManager.Presentation.Tests.Configuration
{
    public class MiddlewareExceptionTests
    {

        [Fact(DisplayName = "Middleware capturar exceção")]
        [Trait("Presentation", "Configuration")]
        public async Task InvokeAsync_DeveLogarErroERetornarStatusCode500_QuandoExcecao()
        {
            // Arrange
            var nextMock = new Mock<RequestDelegate>();
            var loggerMock = new Mock<ILogger<MiddlewareException>>();
            var context = new DefaultHttpContext();
            var exception = new Exception("Test Exception");

            nextMock.Setup(n => n(context)).ThrowsAsync(exception);

            var middleware = new MiddlewareException(nextMock.Object, loggerMock.Object);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            nextMock.Verify(n => n(context), Times.Once);
            context.Response.StatusCode.Should().Be(500);
            context.Response.ContentType.Should().Be("application/json");
        }

        [Fact(DisplayName = "Next middleware")]
        [Trait("Presentation", "Configuration")]
        public async Task InvokeAsync_DevePassarParaProximoMiddleware_QuandoSemExcecao()
        {
            // Arrange
            var nextMock = new Mock<RequestDelegate>();
            var loggerMock = new Mock<ILogger<MiddlewareException>>();
            var context = new DefaultHttpContext();

            var middleware = new MiddlewareException(nextMock.Object, loggerMock.Object);

            // Act
            await middleware.InvokeAsync(context);

            // Assert
            nextMock.Verify(n => n(context), Times.Once);
            loggerMock.VerifyNoOtherCalls();
        }
    }
}

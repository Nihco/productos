using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProductosBFF.Controllers;
using ProductosBFF.Domain.Parameters;
using ProductosBFF.Domain.Universal;
using ProductosBFF.Interfaces.Universal;
using System.Threading.Tasks;
using Xunit;

namespace ProductosBFFTests.ControllerTests
{
    public class UniversalControllerTests
    {
        private readonly Mock<IUniversalInteractor> _mockUniversalInteractor;
        private readonly Mock<ILogger<UniversalController>> _mockLogger;
        private readonly UniversalController _controller;

        public UniversalControllerTests()
        {
            _mockUniversalInteractor = new Mock<IUniversalInteractor>();
            _mockLogger = new Mock<ILogger<UniversalController>>();
            _controller = new UniversalController(_mockUniversalInteractor.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task IngresoUniversal_Success()
        {
            // Arrange
            var ingresoUniversal = new IngresoUniversal();
            var expectedResult = new IngresoUniversalNSD();

            _mockUniversalInteractor
                .Setup(x => x.IngresoUniversal(It.IsAny<IngresoUniversal>()))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.IngresoUniversal(ingresoUniversal) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Equal(expectedResult, result.Value);
        }

        [Fact]
        public async Task IngresoUniversal_NotFound()
        {
            // Arrange
            var ingresoUniversal = new IngresoUniversal();

            _mockUniversalInteractor
                .Setup(x => x.IngresoUniversal(It.IsAny<IngresoUniversal>()))
                .ReturnsAsync((IngresoUniversalNSD)null);

            // Act
            var result = await _controller.IngresoUniversal(ingresoUniversal) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            Assert.Equal("No se ingreso el accidente correctamente", result.Value);
        }

        [Fact]
        public async Task IngresoUniversal_Error()
        {
            // Arrange
            var ingresoUniversal = new IngresoUniversal();

            _mockUniversalInteractor
                .Setup(x => x.IngresoUniversal(It.IsAny<IngresoUniversal>()))
                .ThrowsAsync(new System.Exception("Un Error"));

            // Act
            var result = await _controller.IngresoUniversal(ingresoUniversal) as ObjectResult;

            // Assert
            Assert.Equal(StatusCodes.Status500InternalServerError, result.StatusCode);
            Assert.Equal("No se pudo procesar la solicitud", result.Value.ToString().Trim());
        }

    }
}
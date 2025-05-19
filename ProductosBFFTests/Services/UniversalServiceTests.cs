using Moq;
using ProductosBFF.Domain.Parameters;
using ProductosBFF.Domain.Universal;
using ProductosBFF.Interfaces;
using ProductosBFF.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ProductosBFFTests.Services
{
    public class UniversalServiceTests
    {
        private readonly Mock<IUniversalInfrastructure> _mockUniversalInfrastructure;
        private readonly UniversalService _service;

        public UniversalServiceTests()
        {
            _mockUniversalInfrastructure = new Mock<IUniversalInfrastructure>();
            _service = new UniversalService(_mockUniversalInfrastructure.Object);
        }

        [Fact]
        public async Task IngresoUniversal_Success()
        {
            // Arrange
            var ingresoUniversal = new IngresoUniversal();
            var expectedResult = new IngresoUniversalNSD();

            _mockUniversalInfrastructure
                .Setup(x => x.IngresoUniversal(It.IsAny<IngresoUniversal>()))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _service.IngresoUniversal(ingresoUniversal);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public async Task IngresoUniversal_ThrowsException()
        {
            // Arrange
            var ingresoUniversal = new IngresoUniversal();

            _mockUniversalInfrastructure
                .Setup(x => x.IngresoUniversal(It.IsAny<IngresoUniversal>()))
                .ThrowsAsync(new Exception("Error en la infraestructura"));

            // Act and Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.IngresoUniversal(ingresoUniversal));
        }
    }
}
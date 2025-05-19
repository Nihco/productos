using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProductosBFF.Controllers;
using ProductosBFF.Domain.Parameters;
using ProductosBFF.Interfaces;
using ProductosBFF.Models.Commons;
using ProductosBFF.Models.Productos;
using Xunit;

namespace ProductosBFFTests.ControllerTests
{
    public class PrestadorControllerTest
    {
        private readonly Mock<IPrestadoresService> _prestadorServiceMock;
        private readonly Mock<ILogger<PrestadoresController>> _loggerMock;
        private readonly PrestadoresController _controller;

        public PrestadorControllerTest()
        {
            _prestadorServiceMock = new Mock<IPrestadoresService>();
            _loggerMock = new Mock<ILogger<PrestadoresController>>();
            _controller = new PrestadoresController(_prestadorServiceMock.Object, _loggerMock.Object);
        }
        
        [Fact]
        public async Task GetPrestadores_ReturnsOkObjectResult_WhenPrestadoresExist()
        {
            const long bc = 12345L;
            var expectedDto = new PrestadoresDto
            {
                ListaClase = new List<PrestadorDto>
                {
                    new() { codigo_bc = 1, nombre = "Prestador 1" },
                    new() { codigo_bc = 2, nombre = "Prestador 2" }
                }
            };

            _prestadorServiceMock
                .Setup(service => service.GetPrestadores(It.Is<BodyPrestadores>(b => b.PIN_BC == bc)))
                .ReturnsAsync(expectedDto);
            
            var result = await _controller.GetPrestadores(bc);
          
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<GenericResult<PrestadoresDto>>(okResult.Value);
        }
    }
}
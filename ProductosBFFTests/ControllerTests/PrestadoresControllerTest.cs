using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class PrestadoresControllerTest
    {
        private readonly Mock<IPrestadoresService> _mockPrestadorService;
        private readonly Mock<ILogger<PrestadoresController>> _mockLogger;
        private readonly PrestadoresController _controller;

        public PrestadoresControllerTest()
        {
            _mockPrestadorService = new Mock<IPrestadoresService>();
            _mockLogger = new Mock<ILogger<PrestadoresController>>();
            _controller = new PrestadoresController(_mockPrestadorService.Object, _mockLogger.Object);
        }
        
         [Fact]
        public async Task GetPrestadores_ConBcValidoYServicioRetornaDatos_DebeRetornarOkConGenericResult()
        {
            const long expectedBcValido = 12345L;
            var prestadoresDtoDesdeServicio = new PrestadoresDto
            {
                ListaClase = new List<PrestadorDto>
                {
                    new PrestadorDto { codigo_bc = 1, nombre = "Prestador Encontrado" }
                }
            };

            _mockPrestadorService
                .Setup(s => s.GetPrestadores(It.Is<BodyPrestadores>(bp => bp.PIN_BC == expectedBcValido)))
                .ReturnsAsync(prestadoresDtoDesdeServicio);

            var mensajeEsperado = "Se obtuvieron los prestadores correctamente";
            var statusCodeEsperado = StatusCodes.Status200OK;
            
            var actionResult = await _controller.GetPrestadores(expectedBcValido);
            
            var okObjectResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal(statusCodeEsperado, okObjectResult.StatusCode);

            var genericResult = Assert.IsType<GenericResult<PrestadoresDto>>(okObjectResult.Value);
            Assert.NotNull(genericResult.Result); 
            Assert.Same(prestadoresDtoDesdeServicio, genericResult.Result);

            Assert.NotNull(genericResult.Response);
            Assert.True(genericResult.Response.SuccessfulOperation);
            Assert.Equal(statusCodeEsperado, genericResult.Response.StatusCode);
            Assert.Equal(mensajeEsperado, genericResult.Response.Message);
            Assert.Null(genericResult.Response.Errors); 
        }
        
        [Fact]
        public async Task GetPrestadores_ConBcValidoYServicioRetornaNull_DebeRetornarNotFoundConGenericResult()
        {
            const long expectedBcNoEncontrado = 67890L;
            PrestadoresDto prestadoresNulosDesdeServicio = null;

            _mockPrestadorService
                .Setup(s => s.GetPrestadores(It.Is<BodyPrestadores>(bp => bp.PIN_BC == expectedBcNoEncontrado)))
                .ReturnsAsync(prestadoresNulosDesdeServicio);

            var mensajeEsperado = "No se encontraron prestadores";
            var statusCodeEsperado = StatusCodes.Status404NotFound;
            
            var actionResult = await _controller.GetPrestadores(expectedBcNoEncontrado);
            
            var notFoundObjectResult = Assert.IsType<NotFoundObjectResult>(actionResult);
            Assert.Equal(statusCodeEsperado, notFoundObjectResult.StatusCode);

            var genericResult = Assert.IsType<GenericResult<PrestadoresDto>>(notFoundObjectResult.Value);
            Assert.Null(genericResult.Result);

            Assert.NotNull(genericResult.Response);
            Assert.False(genericResult.Response.SuccessfulOperation);
            Assert.Equal(statusCodeEsperado, genericResult.Response.StatusCode);
            Assert.Equal(mensajeEsperado, genericResult.Response.Message);
            Assert.Null(genericResult.Response.Errors); 
        }
        
        [Fact]
        public async Task GetPrestadores_CuandoServicioLanzaExcepcion_DebeRetornarNotFoundYLoguearError()
        {
            const long expectedBcConErrorServicio = 11223L;
            const string expectedMensajeExcepcion = "Error simulado del servicio";
            var excepcionServicio = new InvalidOperationException(expectedMensajeExcepcion);

            _mockPrestadorService
                .Setup(s => s.GetPrestadores(It.Is<BodyPrestadores>(bp => bp.PIN_BC == expectedBcConErrorServicio)))
                .ThrowsAsync(excepcionServicio);
            
            var actionResult = await _controller.GetPrestadores(expectedBcConErrorServicio);
            
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(actionResult);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
           
            Assert.Equal(expectedMensajeExcepcion, notFoundResult.Value);
            
        }
    }
}
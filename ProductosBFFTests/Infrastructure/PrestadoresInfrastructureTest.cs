using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using ProductosBFF.Domain.Parameters;
using ProductosBFF.Domain.Productos;
using ProductosBFF.Infrastructure;
using ProductosBFF.Interfaces;
using Xunit;

namespace ProductosBFFTests.Infrastructure
{
    public class PrestadoresInfrastructureTest
    {
        private readonly Mock<IHttpClientService> _mockHttpClientService;
        private readonly PrestadoresInfrastructure _infrastructure;

        private const string ConfigKeyFullPath = "PRODUCTO:PRODUCTO_URL";
        private const string ExpectedBaseUrl = "http://api.productos.test/";

        public PrestadoresInfrastructureTest()
        {
            _mockHttpClientService = new Mock<IHttpClientService>();
            var mockConfiguration = new Mock<IConfiguration>();
            var mockConfigurationSection = new Mock<IConfigurationSection>();
            
            mockConfigurationSection.Setup(s => s.Value).Returns(ExpectedBaseUrl);
            
            mockConfiguration.Setup(c => c.GetSection(ConfigKeyFullPath)).Returns(mockConfigurationSection.Object);

            _infrastructure = new PrestadoresInfrastructure(_mockHttpClientService.Object, mockConfiguration.Object);
        }
        
        [Fact]
        public async Task GetPrestadoresAsync_ConBodyValido_DebeLlamarHttpClientConUrlCorrectaYRetornarDatos()
        {
            var bcInput = new BodyPrestadores { PIN_BC = 12345M };
            var urlEsperada = $"{ExpectedBaseUrl}Prestadores/BC/{bcInput.PIN_BC}";

            var prestadoresEsperados = new PrestadoresLeyCorta
            {
                ListaClase = new List<Prestador>
                {
                    new Prestador { CODIGO_BC = "001", NOMBRE = "Prestador Test Infra" }
                }
            };

            _mockHttpClientService
                .Setup(client => client.GetAsync<PrestadoresLeyCorta>(urlEsperada,null,null))
                .ReturnsAsync(prestadoresEsperados);
            
            var resultado = await _infrastructure.GetPrestadoresAsync(bcInput);
            
            Assert.NotNull(resultado);
            Assert.Same(prestadoresEsperados, resultado);
            Assert.Single(resultado.ListaClase);
            Assert.Equal("001", resultado.ListaClase[0].CODIGO_BC);
            Assert.Equal("Prestador Test Infra", resultado.ListaClase[0].NOMBRE);
        }
        
        [Fact]
        public async Task GetPrestadoresAsync_CuandoHttpClientDevuelveListaVacia_DebeRetornarListaVacia()
        { 
            var bcInput = new BodyPrestadores { PIN_BC = 54321M };
            var urlEsperada = $"{ExpectedBaseUrl}Prestadores/BC/{bcInput.PIN_BC}";

            var prestadoresConListaVacia = new PrestadoresLeyCorta
            {
                ListaClase = new List<Prestador>()
            };

            _mockHttpClientService
                .Setup(client => client.GetAsync<PrestadoresLeyCorta>(urlEsperada,null,null))
                .ReturnsAsync(prestadoresConListaVacia);
            
            var resultado = await _infrastructure.GetPrestadoresAsync(bcInput);
            
            Assert.NotNull(resultado);
            Assert.NotNull(resultado.ListaClase);
            Assert.Empty(resultado.ListaClase);

            _mockHttpClientService.Verify(client => client.GetAsync<PrestadoresLeyCorta>(urlEsperada,null,null), Times.Once);
        }
        
        [Fact]
        public async Task GetPrestadoresAsync_CuandoHttpClientDevuelveNull_DebeRetornarNull()
        {
            var bcInput = new BodyPrestadores { PIN_BC = 67890M };
            var urlEsperada = $"{ExpectedBaseUrl}Prestadores/BC/{bcInput.PIN_BC}";
            PrestadoresLeyCorta prestadoresNulos = null;

            _mockHttpClientService
                .Setup(client => client.GetAsync<PrestadoresLeyCorta>(urlEsperada,null,null))
                .ReturnsAsync(prestadoresNulos);
            
            var resultado = await _infrastructure.GetPrestadoresAsync(bcInput);
            
            Assert.Null(resultado);
            _mockHttpClientService.Verify(client => client.GetAsync<PrestadoresLeyCorta>(urlEsperada,null,null), Times.Once);
        }
        
        [Fact]
        public async Task GetPrestadoresAsync_CuandoHttpClientLanzaExcepcion_DebePropagarExcepcion()
        {
            var bcInput = new BodyPrestadores { PIN_BC = 11223M };
            var urlEsperada = $"{ExpectedBaseUrl}Prestadores/BC/{bcInput.PIN_BC}";
          
            var excepcionHttp = new HttpRequestException("Error de red simulado durante la llamada GET.");

            _mockHttpClientService
                .Setup(client => client.GetAsync<PrestadoresLeyCorta>(urlEsperada,null,null))
                .ThrowsAsync(excepcionHttp);
            
            var excepcionReal = await Assert.ThrowsAsync<HttpRequestException>(() => _infrastructure.GetPrestadoresAsync(bcInput));
            Assert.Same(excepcionHttp, excepcionReal);

            _mockHttpClientService.Verify(client => client.GetAsync<PrestadoresLeyCorta>(urlEsperada,null,null), Times.Once);
        }
    }
}
using System.Collections.Generic;
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
    public class PrestadorInfrastructureTests
    {
        private readonly Mock<IHttpClientService> _httpClientServiceMock;
        private readonly PrestadoresInfrastructure _prestadorInfrastructure;
        public PrestadorInfrastructureTests()
        {
            _httpClientServiceMock = new Mock<IHttpClientService>();
            
            var configurationSectionMock = new Mock<IConfigurationSection>();
            configurationSectionMock.Setup(x => x.Value)
                .Returns("https://mock-url.com/prestadores");
            var config = new Mock<IConfiguration>();
            config.Setup(x => x.GetSection("PRODUCTO:LEYCORTA_URL"))
                .Returns(configurationSectionMock.Object);
            config.Setup(x => x.GetSection("PRODUCTO:PRODUCTO_URL"))
                .Returns(configurationSectionMock.Object);
        

        _prestadorInfrastructure = new PrestadoresInfrastructure(_httpClientServiceMock.Object, config.Object);
        }

        [Fact]
        public async Task GetPrestadoresAsync_ShouldReturnCorrectData()
        {
            var body = new BodyPrestadores { PIN_BC = 12345 };
            var expectedResponse = new PrestadoresLeyCorta
            {
                ListaClase = new List<Prestador>
                {
                    new() { CODIGO_BC = "001", NOMBRE = "Prestador 1" },
                    new() { CODIGO_BC = "002", NOMBRE = "Prestador 2" }
                }
            };

            _httpClientServiceMock
                .Setup(client => client.GetAsync<PrestadoresLeyCorta>(It.IsAny<string>(),null,null))
                .ReturnsAsync(expectedResponse);
            
            var result = await _prestadorInfrastructure.GetPrestadoresAsync(body);
            
            Assert.NotNull(result);
            Assert.Equal(2, result.ListaClase.Count);
            Assert.Equal("001", result.ListaClase[0].CODIGO_BC);
            Assert.Equal("Prestador 1", result.ListaClase[0].NOMBRE);
        }
    }
}
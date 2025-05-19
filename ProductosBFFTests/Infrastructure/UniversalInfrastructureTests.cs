using Microsoft.Extensions.Configuration;
using Moq;
using ProductosBFF.Domain.Parameters;
using ProductosBFF.Domain.Universal;
using ProductosBFF.Infrastructure;
using ProductosBFF.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace ProductosBFFTests.Infrastructure
{
    public class UniversalInfrastructureTests
    {
        private readonly Mock<IHttpClientService> _httpClientServiceMock;
        private readonly Mock<IConfiguration> _config;
        private readonly string _baseUrl;


        public UniversalInfrastructureTests()
        {
            _httpClientServiceMock = new Mock<IHttpClientService>();
            _baseUrl = "https://elapi.cl/api/";
            var configurationSectionMock = new Mock<IConfigurationSection>();
            _config = new Mock<IConfiguration>();
            configurationSectionMock
                .Setup(x => x.Value)
                .Returns(_baseUrl);
            _config.Setup(c => c.GetSection("PRODUCTO:PRODUCTO_URL")).Returns(configurationSectionMock.Object);
        }

        [Fact]
        public async Task IngresoUniversal_Ok()
        {
            var paramEntada = new IngresoUniversal()
            {
                Apellidos = "Paterno Materno",
                CausaAccidente = 1
            };

            var expectedResponse = new IngresoUniversalNSD
            {
                pon_id_siniestro = 1
            };

            _httpClientServiceMock.Setup(service => service.PostAsync<IngresoUniversalNSD>(
                It.Is<string>(url => url == _baseUrl + "Universal/IngresoUniversal"),
                It.Is<IngresoUniversal>(param => param == paramEntada),
                null,
                null))
            .ReturnsAsync(expectedResponse);
            var result = await new UniversalInfrastructure(_httpClientServiceMock.Object, _config.Object).IngresoUniversal(paramEntada);

            Assert.NotNull(result);
            Assert.Equal(1, result.pon_id_siniestro);
        }
    }
}
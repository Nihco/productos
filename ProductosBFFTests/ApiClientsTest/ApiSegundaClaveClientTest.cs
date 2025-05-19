using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using ProductosBFF.ApiClients;
using ProductosBFF.Interfaces;
using ProductosBFF.Models.SegundaClave;
using Xunit;

namespace ProductosBFFTests.ApiClientsTest
{
    public class ApiSegundaClaveClientTest
    {
        private readonly Mock<IHttpClientService> _mockHttpClientService;
        private readonly ApiSegundaClaveClient _apiClient;
        private const string TestUrl = "http://fake-api.com/api/";
        private const string ConfigKey = "PRODUCTO:PRODUCTO_URL";

        public ApiSegundaClaveClientTest()
        {
            var mockLogger = new Mock<ILogger<ApiSegundaClaveClient>>();
            var mockConfiguration = new Mock<IConfiguration>();
            _mockHttpClientService = new Mock<IHttpClientService>();

            var mockConfSection = new Mock<IConfigurationSection>();
            mockConfSection.Setup(s => s.Value).Returns(TestUrl);
            mockConfiguration.Setup(c => c.GetSection(ConfigKey)).Returns(mockConfSection.Object);
            mockConfiguration.Setup(c => c[ConfigKey]).Returns(TestUrl);


            _apiClient = new ApiSegundaClaveClient(
                mockLogger.Object,
                mockConfiguration.Object,
                _mockHttpClientService.Object
            );
        }

        [Fact]
        public async Task SolicitarSegundaClaveAsync_Success_ReturnsSegundaClaveDto()
        {
            var solicitud = new SolicitudClaveDto
                { Rut = 12345678, Dv = "9", Folio = 1, DescripcionTransaccion = "Test Solicitud" };
            var expectedResponse = new SegundaClaveDto { CodigoEstado = "OK", TtlSegundos = 180 };
            var expectedUrl = TestUrl + "SegundaClave/SolicitarSegundaClave";

            _mockHttpClientService
                .Setup(s => s.PostAsync<SegundaClaveDto>(
                    It.Is<string>(url => url == expectedUrl),
                    It.Is<SolicitudClaveDto>(req => req == solicitud),
                    It.IsAny<object>(),
                    It.IsAny<Dictionary<string, string>>()))
                .ReturnsAsync(expectedResponse);
            var result = await _apiClient.SolicitarSegundaClaveAsync(solicitud);

            Assert.NotNull(result);
            Assert.Equal(expectedResponse.CodigoEstado, result.CodigoEstado);
            Assert.Equal(expectedResponse.TtlSegundos, result.TtlSegundos);
        }

        [Fact]
        public async Task SolicitarSegundaClaveAsync_ApiReturnsNull_ReturnsEmptyDtoAndLogsError()
        {
            var solicitud = new SolicitudClaveDto
                { Rut = 12345678, Dv = "9", Folio = 1, DescripcionTransaccion = "Test Solicitud Null" };
            const string expectedUrl = TestUrl + "SegundaClave/SolicitarSegundaClave";

            _mockHttpClientService
                .Setup(s => s.PostAsync<SegundaClaveDto>(
                    expectedUrl,
                    solicitud,
                    It.IsAny<object>(),
                    It.IsAny<Dictionary<string, string>>()))
                .ReturnsAsync((SegundaClaveDto)null);

            var result = await _apiClient.SolicitarSegundaClaveAsync(solicitud);

            Assert.NotNull(result);
            Assert.Null(result.CodigoEstado);
            Assert.Equal(0, result.TtlSegundos);
        }

        [Fact]
        public async Task
            SolicitarSegundaClaveAsync_HttpClientThrowsException_ThrowsInvalidOperationExceptionAfterRetries()
        {
            var solicitud = new SolicitudClaveDto
                { Rut = 12345678, Dv = "9", Folio = 1, DescripcionTransaccion = "Test Exception" };
            const string expectedUrl = TestUrl + "SegundaClave/SolicitarSegundaClave";
            var apiException = new Exception("API communication failed");

            _mockHttpClientService
                .Setup(s => s.PostAsync<SegundaClaveDto>(
                    expectedUrl,
                    solicitud,
                    It.IsAny<object>(),
                    It.IsAny<Dictionary<string, string>>()))
                .ThrowsAsync(apiException);

            var caughtException =
                await Assert.ThrowsAsync<InvalidOperationException>(() =>
                    _apiClient.SolicitarSegundaClaveAsync(solicitud));

            Assert.Contains($"Error al consumir el servicio REST {TestUrl}", caughtException.Message);
            Assert.Contains(apiException.Message, caughtException.Message);
        }

        [Fact]
        public async Task VerificarSegundaClaveAsync_Success_ReturnsVerificadoDto()
        {
            var verificacion = new VerificarSegundaClaveDto
            {
                Folio = 1, Codigo = "123456", Ip = "1.1.1.1", Rut = 12345678, Dv = "9",
                DescripcionTransaccion = "Test Verify"
            };
            var expectedResponse = new VerificadoDto { CodigoEstado = "VALIDO", IdAuditoria = 999 };
            const string expectedUrl = TestUrl + "SegundaClave/VerificarSegundaClave";

            _mockHttpClientService
                .Setup(s => s.PostAsync<VerificadoDto>(
                    It.Is<string>(url => url == expectedUrl),
                    It.Is<VerificarSegundaClaveDto>(req => req == verificacion),
                    It.IsAny<object>(),
                    It.IsAny<Dictionary<string, string>>()))
                .ReturnsAsync(expectedResponse);
            
            var result = await _apiClient.VerificarSegundaClaveAsync(verificacion);
            
            Assert.NotNull(result);
            Assert.Equal(expectedResponse.CodigoEstado, result.CodigoEstado);
            Assert.Equal(expectedResponse.IdAuditoria, result.IdAuditoria);
        }
        
        [Fact]
        public async Task VerificarSegundaClaveAsync_ApiReturnsNull_ReturnsEmptyDtoAndLogsError()
        {
            var verificacion = new VerificarSegundaClaveDto { Folio = 1, Codigo = "123456", Ip = "1.1.1.1", Rut = 12345678, Dv = "9", DescripcionTransaccion = "Test Verify Null" };
            const string expectedUrl = TestUrl + "SegundaClave/VerificarSegundaClave";

            _mockHttpClientService
                .Setup(s => s.PostAsync<VerificadoDto>(
                    expectedUrl,
                    verificacion,
                    It.IsAny<object>(),
                    It.IsAny<Dictionary<string, string>>()))
                .ReturnsAsync((VerificadoDto)null);
            
            var result = await _apiClient.VerificarSegundaClaveAsync(verificacion);
            
            Assert.NotNull(result);
            Assert.Null(result.CodigoEstado);
            Assert.Equal(0, result.IdAuditoria);
        }
        
        [Fact]
        public async Task VerificarSegundaClaveAsync_HttpClientThrowsException_ThrowsInvalidOperationExceptionAfterRetries()
        {
            var verificacion = new VerificarSegundaClaveDto { Folio = 1, Codigo = "123456", Ip = "1.1.1.1", Rut = 12345678, Dv = "9", DescripcionTransaccion = "Test Verify Exception" };
            const string expectedUrl = TestUrl + "SegundaClave/VerificarSegundaClave";
            var apiException = new System.Net.Http.HttpRequestException("Network Error");

            _mockHttpClientService
                .Setup(s => s.PostAsync<VerificadoDto>(
                    expectedUrl,
                    verificacion,
                    It.IsAny<object>(),
                    It.IsAny<Dictionary<string, string>>()))
                .ThrowsAsync(apiException);
            
            var caughtException = await Assert.ThrowsAsync<InvalidOperationException>(() => _apiClient.VerificarSegundaClaveAsync(verificacion));

            Assert.Contains($"Error al consumir el servicio REST {TestUrl}", caughtException.Message);
            Assert.Contains(apiException.Message, caughtException.Message);
        }

    }
}
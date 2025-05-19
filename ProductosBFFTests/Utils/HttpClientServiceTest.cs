using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using ProductosBFF.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace ProductosBFFTests.Utils
{
    public class HttpClientServiceTest
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<IConfiguration> _configMock;
        private readonly Mock<ILogger<HttpClientService>> _loggerMock;
        private readonly HttpClientService _httpClientService;

        public HttpClientServiceTest()
        {
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _configMock = new Mock<IConfiguration>();
            _loggerMock = new Mock<ILogger<HttpClientService>>();
            _configMock.Setup(c => c["PRODUCTO_HEADER_NAME"]).Returns("headerName");
            _configMock.Setup(c => c["PRODUCTO_HEADER_VALUE"]).Returns("headerValue");
            _httpClientService = new HttpClientService(_httpClientFactoryMock.Object, _configMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAsync_ReturnsData_OnSuccess()
        {
            var expectedData = new { Name = "Test" };
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedData))
            };

            var httpClientMock = new Mock<HttpMessageHandler>();
            httpClientMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(responseMessage);

            var client = new HttpClient(httpClientMock.Object);
            _httpClientFactoryMock.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(client);

            var result = await _httpClientService.GetAsync<object>("https://test.com/api");

            Assert.NotNull(result);
            Assert.Equal(expectedData.Name, ((dynamic)result).Name.ToString());
        }

        [Fact]
        public async Task GetAsync_LogsError_OnNotFound()
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.NotFound);
            var httpClientMock = new Mock<HttpMessageHandler>();
            httpClientMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(responseMessage);

            var client = new HttpClient(httpClientMock.Object);
            _httpClientFactoryMock.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(client);

            var result = await _httpClientService.GetAsync<object>("https://test.com/api");

            Assert.Null(result);
        }

        [Fact]
        public void AddHeaders_AddsConfiguredHeaders()
        {
            var request = new HttpRequestMessage();
            _configMock.Setup(c => c["PRODUCTO_HEADER_NAME"]).Returns("headerName");
            _configMock.Setup(c => c["PRODUCTO_HEADER_VALUE"]).Returns("headerValue");

            var headers = new Dictionary<string, string>
            {
                { "headerName", "value" }
            };

            _httpClientService.GetType()
                .GetMethod("AddHeaders", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.Invoke(_httpClientService, new object[] { request, headers });

            Assert.True(request.Headers.Contains("headerName"));
            Assert.Equal("headerValue", request.Headers.GetValues("headerName").First());
        }
    }
}
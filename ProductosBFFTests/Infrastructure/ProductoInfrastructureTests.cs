using Moq;
using ProductosBFF.Domain.Parameters;
using ProductosBFF.Infrastructure;
using ProductosBFF.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ProductosBFF.Domain.Causales;
using ProductosBFF.Domain.Productos;
using Xunit;

namespace ProductosBFFTests.Infrastructure
{
    public class ProductoInfrastructureTests
    {
        private readonly Mock<IHttpClientService> _httpClientServiceMock;
       
        private readonly ProductoInfrastructure _productoInfrastructure;

        public ProductoInfrastructureTests()
        {
            _httpClientServiceMock = new Mock<IHttpClientService>();
            var configurationSectionMock = new Mock<IConfigurationSection>();
            var config = new Mock<IConfiguration>();
            configurationSectionMock
                .Setup(x => x.Value)
                .Returns("http://someservice");
            config.Setup(c => c.GetSection("PRODUCTO:PRODUCTO_URL")).Returns(configurationSectionMock.Object);
            _productoInfrastructure = new ProductoInfrastructure(_httpClientServiceMock.Object, config.Object);
        }

        [Fact]
        public async Task ContinuidadProducto_Ok()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"PRODUCTO", "PRODUCTO_URL"},
                {"SectionName:SomeKey", "SectionValue"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();
            
            configuration.GetValue<string>("PRODUCTO:PRODUCTO_URL");

            const bool expectedContinuity = true;
            var continuidadProducto = new ContinuidadProducto();

            _httpClientServiceMock.Setup(service => service.GetAsync<bool>(
                It.Is<string>(url => url.Contains("ContinuidadProducto")),
                continuidadProducto, 
                null 
            )).ReturnsAsync(expectedContinuity);
            
            var result = await _productoInfrastructure.ContinuidadProducto(continuidadProducto);
            
            Assert.True(result);
        }

        [Fact]
        public async Task ContinuidadProducto_No_Ok()
        {
            var continuidadProducto = new ContinuidadProducto(); 

            _httpClientServiceMock.Setup(service => service.GetAsync<bool>(
                It.Is<string>(url => url.Contains("ContinuidadProducto")),
                continuidadProducto,  
                null 
            )).ReturnsAsync(false);
            
            var result = await _productoInfrastructure.ContinuidadProducto(continuidadProducto);
            
            Assert.False(result);  
        }

        [Fact]
        public async Task RegistraContinuidadCesantia_No_Ok()
        {
            const int rut = 987654321;

            _httpClientServiceMock.Setup(service => service.PutAsync<int>(
                It.Is<string>(url => url.Contains($"ContinuidadProducto/RegistraContinuidadCesantia?rut={rut}")),
                null, 
                null,  
                null   
            )).ReturnsAsync(0);
          
            var result = await _productoInfrastructure.RegistraContinuidadCesantia(rut);
        
            Assert.Equal(0, result);
        }
        
        [Fact]
        public async Task GetMotivosCancelacion_ReturnsMotivos_WhenApiCallSucceeds()
        {
            var expectedMotivos = new List<MotivosCancelacion>
            {
                new() { Codigo = "1", Descripcion = "Motivo 1" },
                new() { Codigo = "2", Descripcion = "Motivo 2" }
            };
            
            _httpClientServiceMock.Setup(service => service.GetAsync<List<MotivosCancelacion>>(
                It.Is<string>(url => url.Contains("Causales/GetMotivosCancelacion")), // Verify the URL
                null,
                null  
            )).ReturnsAsync(expectedMotivos);
            
            var result = await _productoInfrastructure.GetMotivosCancelacion();
          
            Assert.Equal(expectedMotivos, result); 
        }
        
        [Fact]
        public async Task CancelaBcCostoCero_Ok()
        {
            var inMemorySettings = new Dictionary<string, string> {
                {"PRODUCTO", "PRODUCTO_URL"},
                {"SectionName:SomeKey", "SectionValue"}
            };

            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            configuration.GetValue<string>("PRODUCTO:PRODUCTO_URL");

            const bool expectedContinuity = true;
            var continuidadProducto = new BodyCancelarParamDto();

            _httpClientServiceMock.Setup(service => service.GetAsync<bool>(
                It.Is<string>(url => url.Contains("ContinuidadProducto")),
                continuidadProducto,
                null
            )).ReturnsAsync(expectedContinuity);

            var result = await _productoInfrastructure.CancelarBcCostoCero(continuidadProducto);

            Assert.IsType<decimal>(result);
        }
        
        [Fact]
        public async Task GetProductosCostoCero_Ok()
        {
            const decimal expectedTestRut = 12345678M;
            var expectedProducts = new List<ProductosCostoCero>
            {
                new() { CODIGO = "COD1", NOMBRE = "Producto 1" },
                new() { CODIGO = "COD2", NOMBRE = "Producto 2" }
            };

            _httpClientServiceMock.Setup(service => service.GetAsync<List<ProductosCostoCero>>(
                It.Is<string>(url => url.Contains($"Products/ProductosCostoCero/{expectedTestRut}")), // Verify correct URL
                null,
                null  
            )).ReturnsAsync(expectedProducts);
            
            var result = await _productoInfrastructure.GetProductosCostoCero(expectedTestRut);
            
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal(expectedProducts.Count, result.Count);
            Assert.Equal(expectedProducts, result); 
        }
        
        [Fact]
        public async Task GetProductosCostoCero_NoData()
        {
            const decimal expectedTestRut = 98765432;
            var expectedProducts = new List<ProductosCostoCero>(); 

            _httpClientServiceMock.Setup(service => service.GetAsync<List<ProductosCostoCero>>(
                It.IsAny<string>(),
                null,
                null
            )).ReturnsAsync(expectedProducts);
            
            var result = await _productoInfrastructure.GetProductosCostoCero(expectedTestRut);
            
            Assert.NotNull(result);
            Assert.Empty(result);
        }
        
        [Fact]
        public async Task GetProductosCostoCero_HandlesException_WhenApiCallFails()
        {
            const decimal expectedTestRut = 55555555;
            _httpClientServiceMock.Setup(service => service.GetAsync<List<ProductosCostoCero>>(
                It.IsAny<string>(),  
                null,   
                null    
            )).ThrowsAsync(new System.Exception("API call failed"));
            
            var exception = await Assert.ThrowsAsync<System.Exception>(() => _productoInfrastructure.GetProductosCostoCero(expectedTestRut));
            Assert.Equal("API call failed", exception.Message);
        }
        
        [Fact]
        public async Task ValidaBcCostoCero_ReturnsTrue_WhenApiCallSucceedsAndReturnsTrue()
        {
            const int expectedFolio = 12345;
            const int expectedDomiCodigo = 789;
            string expectedUrl = $"Products/ValidaBcCostoCero/{expectedFolio}/{expectedDomiCodigo}";

            _httpClientServiceMock.Setup(service => service.GetAsync<bool>(
                    It.Is<string>(url => url == expectedUrl),
                    null,
                    null 
                ))
                .ReturnsAsync(true);
            
            var result = await _productoInfrastructure.ValidaBcCostoCero(expectedFolio, expectedDomiCodigo);
            
            Assert.False(result);
        }
    }
}
using AutoMapper;
using Moq;
using ProductosBFF.Domain.Parameters;
using ProductosBFF.Domain.Productos;
using ProductosBFF.Exceptions;
using ProductosBFF.Interfaces;
using ProductosBFF.Models.BCCesantia;
using ProductosBFF.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProductosBFF.Domain.Causales;
using ProductosBFF.Domain.SegundaClave;
using ProductosBFF.Models.Causales;
using ProductosBFF.Models.Productos;
using Xunit;

namespace ProductosBFFTests.Services
{
    public class ProductoServiceTests
    {
        private readonly Mock<IProductoInfrastructure> _mProductoInfrastructure;
        private readonly Mock<IMapper> _mIMapper;
        private readonly ProductoService _service;

        public ProductoServiceTests()
        {
            _mProductoInfrastructure = new Mock<IProductoInfrastructure>();
            _mIMapper = new Mock<IMapper>();
            var mockLogger = new Mock<ILogger<ProductoService>>();
            var mockConfiguration = new Mock<IConfiguration>();
            _service = new ProductoService(_mProductoInfrastructure.Object, _mIMapper.Object, mockConfiguration.Object,
                mockLogger.Object);
        }

        private readonly BodySolicitudActivacion _bodySolicitudActivacion = new()
        {
            RutAfil = 111,
            Imagen = "Test"
        };

        private readonly DocCesantia _contentResultOk = new()
        {
            SuccessfulOperation = true,
            Result = new Result { EstadoCarga = "OK", Idcontent = "123123123123123213" }
        };


        [Fact]
        public async Task GetContinuidadProductoService_Ok()
        {
            //Arrange
            _mProductoInfrastructure.Setup(x => x.ContinuidadProducto(It.IsAny<ContinuidadProducto>()))
                .Returns(Task.FromResult(true));

            //Act
            var resp = await new ProductoService(_mProductoInfrastructure.Object, _mIMapper.Object, null, null)
                .GetContinuidadProductoService(new ContinuidadProducto { rut = 111 });

            //Assert
            Assert.IsType<bool>(resp);
            Assert.True(resp);
            _mProductoInfrastructure.VerifyAll();
        }

        [Fact]
        public async Task GetContinuidadProductoService_No_Ok()
        {
            //Arrange
            _mProductoInfrastructure.Setup(x => x.ContinuidadProducto(It.IsAny<ContinuidadProducto>()))
                .Returns(Task.FromResult(false));

            //Act
            var resp = await new ProductoService(_mProductoInfrastructure.Object, _mIMapper.Object, null, null)
                .GetContinuidadProductoService(new ContinuidadProducto { rut = 111 });

            //Assert
            Assert.IsType<bool>(resp);
            Assert.False(resp);
            _mProductoInfrastructure.VerifyAll();
        }

        [Fact]
        public async Task GetContinuidadProductoService_Exception()
        {
            //Arrange
            _mProductoInfrastructure.Setup(x => x.ContinuidadProducto(It.IsAny<ContinuidadProducto>()))
                .Throws(new InvalidOperationException("Error al consumir el servicio REST"));

            //Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                new ProductoService(_mProductoInfrastructure.Object, _mIMapper.Object, null, null)
                    .GetContinuidadProductoService(new ContinuidadProducto() { rut = 111 }));
            Assert.Equal("Error al consumir el servicio REST", exception.Message);
        }

        [Fact]
        public async Task RegistraSolicitudContinuidadCesantia_Todo_Ok()
        {
            _mProductoInfrastructure.Setup(x => x.EnviarDatosDocBCCesantia(It.IsAny<DtoDocCm>()))
                .Returns(Task.FromResult(_contentResultOk));
            _mProductoInfrastructure.Setup(x => x.RegistraContinuidadCesantia(_bodySolicitudActivacion.RutAfil))
                .Returns(Task.FromResult(33));
            _mProductoInfrastructure.Setup(x => x.EnviarArchivosSini(It.IsAny<BodyBcArchivo>()))
                .Returns(Task.FromResult(new ResponseEnviarArchivo()));
            var resp = await new ProductoService(_mProductoInfrastructure.Object, _mIMapper.Object, null, null)
                .RegistraSolicitudContinuidadCesantia(_bodySolicitudActivacion);

            Assert.IsType<int>(resp);
            Assert.Equal(33, resp);
            _mProductoInfrastructure.VerifyAll();
        }

        [Fact]
        public async Task RegistraSolicitudContinuidadCesantia_EnviarDatosDocBCCesantia_Exception()
        {
            //Arrange
            _mProductoInfrastructure.Setup(x => x.EnviarDatosDocBCCesantia(It.IsAny<DtoDocCm>()))
                .Throws(new ContentManagerException("Error al consumir el servicio REST"));

            //Act
            var exception = await Assert.ThrowsAsync<ContentManagerException>(() =>
                new ProductoService(_mProductoInfrastructure.Object, _mIMapper.Object, null, null)
                    .RegistraSolicitudContinuidadCesantia(_bodySolicitudActivacion));
            Assert.Equal("Error al cargar archivo: Error al consumir el servicio REST", exception.Message);
            _mProductoInfrastructure.VerifyAll();
        }

        [Fact]
        public async Task RegistraSolicitudContinuidadCesantia_RegistraContinuidadCesantia_0()
        {
            _mProductoInfrastructure.Setup(x => x.EnviarDatosDocBCCesantia(It.IsAny<DtoDocCm>()))
                .Returns(Task.FromResult(_contentResultOk));
            _mProductoInfrastructure.Setup(x => x.RegistraContinuidadCesantia(_bodySolicitudActivacion.RutAfil))
                .Returns(Task.FromResult(0));

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                new ProductoService(_mProductoInfrastructure.Object, _mIMapper.Object, null, null)
                    .RegistraSolicitudContinuidadCesantia(_bodySolicitudActivacion));
            Assert.Equal("Error al ingresar datos: No se pudo registrar el siniestro", exception.Message);
            _mProductoInfrastructure.VerifyAll();
        }

        [Fact]
        public async Task RegistraSolicitudContinuidadCesantia_RegistraContinuidadCesantia_Exception()
        {
            _mProductoInfrastructure.Setup(x => x.EnviarDatosDocBCCesantia(It.IsAny<DtoDocCm>()))
                .Returns(Task.FromResult(_contentResultOk));
            _mProductoInfrastructure.Setup(x => x.RegistraContinuidadCesantia(_bodySolicitudActivacion.RutAfil))
                .Throws(new InvalidOperationException("Error al consumir el servicio REST"));

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                new ProductoService(_mProductoInfrastructure.Object, _mIMapper.Object, null, null)
                    .RegistraSolicitudContinuidadCesantia(_bodySolicitudActivacion));
            Assert.Equal("Error al ingresar datos: Error al consumir el servicio REST", exception.Message);
            _mProductoInfrastructure.VerifyAll();
        }

        [Fact]
        public async Task RegistraSolicitudContinuidadCesantia_EnviarArchivosSini_Exception()
        {
            _mProductoInfrastructure.Setup(x => x.EnviarDatosDocBCCesantia(It.IsAny<DtoDocCm>()))
                .Returns(Task.FromResult(_contentResultOk));
            _mProductoInfrastructure.Setup(x => x.RegistraContinuidadCesantia(_bodySolicitudActivacion.RutAfil))
                .Returns(Task.FromResult(33));
            _mProductoInfrastructure.Setup(x => x.EnviarArchivosSini(It.IsAny<BodyBcArchivo>()))
                .Throws(new InvalidOperationException("Error al consumir el servicio REST"));

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                new ProductoService(_mProductoInfrastructure.Object, _mIMapper.Object, null, null)
                    .RegistraSolicitudContinuidadCesantia(_bodySolicitudActivacion));
            Assert.Equal("Error al ingresar datos: Error al consumir el servicio REST", exception.Message);
            _mProductoInfrastructure.VerifyAll();
        }

        [Fact]
        public async Task GetProductos_Ok()
        {
            var bodyProducto = new BodyProducto { PIN_RUT = 12345678 };
            var productos = new List<Producto>
            {
                new() { CODIGO = "1", NOMBRE = "Producto 1" },
                new() { CODIGO = "2", NOMBRE = "Producto 2" }
            };

            var costoCeroList = new List<ProductosCostoCero>
            {
                new() { CODIGO = "CC1", NOMBRE = "Costo Cero 1" },
                new() { CODIGO = "CC2", NOMBRE = "Costo Cero 2" }
            };

            var mappedProductDtos = new List<ProductDto>
            {
                new() { id = 1, name = "Producto 1" },
                new() { id = 2, name = "Producto 2" }
            };

            var mappedCostoCeroDtos = new List<ProductDto>
            {
                new() { id = 3, name = "Costo Cero 1" },
                new() { id = 4, name = "Costo Cero 2" }
            };

            var expectedDtos = new List<ProductDto>();
            expectedDtos.AddRange(mappedProductDtos);
            expectedDtos.AddRange(mappedCostoCeroDtos);

            _mProductoInfrastructure.Setup(x => x.GetProductsAsync(bodyProducto)).ReturnsAsync(productos);
            _mProductoInfrastructure.Setup(x => x.GetProductosCostoCero(bodyProducto.PIN_RUT))
                .ReturnsAsync(costoCeroList);

            _mIMapper.Setup(m => m.Map<List<Producto>, List<ProductDto>>(productos)).Returns(mappedProductDtos);
            _mIMapper.Setup(m => m.Map<List<ProductosCostoCero>, List<ProductDto>>(costoCeroList))
                .Returns(mappedCostoCeroDtos);

            var result = await _service.GetProductos(bodyProducto);


            Assert.NotNull(result);
            Assert.Equal(expectedDtos.Count, result.Count);

            Assert.Equal(mappedProductDtos[0].id, result[0].id);
            Assert.Equal(mappedProductDtos[0].name, result[0].name);
            Assert.Equal(mappedProductDtos[1].id, result[1].id);
            Assert.Equal(mappedProductDtos[1].name, result[1].name);

            Assert.Equal(mappedCostoCeroDtos[0].id, result[2].id);
            Assert.Equal(mappedCostoCeroDtos[0].name, result[2].name);
            Assert.Equal(mappedCostoCeroDtos[1].id, result[3].id);
            Assert.Equal(mappedCostoCeroDtos[1].name, result[3].name);

            foreach (var dto in result)
            {
                Assert.False(dto.FlagMantencion);
            }
        }

        [Fact]
        public async Task GetProductosLeyCorta_Ok()
        {
            const int rut = 12345678;
            var productosLeyCorta = new List<ProductoLeyCorta>
            {
                new() { codigo = "001", nombre = "Producto Ley Corta 1" },
                new() { codigo = "002", nombre = "Producto Ley Corta 2" }
            };

            var mappedProductLeyCortaDtos = new List<ProductLeyCortaDto>
            {
                new() { id = "001", name = "Producto Ley Corta 1" },
                new() { id = "002", name = "Producto Ley Corta 2" }
            };

            _mProductoInfrastructure.Setup(x => x.GetProductosLeyCorta(rut)).ReturnsAsync(productosLeyCorta);
            _mIMapper.Setup(m => m.Map<List<ProductoLeyCorta>, List<ProductLeyCortaDto>>(productosLeyCorta))
                .Returns(mappedProductLeyCortaDtos);

            var result = await _service.GetProductosLeyCorta(rut);

            Assert.NotNull(result);
            Assert.Equal(mappedProductLeyCortaDtos.Count, result.Count);
            Assert.Equal(mappedProductLeyCortaDtos, result);
        }

        [Fact]
        public async Task GetMotivos_Ok()
        {
            var motivosCancelacion = new List<MotivosCancelacion>
            {
                new() { Codigo = "1", Descripcion = "Motivo 1" },
                new() { Codigo = "2", Descripcion = "Motivo 2" }
            };

            var expectedMotivosDto = new List<MotivosCancelacionDto>
            {
                new() { Codigo = "1", Descripcion = "Motivo 1" },
                new() { Codigo = "2", Descripcion = "Motivo 2" }
            };

            _mProductoInfrastructure.Setup(i => i.GetMotivosCancelacion()).ReturnsAsync(motivosCancelacion);
            _mIMapper.Setup(m => m.Map<List<MotivosCancelacion>, List<MotivosCancelacionDto>>(motivosCancelacion))
                .Returns(expectedMotivosDto);

            var result = await _service.GetMotivos();

            Assert.NotNull(result);
        }

       [Fact]
    public async Task CancelaBcCostoCero_ReturnsExpectedResponse_WhenInfrastructureReturnsData()
    {
        const string expectedPinFirmaAsString = "9876";
        if (!int.TryParse(expectedPinFirmaAsString, out var expectedPinFirmaAsInt))
        {
            throw new InvalidOperationException("Test setup error: expectedPinFirmaAsString is not a valid integer.");
        }

        var bodyParam = new BodyCancelarParamDto
        {
            pin_folio = 12345,
            pin_codigo_bc = "BC1",
            pin_codigo_motivo = "MOT1",
            pin_firma = expectedPinFirmaAsString,
            pi_dFecha_Autor = DateTime.MinValue,
            pi_vIp_Cliente = "initial_ip",
        };
        
        var expectedAuditoriaResponse = new ResponseAuditoria
        {
            Ip = "10.0.0.5",
            FechaAutorizacion = DateTime.Now.AddMinutes(-10),
            IdOperacion = 55,
            CodigoOperacion = "AUDIT01",
            Rut = 11223344,
            Dv = "5"
        };
        
        const decimal expectedNroSolicitud = 100011;
        
        var expectedComprobanteResponse = new CancelaBcCostoCeroResponse
        {
            nroSolicitud = 999,
            disponibleHasta = DateTime.Now.AddDays(15), 
            documentoBase64 = "base64string_from_mock", 
            fechaTerminoBeneficio = DateTime.Now.AddDays(30)
        };
     
        _mProductoInfrastructure.Setup(i => i.ObtenerAuditoria(expectedPinFirmaAsInt))
            .ReturnsAsync(expectedAuditoriaResponse);
        
        _mProductoInfrastructure.Setup(i => i.CancelarBcCostoCero(It.Is<BodyCancelarParamDto>(p =>
                p.pin_folio == bodyParam.pin_folio &&
                p.pin_codigo_bc == bodyParam.pin_codigo_bc &&
                p.pin_codigo_motivo == bodyParam.pin_codigo_motivo &&
                p.pin_firma == bodyParam.pin_firma &&
                p.pi_vIp_Cliente == expectedAuditoriaResponse.Ip 
            )))
            .ReturnsAsync(expectedNroSolicitud);
      
        _mProductoInfrastructure.Setup(i => i.ObtenerComprobante(It.Is<ObtenerComprobanteParam>(p =>
                p.Folio == bodyParam.pin_folio.ToString() &&
                p.Canal == "SUDI" &&
                p.Codigo == bodyParam.pin_codigo_bc &&
                p.LogTermino == expectedNroSolicitud.ToString() 
            )))
            .ReturnsAsync(expectedComprobanteResponse); 
        
        var result = await _service.CancelaBcCostoCero(bodyParam);
        
        Assert.NotNull(result);
        
        Assert.Equal(expectedNroSolicitud, result.nroSolicitud);
        Assert.Equal(expectedComprobanteResponse.documentoBase64, result.documentoBase64);
        Assert.Equal(expectedComprobanteResponse.fechaTerminoBeneficio, result.disponibleHasta);
        
        Assert.IsType<DateTime>(result.disponibleHasta);
        Assert.IsType<decimal>(result.nroSolicitud);
        Assert.IsType<string>(result.documentoBase64);
    }

        [Fact]
        public async Task DetalleSolicitudVC_ReturnsMappedDtoDetalleSolicitudVC_WhenDataIsFound()
        {
            var bodyHistorialCabecera = new BodyHistorialCabecera();
            var detalleSolicitudDomain = new List<ResponseDetalleSolicitudVC>
            {
                new() { NOMBRE_BENEFICIARIO = "Juan Pérez" },
                new() { NOMBRE_BENEFICIARIO = "María Gómez" }
            };

            var mappedDetalleSolicitudDtos = new List<DtoDetalleSolicitudVC>
            {
                new() { beneficiario = "Juan Pérez" },
                new() { beneficiario = "María Gómez" }
            };

            _mProductoInfrastructure.Setup(x => x.DetalleSolicitudVC(bodyHistorialCabecera))
                .ReturnsAsync(detalleSolicitudDomain);
            _mIMapper.Setup(m => m.Map<List<DtoDetalleSolicitudVC>>(detalleSolicitudDomain))
                .Returns(mappedDetalleSolicitudDtos);

            var result = await _service.DetalleSolicitudVC(bodyHistorialCabecera);

            Assert.NotNull(result);
            Assert.Equal(mappedDetalleSolicitudDtos.Count, result.Count);
            Assert.Equal("Juan pérez", result[0].beneficiario);
            Assert.Equal("María gómez", result[1].beneficiario);
        }

        [Fact]
        public async Task DetalleSolicitudCesantia_ReturnsMappedDtoDetalleSolicitudCesantia_WhenDataIsFound()
        {
            var bodyHistorialCabecera = new BodyHistorialCabecera { pin_id_siniestro = 12345 };
            var detalleSolicitudDomain = new List<ResponseDetalleSolicitudCesantia>
            {
                new() { NOMBRE_BENEFICIARIO = "Juan Pérez", CAUSAL_DESPIDO = "Causal 1" },
                new() { NOMBRE_BENEFICIARIO = "María Gómez", CAUSAL_DESPIDO = "Causal 2" }
            };

            var mappedDetalleSolicitudDtos = new List<DtoDetalleSolicitudCesantia>
            {
                new() { beneficiario = "Juan Pérez", causalDespido = "Causal 1" },
                new() { beneficiario = "María Gómez", causalDespido = "Causal 2" }
            };

            _mProductoInfrastructure.Setup(x => x.DetalleSolicitudCesantia(bodyHistorialCabecera))
                .ReturnsAsync(detalleSolicitudDomain);
            _mIMapper.Setup(m => m.Map<List<DtoDetalleSolicitudCesantia>>(detalleSolicitudDomain))
                .Returns(mappedDetalleSolicitudDtos);

            var result = await _service.DetalleSolicitudCesantia(bodyHistorialCabecera);

            Assert.NotNull(result);
            Assert.Equal(mappedDetalleSolicitudDtos.Count, result.Count);
            Assert.Equal("Juan pérez", result[0].beneficiario);
            Assert.Equal("Causal 1", result[0].causalDespido);
            Assert.Equal("María gómez", result[1].beneficiario);
            Assert.Equal("Causal 2", result[1].causalDespido);
        }

        [Fact]
        public async Task HistorialSolicitudes_ReturnsMappedDtoHistorialSolicitudes_WhenDataIsFound()
        {
            var bodyHistorialSolicitudes = new BodyHistorialSolicitudes { pin_folio = 12345 };
            var historialDomainList = new List<ResponseHistorialSolicitudes>
            {
                new ResponseHistorialSolicitudes { tipo_solicitud = "Solicitud 1", Id_Solicitud = 1 },
                new ResponseHistorialSolicitudes { tipo_solicitud = "Solicitud 2", Id_Solicitud = 2 }
            };

            var mappedHistorialDtos = new List<DtoHistorialSolicitudes>
            {
                new DtoHistorialSolicitudes { tipoSolicitud = "Solicitud 1", id = 1 },
                new DtoHistorialSolicitudes { tipoSolicitud = "Solicitud 2", id = 2 }
            };

            _mProductoInfrastructure.Setup(x => x.HistorialSolicitudes(bodyHistorialSolicitudes))
                .ReturnsAsync(historialDomainList);
            _mIMapper.Setup(m => m.Map<List<DtoHistorialSolicitudes>>(historialDomainList))
                .Returns(mappedHistorialDtos);

            var result = await _service.HistorialSolicitudes(bodyHistorialSolicitudes);

            Assert.NotNull(result);
            Assert.Equal(mappedHistorialDtos.Count, result.Count);
            Assert.Equal("Solicitud 1", result[0].tipoSolicitud);
            Assert.Equal(1, result[0].id);
            Assert.Equal("Solicitud 2", result[1].tipoSolicitud);
            Assert.Equal(2, result[1].id);
        }

        [Fact]
        public async Task ValidaBcCostoCero_ReturnsTrue_WhenInfrastructureReturnsTrue()
        {
            const int expectedFolio = 123;
            const int expectedDomiCodigo = 456;
            _mProductoInfrastructure.Setup(infra => infra.ValidaBcCostoCero(expectedFolio, expectedDomiCodigo))
                .ReturnsAsync(true);

            var result = await _service.ValidaBcCostoCero(expectedFolio, expectedDomiCodigo);

            Assert.True(result);
        }

        [Fact]
        public async Task ValidaBcCostoCero_ReturnsFalse_WhenInfrastructureReturnsFalse()
        {
            const int folio = 789;
            const int domiCodigo = 101;
            _mProductoInfrastructure.Setup(infra => infra.ValidaBcCostoCero(folio, domiCodigo))
                .ReturnsAsync(false);

            var result = await _service.ValidaBcCostoCero(folio, domiCodigo);

            Assert.False(result);
        }

        [Fact]
        public async Task ValidaBcCostoCero_ThrowsOriginalException_WhenInfrastructureThrowsException()
        {
            const int folio = 222;
            const int domiCodigo = 333;
            var expectedException = new HttpRequestException("Infrastructure layer failed");
            _mProductoInfrastructure.Setup(infra => infra.ValidaBcCostoCero(folio, domiCodigo))
                .ThrowsAsync(expectedException);

            var actualException = await Assert.ThrowsAsync<HttpRequestException>(
                () => _service.ValidaBcCostoCero(folio, domiCodigo)
            );

            Assert.Same(expectedException, actualException);
        }
    }
}
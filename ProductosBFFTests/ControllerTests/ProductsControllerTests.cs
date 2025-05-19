using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using ProductosBFF.Controllers;
using ProductosBFF.Domain.Parameters;
using ProductosBFF.Exceptions;
using ProductosBFF.Interfaces;
using ProductosBFF.Models.Accidentes;
using ProductosBFF.Models.BCCesantia;
using ProductosBFF.Models.Causales;
using ProductosBFF.Models.Commons;
using ProductosBFF.Models.Productos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ProductosBFFTests.ControllerTests
{
    public class ProductsControllerTests
    {
        private readonly ProductsController _productsController;
        private readonly Mock<IProductoService> _productoServiceMock = new();
        private readonly Mock<ILogger<ProductsController>> _loggerMock = new();
        private readonly BodySolicitudActivacion _bodySolicitudActivacion = new()
        {
            RutAfil = 111,
            Imagen = "Test"
        };
        

        public ProductsControllerTests()
        {
            var httpContext = new DefaultHttpContext();
            _productsController = new ProductsController(_productoServiceMock.Object, _loggerMock.Object)
                {
                    ControllerContext = new ControllerContext()
                    {
                        HttpContext = httpContext
                    }
                };
        }

        private static List<ProductDto> GetProductosDto()
        {
            return new List<ProductDto>
            {
                new()
                {
                    id = 398,
                    name = "ACCIDENTE PROTEGIDO LIBRE ELECCION",
                    amountUf = "UF 0,153/mes",
                    valueAprox = "$5.177",
                    fecha_inicio = "fecha_inicio",
                    usar_desde = "usar_desde",
                    codigo_plan = "codigo_plan",
                    url_contrato = "url_contrato",
                    plazo_uso = "plazo_uso",
                    texto_etiqueta = "texto_etiqueta",
                    color_etiqueta = "color_etiqueta",
                    icono = "icono",
                    plazo_valido = true,
                    texto_solicitud = "texto_solicitud",
                    familia = "familia",
                    es_multicotizante = false,
                    es_colectivo = true,
                    tipoActivacion = "tipoActivacion",
                    es_cesantia = true,
                    es_urgencia = true,
                    es_vida_camara = true,
                    es_familia_protegida = true,
                    EsCostoCero = false,
                    FechaCancelado = DateTime.Now,
                    DisponibleHasta = DateTime.Now,
                    DuracionBCGratis = 123,
                    SeEstaCobrandoBc = true,
                    SePuedeCancelar = true
                },
                new()
                {
                    id = 363,
                    name = "ASISTENCIA EN VIAJE MUNDO TOTAL",
                    amountUf = "UF 0,213/mes",
                    valueAprox = "$7.207",
                    fecha_inicio = "fecha_inicio",
                    usar_desde = "usar_desde",
                    codigo_plan = "codigo_plan",
                    url_contrato = "url_contrato",
                    plazo_uso = "plazo_uso",
                    texto_etiqueta = "texto_etiqueta",
                    color_etiqueta = "color_etiqueta",
                    icono = "icono",
                    plazo_valido = true,
                    texto_solicitud = "texto_solicitud",
                    familia = "familia",
                    es_multicotizante = false,
                    es_colectivo = true,
                    tipoActivacion = "tipoActivacion",
                    es_cesantia = true,
                    es_urgencia = true,
                    es_vida_camara = true,
                    es_familia_protegida = true,
                    EsCostoCero = false,
                    FechaCancelado = DateTime.Now,
                    DisponibleHasta = DateTime.Now,
                    DuracionBCGratis = 123,
                    SeEstaCobrandoBc = true,
                    SePuedeCancelar = true
                },
                new()
                {
                    id = 403,
                    name = "BONIFICACI�N EN FARMACIA PLUS",
                    amountUf = "UF 0,264/mes",
                    valueAprox = "$8.933",
                    fecha_inicio = "fecha_inicio",
                    usar_desde = "usar_desde",
                    codigo_plan = "codigo_plan",
                    url_contrato = "url_contrato",
                    plazo_uso = "plazo_uso",
                    texto_etiqueta = "texto_etiqueta",
                    color_etiqueta = "color_etiqueta",
                    icono = "icono",
                    plazo_valido = true,
                    texto_solicitud = "texto_solicitud",
                    familia = "familia",
                    es_multicotizante = false,
                    es_colectivo = true,
                    tipoActivacion = "tipoActivacion",
                    es_cesantia = true,
                    es_urgencia = true,
                    es_vida_camara = true,
                    es_familia_protegida = true,
                    EsCostoCero = false,
                    FechaCancelado = DateTime.Now,
                    DisponibleHasta = DateTime.Now,
                    DuracionBCGratis = 123,
                    SeEstaCobrandoBc = true,
                    SePuedeCancelar = true
                }
            };
        }

        private static DocumentoBCDto GetDocumentsBase64Dto()
        {
            return new DocumentoBCDto()
            {
                Base64 = "JVBERi0xLjQKJeLjz9MKMSAwIG9iago8PC9UeXBlL1hPYmplY3QvU3VidHlwZS9JbWFn",
                Cabecera = "data:application/pdf;base64,",
                Titulo = "Beneficio Complementario N� 321"
            };
        }

        [Fact]
        public async Task Get_ProductosAdicionales_ByRut_Returns_Ok()
        {
            //Arrange
            _productoServiceMock.Setup(x => x.GetProductos(It.IsAny<BodyProducto>()))
                .ReturnsAsync(GetProductosDto());

            //Act
            var result = await _productsController.GetProducts(It.IsAny<long>());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Get_ProductosAdicionales_ByRut_Returns_Null()
        {
            //Arrange
            List<ProductDto> products = null;
            _productoServiceMock.Setup(x => x.GetProductos(It.IsAny<BodyProducto>()))
                .ReturnsAsync(products);

            //Act
            var result = await _productsController.GetProducts(It.IsAny<long>());

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
        
        [Fact]
        public async Task GetProducts_Exception()
        {
            const long expectedRut = 12345678L;
            const string expectedExceptionMessage = "exception";
            var expectedException = new Exception(expectedExceptionMessage);

            _productoServiceMock.Setup(x => x.GetProductos(It.IsAny<BodyProducto>()))
                .ThrowsAsync(expectedException);
            
            _loggerMock.Setup(x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(), 
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
            ));
            
            var result = await _productsController.GetProducts(expectedRut);
            
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task Get_DownloadBC_Returns_Ok()
        {
            //Arrange
            _productoServiceMock.Setup(x => x.GetDocumentsBase64(It.IsAny<ProductoBC>()))
                .ReturnsAsync(GetDocumentsBase64Dto());

            //Act
            var result = await _productsController.DownloadDocumentBC(It.IsAny<string>());

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Get_DownloadBC_Returns_Null()
        {
            //Arrange
            DocumentoBCDto documentBase64 = new()
            {
                Base64 = "Ha ocurrido un error al obtener el PDF",
                Cabecera = null,
                Titulo = null
            };

            _productoServiceMock.Setup(x => x.GetDocumentsBase64(It.IsAny<ProductoBC>()))
                .ReturnsAsync(documentBase64);

            //Act
            var result = await _productsController.DownloadDocumentBC(It.IsAny<string>());

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }



        [Fact]
        public async Task GetCausalesDespidos()
        {
            List<CausalesDto> causalesList = new()
            {
                new CausalesDto { Id_Causales_Despido = "1", Causales_Despido = "Test1", Id_Tipo_Trabajador = "Tipo1" },
                new CausalesDto { Id_Causales_Despido = "2", Causales_Despido = "Test2", Id_Tipo_Trabajador = "Tipo2" }
            };

            _productoServiceMock.Setup(s => s.GetCausales()).ReturnsAsync(causalesList);

            ActionResult result = await _productsController.GetCausalesDespidos();

            OkObjectResult okObjectResult = Assert.IsType<OkObjectResult>(result);
            List<CausalesDto> returnValue = Assert.IsType<List<CausalesDto>>(okObjectResult.Value);
            Assert.Equal(2, returnValue.Count);
        }
        
        [Fact]
        public async Task GetCausalesDespidos_Exception()
        {
            const string expectedExceptionMessage = "exception";
            var expectedException = new Exception(expectedExceptionMessage);

            _productoServiceMock.Setup(x => x.GetCausales())
                .ThrowsAsync(expectedException);
            
            _loggerMock.Setup(x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
            ));
            
            var result = await _productsController.GetCausalesDespidos();
            
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task Get_Responde_Ok_True()
        {
            _productoServiceMock.Setup(x => x.GetContinuidadProductoService(It.IsAny<ContinuidadProducto>())).Returns(Task.FromResult(true));

            var resp = await _productsController.Get(111);
            var respObj = resp as OkObjectResult;

            Assert.IsType<OkObjectResult>(resp);
            Assert.Equal(200, respObj.StatusCode);
            Assert.True((respObj.Value as GenericResult<bool>).Result);
            _productoServiceMock.VerifyAll();
        }

        [Fact]
        public async Task Get_Responde_Ok_False()
        {
            //Arrange
            _productoServiceMock.Setup(x => x.GetContinuidadProductoService(It.IsAny<ContinuidadProducto>())).Returns(Task.FromResult(false));

            //Act
            var resp = await _productsController.Get(111);
            var respObj = resp as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(resp);
            Assert.Equal(200, respObj.StatusCode);
            Assert.False((respObj.Value as GenericResult<bool>).Result);
            _productoServiceMock.VerifyAll();
        }

        [Fact]
        public async Task Get_Responde_NotFound()
        {
            //Arrange
            _productoServiceMock.Setup(x => x.GetContinuidadProductoService(It.IsAny<ContinuidadProducto>())).Throws(new InvalidOperationException("Error al consumir el servicio REST"));

            //Act
            var resp = await _productsController.Get(111);
            var respObj = resp as NotFoundResult;

            //Assert
            Assert.IsType<NotFoundResult>(resp);
            Assert.Equal(404, respObj.StatusCode);
            _productoServiceMock.VerifyAll();
        }

        [Fact]
        public async Task RegistraSolicitudContinuidadCesantia_Responde_Ok()
        {
            _productoServiceMock.Setup(x => x.RegistraSolicitudContinuidadCesantia(It.IsAny<BodySolicitudActivacion>())).Returns(Task.FromResult(33));

            var resp = await _productsController.RegistraSolicitudContinuidadCesantia(_bodySolicitudActivacion);
            var respObj = resp as OkObjectResult;

            Assert.IsType<OkObjectResult>(resp);
            Assert.Equal(200, respObj.StatusCode);
            Assert.Equal(33, respObj.Value);
            _productoServiceMock.VerifyAll();
        }

        [Fact]
        public async Task RegistraSolicitudContinuidadCesantia_Responde_NotFound()
        {
            //Arrange
            _productoServiceMock.Setup(x => x.RegistraSolicitudContinuidadCesantia(It.IsAny<BodySolicitudActivacion>())).Throws(new InvalidOperationException("Error al ingresar datos No se pudo registrar el siniestro"));

            //Act
            var resp = await _productsController.RegistraSolicitudContinuidadCesantia(_bodySolicitudActivacion);
            var respObj = resp as NotFoundObjectResult;

            //Assert
            Assert.IsType<NotFoundObjectResult>(resp);
            Assert.Equal(404, respObj.StatusCode);
            Assert.Equal("Error al ingresar datos No se pudo registrar el siniestro", respObj.Value);
            _productoServiceMock.VerifyAll();
        }

        [Fact]
        public async Task RegistraSolicitudContinuidadCesantia_Responde_Conflict()
        {
            //Arrange
            _productoServiceMock.Setup(x => x.RegistraSolicitudContinuidadCesantia(It.IsAny<BodySolicitudActivacion>())).Throws(new ContentManagerException("Error al cargar archivo: Error al consumir el servicio REST"));

            //Act
            var resp = await _productsController.RegistraSolicitudContinuidadCesantia(_bodySolicitudActivacion);
            var respObj = resp as ConflictObjectResult;

            //Assert
            Assert.IsType<ConflictObjectResult>(resp);
            Assert.Equal(409, respObj.StatusCode);
            Assert.Equal("Error al cargar archivo: Error al consumir el servicio REST", respObj.Value);
            _productoServiceMock.VerifyAll();
        }

        [Fact]
        public async Task HistorialSolicitudes_Returns_Ok()
        {
            var historialSolicitudes = new List<DtoHistorialSolicitudes>();

            _productoServiceMock.Setup(x => x.HistorialSolicitudes(It.IsAny<BodyHistorialSolicitudes>()))
                .ReturnsAsync(historialSolicitudes);

            var result = await _productsController.HistorialSolicitudes(123);

            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<DtoHistorialSolicitudes>>(okResult.Value);
        }

        [Fact]
        public async Task DetalleSolicitudCesantia_Returns_Ok()
        {
            var detalleSolicitudCesantia = new List<DtoDetalleSolicitudCesantia>();

            _productoServiceMock.Setup(x => x.DetalleSolicitudCesantia(It.IsAny<BodyHistorialCabecera>()))
                .ReturnsAsync(detalleSolicitudCesantia);

            var result = await _productsController.DetalleSolicitudCesantia(123);

            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<DtoDetalleSolicitudCesantia>>(okResult.Value);
        }

        [Fact]
        public async Task DetalleSolicitudVC_Returns_Ok()
        {
            var detalleSolicitudVc = new List<DtoDetalleSolicitudVC>();

            _productoServiceMock.Setup(x => x.DetalleSolicitudVC(It.IsAny<BodyHistorialCabecera>()))
                .ReturnsAsync(detalleSolicitudVc);

            var result = await _productsController.DetalleSolicitudVC(123);

            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<List<DtoDetalleSolicitudVC>>(okResult.Value);
        }

        [Fact]
        public async Task ProcesoSolicitud_Returns_Ok()
        {
            var traZaPadreDtoList = new DtoResponseTrazaPadre();

            _productoServiceMock.Setup(x => x.SolicitudesTrazaPadre(It.IsAny<TrazaPadreParam>()))
                .ReturnsAsync(traZaPadreDtoList);

            var result = await _productsController.ProcesoSolicitud(123);

            Assert.NotNull(result);
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.IsType<DtoResponseTrazaPadre>(okResult.Value);
        }

        [Fact]
        public async Task EnviarDatosCesantia_ShouldReturnOk_WhenDataIsSentSuccessfully()
        {
            var expectedResponse = new ResponseBcAfilSini();
            _productoServiceMock.Setup(service => service.EnviarDatosCesantia(It.IsAny<BodySolicitudActivacion>()))
                .ReturnsAsync(expectedResponse);

            var result = await _productsController.EnviarDatosCesantia(new BodySolicitudActivacion());

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResponse, okResult.Value);
        }

        [Fact]
        public async Task EnviarDatosCesantia_ShouldReturnNotFound_WhenExceptionIsThrown()
        {
            _productoServiceMock.Setup(service => service.EnviarDatosCesantia(It.IsAny<BodySolicitudActivacion>()))
                .Throws(new Exception());

            var result = await _productsController.EnviarDatosCesantia(new BodySolicitudActivacion());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task PostEnvioCorreo_Ok()
        {
            var envioCorreo = new EnvioCorreo();
            var expectedResponse = new ResponseEnvioCorreo();

            _productoServiceMock.Setup(service => service.EnviarCorreo(envioCorreo))
                .ReturnsAsync(expectedResponse);

            var result = await _productsController.PostEnvioCorreo(envioCorreo);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GenericResult<ResponseEnvioCorreo>>(okResult.Value);
            Assert.Equal(expectedResponse, returnValue.Result);
        }

        [Fact]
        public async Task PostGenerarFun4_Ok()
        {
            var datosFun4 = new DtoFun4();
            var expectedResponse = new Fun4 { Codigo = 0 };

            _productoServiceMock.Setup(service => service.EnviarFun4(datosFun4))
                .ReturnsAsync(expectedResponse);

            var result = await _productsController.PostGenerarFun4(datosFun4);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(expectedResponse.Codigo, okResult.Value);
        }

        [Fact]
        public async Task PostGenerarFun4_NotFound()
        {
            var datosFun4 = new DtoFun4();
            var expectedResponse = new Fun4 { Codigo = 1 };

            _productoServiceMock.Setup(service => service.EnviarFun4(datosFun4))
                .ReturnsAsync(expectedResponse);

            var result = await _productsController.PostGenerarFun4(datosFun4);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(expectedResponse.Codigo, notFoundResult.Value);
        }

        [Fact]
        public async Task PostGenerarFun4_Exception()
        {
            var datosFun4 = new DtoFun4();

            _productoServiceMock.Setup(service => service.EnviarFun4(datosFun4))
                .ThrowsAsync(new Exception("Error al generar Fun4"));

            var result = await _productsController.PostGenerarFun4(datosFun4);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ConsultaTipoAccidente_Ok()
        {
            var expectedTiposAccidente = new List<TipoAccidenteDto>
            {
                new(),
                new()
            };

            _productoServiceMock.Setup(service => service.ObtenerTipoAccidente())
                .ReturnsAsync(expectedTiposAccidente);

            var result = await _productsController.ConsultaTipoAccidente();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GenericResult<List<TipoAccidenteDto>>>(okResult.Value);
            Assert.Equal(expectedTiposAccidente, returnValue.Result);
        }

        [Fact]
        public async Task ConsultaTipoAccidente_NotFound()
        {
            _productoServiceMock.Setup(service => service.ObtenerTipoAccidente())
                .ReturnsAsync((List<TipoAccidenteDto>)null);

            var result = await _productsController.ConsultaTipoAccidente();

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var returnValue = Assert.IsType<GenericResult<List<TipoAccidenteDto>>>(notFoundResult.Value);
            Assert.Null(returnValue.Result);
        }

        [Fact]
        public async Task ConsultaTipoAccidente_Exception()
        {
            _productoServiceMock.Setup(service => service.ObtenerTipoAccidente())
                .ThrowsAsync(new Exception("Error al consultar tipos de accidente"));

            var result = await _productsController.ConsultaTipoAccidente();

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DocumentosAccidente_Ok()
        {
            var documentosAccidente = new BodyDocumentosAccidente();
            var expectedDocumentos = new List<DocumentosAccidenteDto>
            {
                new(),
                new()
            };

            _productoServiceMock.Setup(service => service.ObtenerDocumentosAccidente(documentosAccidente))
                .ReturnsAsync(expectedDocumentos);

            var result = await _productsController.DocumentosAccidente(documentosAccidente);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GenericResult<List<DocumentosAccidenteDto>>>(okResult.Value);
            Assert.Equal(expectedDocumentos, returnValue.Result);
        }

        [Fact]
        public async Task DocumentosAccidente_NotFound()
        {
            var documentosAccidente = new BodyDocumentosAccidente();

            _productoServiceMock.Setup(service => service.ObtenerDocumentosAccidente(documentosAccidente))
                .ReturnsAsync((List<DocumentosAccidenteDto>)null);

            var result = await _productsController.DocumentosAccidente(documentosAccidente);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var returnValue = Assert.IsType<GenericResult<List<DocumentosAccidenteDto>>>(notFoundResult.Value);
            Assert.Null(returnValue.Result);
        }

        [Fact]
        public async Task DocumentosAccidente_Exception()
        {
            var documentosAccidente = new BodyDocumentosAccidente();

            _productoServiceMock.Setup(service => service.ObtenerDocumentosAccidente(documentosAccidente))
                .ThrowsAsync(new Exception("Error al consultar documentos de accidente"));

            var result = await _productsController.DocumentosAccidente(documentosAccidente);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task IngresarArchivoAccidente_Ok()
        {
            var ingresarArchivoAccidente = new BodyIngresarArchivoAccidente();
            var expectedResponse = new IngresarArchivoAccidenteDto();

            _productoServiceMock.Setup(service => service.IngresarArchivoAccidente(ingresarArchivoAccidente))
                .ReturnsAsync(expectedResponse);

            var result = await _productsController.IngresarArchivoAccidente(ingresarArchivoAccidente);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GenericResult<IngresarArchivoAccidenteDto>>(okResult.Value);
            Assert.Equal(expectedResponse, returnValue.Result);
        }

        [Fact]
        public async Task IngresarArchivoAccidente_NotFound()
        {
            var ingresarArchivoAccidente = new BodyIngresarArchivoAccidente();

            _productoServiceMock.Setup(service => service.IngresarArchivoAccidente(ingresarArchivoAccidente))
                .ReturnsAsync((IngresarArchivoAccidenteDto)null);

            var result = await _productsController.IngresarArchivoAccidente(ingresarArchivoAccidente);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var returnValue = Assert.IsType<GenericResult<IngresarArchivoAccidenteDto>>(notFoundResult.Value);
            Assert.Null(returnValue.Result);
        }

        [Fact]
        public async Task IngresarArchivoAccidente_Exception()
        {
            var ingresarArchivoAccidente = new BodyIngresarArchivoAccidente();

            _productoServiceMock.Setup(service => service.IngresarArchivoAccidente(ingresarArchivoAccidente))
                .ThrowsAsync(new Exception("Error al ingresar archivo accidente"));

            var result = await _productsController.IngresarArchivoAccidente(ingresarArchivoAccidente);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task IngresarAccidente_Ok()
        {
            var ingresarAccidente = new BodyIngresarAccidente();
            var expectedResponse = new IngresarAccidenteDto();

            _productoServiceMock.Setup(service => service.IngresarAccidente(ingresarAccidente))
                .ReturnsAsync(expectedResponse);

            var result = await _productsController.IngresarAccidente(ingresarAccidente);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GenericResult<IngresarAccidenteDto>>(okResult.Value);
            Assert.Equal(expectedResponse, returnValue.Result);
        }

        [Fact]
        public async Task IngresarAccidente_NotFound()
        {
            var ingresarAccidente = new BodyIngresarAccidente();

            _productoServiceMock.Setup(service => service.IngresarAccidente(ingresarAccidente))
                .ReturnsAsync((IngresarAccidenteDto)null);

            var result = await _productsController.IngresarAccidente(ingresarAccidente);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var returnValue = Assert.IsType<GenericResult<IngresarAccidenteDto>>(notFoundResult.Value);
            Assert.Null(returnValue.Result);
        }

        [Fact]
        public async Task IngresarAccidente_Exception()
        {
            var ingresarAccidente = new BodyIngresarAccidente();

            _productoServiceMock.Setup(service => service.IngresarAccidente(ingresarAccidente))
                .ThrowsAsync(new Exception("Error al ingresar accidente"));
            var result = await _productsController.IngresarAccidente(ingresarAccidente);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CargaArchivosShira_Ok()
        {
            var cargarArchivoShira = new BodyCargarArchivoShira();
            var expectedResponse = new CargarArchivoShiraDto();

            _productoServiceMock.Setup(service => service.CargaArchivosShira(cargarArchivoShira))
                .ReturnsAsync(expectedResponse);

            var result = await _productsController.CargaArchivosShira(cargarArchivoShira);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GenericResult<CargarArchivoShiraDto>>(okResult.Value);
            Assert.Equal(expectedResponse, returnValue.Result);
        }

        [Fact]
        public async Task CargaArchivosShira_NotFound()
        {
            var cargarArchivoShira = new BodyCargarArchivoShira();

            _productoServiceMock.Setup(service => service.CargaArchivosShira(cargarArchivoShira))
                .ReturnsAsync((CargarArchivoShiraDto)null);

            var result = await _productsController.CargaArchivosShira(cargarArchivoShira);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var returnValue = Assert.IsType<GenericResult<CargarArchivoShiraDto>>(notFoundResult.Value);
            Assert.Null(returnValue.Result);
        }

        [Fact]
        public async Task CargaArchivosShira_Exception()
        {
            var cargarArchivoShira = new BodyCargarArchivoShira();

            _productoServiceMock.Setup(service => service.CargaArchivosShira(cargarArchivoShira))
                .ThrowsAsync(new Exception("Error al cargar archivo a Shira"));

            var result = await _productsController.CargaArchivosShira(cargarArchivoShira);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CargaArchivosContent_Ok()
        {
            var cargarArchivoContent = new BodyCargarArchivoContent();
            var expectedResponse = new CargarArchivoContentDto();

            _productoServiceMock.Setup(service => service.CargaArchivosContent(cargarArchivoContent))
                .ReturnsAsync(expectedResponse);

            var result = await _productsController.CargaArchivosContent(cargarArchivoContent);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GenericResult<CargarArchivoContentDto>>(okResult.Value);
            Assert.Equal(expectedResponse, returnValue.Result);
        }

        [Fact]
        public async Task CargaArchivosContent_NotFound()
        {
            var cargarArchivoContent = new BodyCargarArchivoContent();

            _productoServiceMock.Setup(service => service.CargaArchivosContent(cargarArchivoContent))
                .ReturnsAsync((CargarArchivoContentDto)null);

            var result = await _productsController.CargaArchivosContent(cargarArchivoContent);

            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            var returnValue = Assert.IsType<GenericResult<CargarArchivoContentDto>>(notFoundResult.Value);
            Assert.Null(returnValue.Result);
        }

        [Fact]
        public async Task CargaArchivosContent_Exception()
        {
            var cargarArchivoContent = new BodyCargarArchivoContent();

            _productoServiceMock.Setup(service => service.CargaArchivosContent(cargarArchivoContent))
                .ThrowsAsync(new Exception("Error al cargar archivo a Content"));

            var result = await _productsController.CargaArchivosContent(cargarArchivoContent);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetProductosLeyCorta_Ok()
        {
            var mockProducts = new List<ProductLeyCortaDto>
            {
                new() { id = "1", name = "Nombre Bc1" },
                new() { id = "2", name = "Nombre Bc2" }
            };
            _productoServiceMock.Setup(service => service.GetProductosLeyCorta(It.IsAny<decimal>()))
                .ReturnsAsync(mockProducts);

            var result = await _productsController.GetProductosLeyCorta(12345678m);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GenericResult<List<ProductLeyCortaDto>>>(okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, returnValue.Response.StatusCode);
            Assert.Equal(mockProducts, returnValue.Result);
            Assert.True(returnValue.Response.SuccessfulOperation);
        }

        [Fact]
        public async Task GetProductosLeyCorta_Exception()
        {
            const decimal expectedRut = 98765432M;
            const string expectedExceptionMessage = "exception";
            var expectedException = new Exception(expectedExceptionMessage);

            _productoServiceMock.Setup(x => x.GetProductosLeyCorta(expectedRut))
                .ThrowsAsync(expectedException);
            
            _loggerMock.Setup(x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()
            ));
            
            var result = await _productsController.GetProductosLeyCorta(expectedRut);
            
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
        }
        
        [Fact]
        public async Task ConsultaCausaAccidente_Ok()
        {
            var mockCausales = new List<CausalAccidenteDto>
            {
                new() { Id = 1, Descripcion = "Causal 1" },
                new() { Id = 2, Descripcion = "Causal 2" }
            };

            _productoServiceMock.Setup(service => service.ObtenerCausalAccidente(It.IsAny<ParamTipoAccidente>()))
                .ReturnsAsync(mockCausales);

            var result = await _productsController.ConsultaCausaAccidente(123);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<GenericResult<List<CausalAccidenteDto>>>(okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, returnValue.Response.StatusCode);
            Assert.Equal(mockCausales, returnValue.Result);
            Assert.True(returnValue.Response.SuccessfulOperation);
        }

        [Fact]
        public async Task ObtenerDiagnosticoVc_ReturnsOkObjectResult_WhenDiagnosticoIsFound()
        {
            // Arrange
            var mockDiagnostico = new List<DtoResponseDiagnostico>
            {
                new()
                {
                    descripcion_bc = "Diagn�stico1",
                    id_siniestro = 1
                },
                new()
                {
                    descripcion_bc = "Diagn�stico2",
                    id_siniestro = 2
                }
            };

            _productoServiceMock.Setup(service => service.ObtenerDiagnosticoVc(It.IsAny<BodyObtenerDiagnostico>()))
                .ReturnsAsync(mockDiagnostico);

            // Act
            var result = await _productsController.ObtenerDiagnosticoVc(new BodyObtenerDiagnostico());

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<DtoResponseDiagnostico>>(okResult.Value);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
            Assert.Equal(mockDiagnostico, returnValue);
        }

        
        [Fact]
        public async Task GetMotivosCancelacion_Ok()
        {
            var expectedMotivos = new List<MotivosCancelacionDto>
            {
                new() { Codigo = "1", Descripcion = "Motivo 1" },
                new() { Codigo = "2", Descripcion = "Motivo 2" }
            };

            _productoServiceMock.Setup(s => s.GetMotivos()).ReturnsAsync(expectedMotivos);
            
            var result = await _productsController.GetMotivosCancelacion();
            
            var okResult = Assert.IsType<OkObjectResult>(result); 
            var motivos = Assert.IsType<List<MotivosCancelacionDto>>(okResult.Value);
            Assert.NotNull(motivos); 
        }
        
        [Fact]
        public async Task GetMotivosCancelacion_ReturnsNotFoundObject_WhenServiceReturnsNull()
        {
            _productoServiceMock.Setup(s => s.GetMotivos()).ReturnsAsync((List<MotivosCancelacionDto>)null);
            
            var result = await _productsController.GetMotivosCancelacion();
            
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, notFoundResult.StatusCode);
            
            var genericResult = Assert.IsType<GenericResult<List<MotivosCancelacionDto>>>(notFoundResult.Value);
            Assert.Null(genericResult.Result);
            Assert.Equal(500, genericResult.Response.StatusCode);
            Assert.Equal("Error al obtener el motivos cancelacion", genericResult.Response.Message);
            _productoServiceMock.Verify(s => s.GetMotivos(), Times.Once);
        }
        
        [Fact]
        public async Task ValidarBcCostoCero_ReturnsOkTrue_WhenServiceReturnsTrue()
        {
            const int folio = 12345;
            const int domiCodigo = 789;
            _productoServiceMock.Setup(service => service.ValidaBcCostoCero(folio, domiCodigo))
                .ReturnsAsync(true);
            var result = await _productsController.ValidarBcCostoCero(folio, domiCodigo);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            Assert.IsType<bool>(okResult.Value);
            Assert.True((bool)okResult.Value);
        }
        
        [Fact]
        public async Task ValidarBcCostoCero_ReturnsOkFalse_WhenServiceReturnsFalse()
        {
            const int folio = 54321;
            const int domiCodigo = 987;
            _productoServiceMock.Setup(service => service.ValidaBcCostoCero(folio, domiCodigo))
                .ReturnsAsync(false);
            
            var result = await _productsController.ValidarBcCostoCero(folio, domiCodigo);
            
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
            Assert.IsType<bool>(okResult.Value);
            Assert.False((bool)okResult.Value);
        }
        
        [Fact]
        public async Task ValidarBcCostoCero_ReturnsBadRequest_WhenServiceThrowsArgumentException()
        {
            const int folio = 111;
            const int domiCodigo = 222;
            var argumentException = new ArgumentException("Invalid arguments provided");
            _productoServiceMock.Setup(service => service.ValidaBcCostoCero(folio, domiCodigo))
                .ThrowsAsync(argumentException);
            
            var result = await _productsController.ValidarBcCostoCero(folio, domiCodigo);
            
            Assert.IsType<BadRequestResult>(result); 
            _productoServiceMock.Verify(service => service.ValidaBcCostoCero(folio, domiCodigo), Times.Once);
        }
        
        [Fact]
        public async Task ValidarBcCostoCero_ReturnsProblem_WhenServiceThrowsInvalidOperationException()
        {
            const int folio = 333;
            const int domiCodigo = 444;
            var invalidOperationException = new InvalidOperationException("Operation cannot be completed");
            _productoServiceMock.Setup(service => service.ValidaBcCostoCero(folio, domiCodigo))
                .ThrowsAsync(invalidOperationException);
            
            var result = await _productsController.ValidarBcCostoCero(folio, domiCodigo);
            
            var objectResult = Assert.IsAssignableFrom<ObjectResult>(result);
            Assert.Equal(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
            _productoServiceMock.Verify(service => service.ValidaBcCostoCero(folio, domiCodigo), Times.Once);
        }

    }
}
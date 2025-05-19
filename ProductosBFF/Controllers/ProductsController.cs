using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProductosBFF.Domain.Parameters;
using ProductosBFF.Exceptions;
using ProductosBFF.Filters;
using ProductosBFF.Interfaces;
using ProductosBFF.Models.Accidentes;
using ProductosBFF.Models.BCCesantia;
using ProductosBFF.Models.Commons;
using ProductosBFF.Models.Productos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductosBFF.Models.Causales;

namespace ProductosBFF.Controllers
{
    /// <summary>
    /// Controlador
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [UserAuthorizationFilter]
    public class ProductsController : ControllerBase
    {
        private readonly IProductoService _productoService;
        
        private readonly ILogger<ProductsController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductsController(IProductoService productoService, ILogger<ProductsController> logger)
        {
            _productoService = productoService;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene los productos de un usuario según su RUT
        /// </summary>
        /// <param name="rut">RUT identificador</param>
        /// <returns>
        /// Una instancia de <see cref="ProductDto"/>
        /// </returns>
        /// <response code="200">Se encontraron los productos</response>
        /// <response code="401">Usuario no autenticado</response>
        /// <response code="403">Recurso prohibido para el usuario autenticado</response>
        /// <response code="404">No se encontraron los productos</response>
        [HttpGet("Product/{rut}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<List<ProductDto>>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> GetProducts(long rut)
        {
            try
            {
                var productos = await _productoService.GetProductos(new BodyProducto() { PIN_RUT = rut });

                if (productos == null)
                {
                    var notFoundObjectResult =
                        new GenericResult<List<ProductDto>>(null, 404, "No se encontraron productos");
                    return new NotFoundObjectResult(notFoundObjectResult);
                }

                var okObjectResult =
                    new GenericResult<List<ProductDto>>(productos, 200, "Se obtuvieron los productos correctamente");
                return new OkObjectResult(okObjectResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }

        /// <summary>
        /// Obtiene los productos de ley corta
        /// </summary>
        /// <param name="rut">RUT identificador</param>
        /// <returns>
        /// Una instancia de <see cref="ProductDto"/>
        /// </returns>
        /// <response code="200">Se encontraron los productos</response>
        /// <response code="401">Usuario no autenticado</response>
        /// <response code="403">Recurso prohibido para el usuario autenticado</response>
        /// <response code="404">No se encontraron los productos</response>
        [HttpGet("ProductosLeyCorta/{rut}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<List<ProductLeyCortaDto>>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> GetProductosLeyCorta(decimal rut)
        {
            try
            {
                var productos = await _productoService.GetProductosLeyCorta(rut);

                if (productos == null)
                {
                    var notFoundObjectResult =
                        new GenericResult<List<ProductLeyCortaDto>>(null, 404, "No se encontraron productos");
                    return new NotFoundObjectResult(notFoundObjectResult);
                }

                var okObjectResult =
                    new GenericResult<List<ProductLeyCortaDto>>(productos, 200, "Se obtuvieron los productos correctamente");
                return new OkObjectResult(okObjectResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }

        /// <summary> Descarga BC </summary>
        /// <param name="CodeBC"></param>
        /// <returns></returns>
        [HttpGet("DownloadBC")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<DocumentoBCDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DownloadDocumentBC(string CodeBC)
        {
            try
            {
                var downLoadBCBase64 = await _productoService.GetDocumentsBase64(new ProductoBC() { BCCode = CodeBC });

                if (downLoadBCBase64 == null || downLoadBCBase64.Base64 == "Ha ocurrido un error al obtener el PDF")
                {
                    var notFoundObjectResult = new GenericResult<DocumentoBCDto>(null, 404,
                        "No se encontró el archivo solicitado para descarga");
                    return new NotFoundObjectResult(notFoundObjectResult);
                }

                var okObjectResult = new GenericResult<DocumentoBCDto>(downLoadBCBase64, 200, "Archivo BC en base 64");
                return new OkObjectResult(okObjectResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }


        /// <summary>
        /// Endpoint que trae las causales despidos
        /// </summary>
        /// <returns></returns>
        ///
        [HttpGet("GetCausales")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> GetCausalesDespidos()
        {
            try
            {
                var causales = await _productoService.GetCausales();
                return new OkObjectResult(causales);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return NotFound();
            }
        }

        /// <summary>
        /// Trae Motivos Cancelacion
        /// </summary>
        /// <returns></returns>
		[HttpGet("GetMotivosCancelacion")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MotivosCancelacionDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
		[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
		[ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
		public async Task<ActionResult> GetMotivosCancelacion()
        {
            try
            {
                var motivos = await _productoService.GetMotivos();
                if (motivos != null) return new OkObjectResult(motivos);

                var notFoundObjectResult =
                    new GenericResult<List<MotivosCancelacionDto>>(null, 500,
                        "Error al obtener el motivos cancelacion");
                return new NotFoundObjectResult(notFoundObjectResult);
            }
            catch (ArgumentException ar)
            {
                _logger.LogError(ar, ar.Message);
                return BadRequest();
            }
            catch (InvalidOperationException ioe)
            {
                _logger.LogError(ioe, ioe.Message);
                return Problem();
            }
			catch (Exception e)
			{
				_logger.LogError(e, e.Message);
				return NotFound();
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="solicitud"></param>
		/// <returns></returns>
		[HttpPost("EnviarDatosCesantia")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseBcAfilSini))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> EnviarDatosCesantia(BodySolicitudActivacion solicitud)
        {
            try
            {
                ResponseBcAfilSini response = await _productoService.EnviarDatosCesantia(solicitud);

                return new OkObjectResult(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return NotFound();
            }
        }

        /// <summary>
        /// Obtiene la continuidad del producto
        /// </summary>
        /// <param name="rut">RUT identificador</param>
        /// <response code="200">Se encontraron los productos</response>
        /// <response code="401">Usuario no autenticado</response>
        /// <response code="403">Recurso prohibido para el usuario autenticado</response>
        /// <response code="404">No se encontraron los productos</response>
        [HttpGet("ContinuidadProducto")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> Get(int rut)
        {
            try
            {
                var result = _productoService.GetContinuidadProductoService(new ContinuidadProducto { rut = rut });

                var okObjectResult = new GenericResult<bool>(await result, 200, await result ? "True" : "False");

                return new OkObjectResult(okObjectResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }

        /// <summary>
        /// Registra una nueva solicitud de continuidad de BC de cesantía
        /// </summary>
        /// <param name="solicitud"></param>
        /// <returns></returns>
        [HttpPost("RegistraSolicitudContinuidadCesantia")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = null)]
        public async Task<ActionResult> RegistraSolicitudContinuidadCesantia(BodySolicitudActivacion solicitud)
        {
            try
            {
                int response = await _productoService.RegistraSolicitudContinuidadCesantia(solicitud);

                return Ok(response);
            }
            catch (ContentManagerException e)
            {
                _logger.LogError(e, e.Message);
                return Conflict(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return NotFound(e.Message);
            }
        }

        /// <summary>
        /// Obtiene el estado del usuario si es multicotizante
        /// </summary>
        /// <param name="envioCorreo"></param>
        /// <returns></returns>
        /// <response code="200">Se enviaron los correos</response>
        /// <response code="401">Usuario no autenticado</response>
        /// <response code="403">Recurso prohibido para el usuario autenticado</response>
        /// <response code="404">No se encontraron los productos</response>
        [HttpPost("EnvioCorreo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseEnvioCorreo))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> PostEnvioCorreo(EnvioCorreo envioCorreo)
        {
            try
            {
                var enviocorreo = await _productoService.EnviarCorreo(envioCorreo);

                var okObjectResult = new GenericResult<ResponseEnvioCorreo>(enviocorreo, 200, "OK");

                return new OkObjectResult(okObjectResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }

        /// <summary>
        /// Genera el documento fun4
        /// </summary>
        /// <param name="datosFun4"></param>
        /// <returns></returns>
        /// <response code="200">Se enviaron los correos</response>
        /// <response code="401">Usuario no autenticado</response>
        /// <response code="403">Recurso prohibido para el usuario autenticado</response>
        /// <response code="404">No se logro generar el fun4 </response>
        [HttpPost("GeneraFun4")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Fun4))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> PostGenerarFun4(DtoFun4 datosFun4)
        {
            try
            {
                var generarfun4 = await _productoService.EnviarFun4(datosFun4);

                if (generarfun4.Codigo != 0)
                {
                    return new NotFoundObjectResult(generarfun4.Codigo);
                }

                return new OkObjectResult(generarfun4.Codigo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="idSiniestro"></param>
        /// <returns></returns>
        [HttpGet("ProcesoSolicitud/{idSiniestro:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DtoResponseTrazaPadre))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> ProcesoSolicitud(int idSiniestro)
        {
            try
            {
                var result = await _productoService.SolicitudesTrazaPadre(new TrazaPadreParam
                    { pin_id_siniestro = idSiniestro });

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }


        /// <summary>
        /// Metodo que trae el historial de solicitudes del afiliado
        /// </summary>
        /// <param name="idSiniestro"></param>
        /// <returns></returns>
        [HttpGet("DetalleSolicitudVC/{idSiniestro:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DtoDetalleSolicitudVC>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> DetalleSolicitudVC(int idSiniestro)
        {
            try
            {
                var result = await _productoService.DetalleSolicitudVC(new BodyHistorialCabecera
                    { pin_id_siniestro = idSiniestro });

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }

        /// <summary>
        /// Metodo que trae el historial de solicitudes del afiliado
        /// </summary>
        /// <param name="idSiniestro"></param>
        /// <returns></returns>
        [HttpGet("DetalleSolicitudCesantia/{idSiniestro:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DtoDetalleSolicitudCesantia>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> DetalleSolicitudCesantia(int idSiniestro)
        {
            try
            {
                var result = await _productoService.DetalleSolicitudCesantia(new BodyHistorialCabecera
                    { pin_id_siniestro = idSiniestro });

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }

        /// <summary>
        /// Metodo que trae el historial de solicitudes del afiliado
        /// </summary>
        /// <param name="folio"></param>
        /// <returns></returns>
        [HttpGet("HistorialSolicitudes/{folio:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DtoHistorialSolicitudes>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> HistorialSolicitudes(int folio)
        {
            try
            {
                var result =
                    await _productoService.HistorialSolicitudes(new BodyHistorialSolicitudes { pin_folio = folio });

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }

        // <summary>
        /// Consulta tipo accidente
        /// <returns>
        /// Una colección de <see cref="TipoAccidenteDto"/>
        /// </returns>
        /// <response code="200">Se consultó tipo accidente</response>
        /// <response code="401">Usuario no autenticado</response>
        /// <response code="403">Recurso prohibido para el usuario autenticado</response>
        /// <response code="404">No se consultó ningún tipo de accidente</response>
        [HttpGet("TipoAccidente")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TipoAccidenteDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> ConsultaTipoAccidente()
        {
            try
            {

                var consultaTipoAccidente = await _productoService.ObtenerTipoAccidente();

                if (consultaTipoAccidente == null)
                {
                    var notFoundObjectResult =
                        new GenericResult<List<TipoAccidenteDto>>(null, 404, "No se encontraron tipos de accidente");
                    return new NotFoundObjectResult(notFoundObjectResult);
                }


                var okObjectResult =
                    new GenericResult<List<TipoAccidenteDto>>(consultaTipoAccidente, 200, "Consulta tipo accidente ok");
                return new OkObjectResult(okObjectResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }

        // <summary>
        /// Consulta causa accidente
        /// <param name="tipoAccidente"></param>
        /// <returns>
        /// Una colección de <see cref="CausalAccidenteDto"/>
        /// </returns>
        /// <response code="200">Se consultó causa accidente</response>
        /// <response code="401">Usuario no autenticado</response>
        /// <response code="403">Recurso prohibido para el usuario autenticado</response>
        /// <response code="404">No se consultó ninguna causa de accidente</response>
        [HttpGet("CausaAccidente/{tipoAccidente:decimal}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CausalAccidenteDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> ConsultaCausaAccidente(decimal tipoAccidente)
        {
            try
            {
                ParamTipoAccidente pTipoAccidente = new ParamTipoAccidente
                {
                    IdTipoAccidente = tipoAccidente
                };
                var consultaCausalAccidente = await _productoService.ObtenerCausalAccidente(pTipoAccidente);

                if (consultaCausalAccidente == null)
                {
                    var notFoundObjectResult =
                        new GenericResult<List<CausalAccidenteDto>>(null, 404,
                            "No se encontraron causales de accidente");
                    return new NotFoundObjectResult(notFoundObjectResult);
                }


                var okObjectResult = new GenericResult<List<CausalAccidenteDto>>(consultaCausalAccidente, 200,
                    "Consulta causal accidente ok");
                return new OkObjectResult(okObjectResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }

        // <summary>
        /// Consulta documentos accidente
        /// <param name="documentosAccidente"></param>
        /// <returns>
        /// Una colección de <see cref="DocumentosAccidenteDto"/>
        /// </returns>
        /// <response code="200">Se consultó documentos accidente</response>
        /// <response code="401">Usuario no autenticado</response>
        /// <response code="403">Recurso prohibido para el usuario autenticado</response>
        /// <response code="404">No se consultó ninguna causa de accidente</response>
        [HttpPost("DocumentosAccidente")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DocumentosAccidenteDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> DocumentosAccidente([FromBody] BodyDocumentosAccidente documentosAccidente)
        {
            try
            {
                var consultaDocumentosAccidente =
                    await _productoService.ObtenerDocumentosAccidente(documentosAccidente);

                if (consultaDocumentosAccidente == null)
                {
                    var notFoundObjectResult =
                        new GenericResult<List<DocumentosAccidenteDto>>(null, 404,
                            "No se encontraron documentos de accidente");
                    return new NotFoundObjectResult(notFoundObjectResult);
                }


                var okObjectResult = new GenericResult<List<DocumentosAccidenteDto>>(consultaDocumentosAccidente, 200,
                    "Consulta documentos accidente ok");
                return new OkObjectResult(okObjectResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }

        // <summary>
        /// Ingresar archivo accidente
        /// <param name="ingresarArchivoAccidente"></param>
        /// <returns>
        /// Una colección de <see cref="DocumentosAccidenteDto"/>
        /// </returns>
        /// <response code="200">Se consultó documentos accidente</response>
        /// <response code="401">Usuario no autenticado</response>
        /// <response code="403">Recurso prohibido para el usuario autenticado</response>
        /// <response code="404">No se consultó ninguna causa de accidente</response>
        [HttpPost("IngresarArchivoAccidente")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IngresarArchivoAccidenteDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> IngresarArchivoAccidente(
            [FromBody] BodyIngresarArchivoAccidente ingresarArchivoAccidente)
        {
            try
            {
                var ingArchivoAccidente = await _productoService.IngresarArchivoAccidente(ingresarArchivoAccidente);

                if (ingArchivoAccidente == null)
                {
                    var notFoundObjectResult =
                        new GenericResult<IngresarArchivoAccidenteDto>(null, 404,
                            "No pudo ingresar el archivo accidente");
                    return new NotFoundObjectResult(notFoundObjectResult);
                }


                var okObjectResult = new GenericResult<IngresarArchivoAccidenteDto>(ingArchivoAccidente, 200,
                    "Se ingreso el archivo accidente correctamente");
                return new OkObjectResult(okObjectResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }

        // <summary>
        /// Ingresar accidente
        /// <param name="ingresarAccidente"></param>
        /// <returns>
        /// Una colección de <see cref="DocumentosAccidenteDto"/>
        /// </returns>
        /// <response code="200">Se consultó documentos accidente</response>
        /// <response code="401">Usuario no autenticado</response>
        /// <response code="403">Recurso prohibido para el usuario autenticado</response>
        /// <response code="404">No se consultó ninguna causa de accidente</response>
        [HttpPost("IngresarAccidente")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IngresarAccidenteDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> IngresarAccidente([FromBody] BodyIngresarAccidente ingresarAccidente)
        {
            try
            {
                var ingAccidente = await _productoService.IngresarAccidente(ingresarAccidente);

                if (ingAccidente == null)
                {
                    var notFoundObjectResult =
                        new GenericResult<IngresarAccidenteDto>(null, 404, "No pudo ingresar el accidente");
                    return new NotFoundObjectResult(notFoundObjectResult);
                }


                var okObjectResult =
                    new GenericResult<IngresarAccidenteDto>(ingAccidente, 200, "Se ingreso el accidente correctamente");
                return new OkObjectResult(okObjectResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }

        // <summary>
        /// Ingresar accidente
        /// <param name="cargarArchivoShira"></param>
        /// <returns>
        /// Una colección de <see cref="CargarArchivoShiraDto"/>
        /// </returns>
        /// <response code="200">Se consultó documentos accidente</response>
        /// <response code="401">Usuario no autenticado</response>
        /// <response code="403">Recurso prohibido para el usuario autenticado</response>
        /// <response code="404">No se consultó ninguna causa de accidente</response>
        [HttpPost("CargaArchivosShira")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CargarArchivoShiraDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> CargaArchivosShira([FromBody] BodyCargarArchivoShira cargarArchivoShira)
        {
            try
            {
                var cargaArchivo = await _productoService.CargaArchivosShira(cargarArchivoShira);

                if (cargaArchivo == null)
                {
                    var notFoundObjectResult =
                        new GenericResult<CargarArchivoShiraDto>(null, 404,
                            "No se cargó el archivo correctamente a shira");
                    return new NotFoundObjectResult(notFoundObjectResult);
                }


                var okObjectResult = new GenericResult<CargarArchivoShiraDto>(cargaArchivo, 200,
                    "se cargó el archivo correctamente a shira");
                return new OkObjectResult(okObjectResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }

        // <summary>
        /// Ingresar accidente
        /// <param name="cargarArchivoContent"></param>
        /// <returns>
        /// Una colección de <see cref="CargarArchivoShiraDto"/>
        /// </returns>
        /// <response code="200">Se consultó documentos accidente</response>
        /// <response code="401">Usuario no autenticado</response>
        /// <response code="403">Recurso prohibido para el usuario autenticado</response>
        /// <response code="404">No se consultó ninguna causa de accidente</response>
        [HttpPost("CargaArchivosContent")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CargarArchivoContentDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = null)]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = null)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = null)]
        public async Task<ActionResult> CargaArchivosContent([FromBody] BodyCargarArchivoContent cargarArchivoContent)
        {
            try
            {
                var cargaArchivo = await _productoService.CargaArchivosContent(cargarArchivoContent);

                if (cargaArchivo == null)
                {
                    var notFoundObjectResult = new GenericResult<CargarArchivoContentDto>(null, 404,
                        "No se cargó el archivo correctamente a content");
                    return new NotFoundObjectResult(notFoundObjectResult);
                }


                var okObjectResult = new GenericResult<CargarArchivoContentDto>(cargaArchivo, 200,
                    "se cargó el archivo correctamente content");
                return new OkObjectResult(okObjectResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return NotFound();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obtenerDiagnostico"></param>
        /// <returns></returns>
        [HttpPost("ObtenerDiagnosticoVc")]
        [ProducesResponseType(typeof(ResponseObtenerDiagnostico), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ObtenerDiagnosticoVc(BodyObtenerDiagnostico obtenerDiagnostico)
        {
            try
            {
                var result = await _productoService.ObtenerDiagnosticoVc(obtenerDiagnostico);
                return new OkObjectResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return NotFound();
            }
        }
        /// <summary>
        /// Endpoint para cancelar Bc Costo Cero
        /// </summary>
        /// <param name="bodyCancelarBcParam"></param>
        /// <returns></returns>
        [HttpPost("CancelaBcCostoCero")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GenericResult<CancelaBcCostoCeroResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [TypeFilter(typeof(ValidSecondCodeAuthorizationFilter))]
        public async Task<ActionResult> CancelaBcCostoCero(BodyCancelarBcParam bodyCancelarBcParam)
        {
            try
            {
                var auditoria = HttpContext.Items["IdAuditoria"].ToString();
                
                var bodyCancelarConFirma = new BodyCancelarParamDto
                {
                    pin_codigo_bc = bodyCancelarBcParam.pin_codigo_bc,
                    pin_codigo_motivo = bodyCancelarBcParam.pin_codigo_motivo,
                    pin_folio = bodyCancelarBcParam.pin_folio,
                    pin_firma =  auditoria
                };
                var result = await _productoService.CancelaBcCostoCero(bodyCancelarConFirma);
                
                if (result == null)
                {
                    var notFoundObjectResult =
                        new GenericResult<CancelaBcCostoCeroResponse>(null, 500, "No se pudo cancelar el bc");
                    return new NotFoundObjectResult(notFoundObjectResult);
                }
                
                var okObjectResult =
                    new GenericResult<CancelaBcCostoCeroResponse>(result, 200, "Se pudo cancelar el bc costo cero");
                return new OkObjectResult(okObjectResult);
            }
            catch (ArgumentException ar)
            {
                _logger.LogError(ar, ar.Message);
                return BadRequest();
            }
            catch (InvalidOperationException ioe)
            {
                _logger.LogError(ioe, ioe.Message);
                return Problem();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return NotFound();
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="domiCodigo"></param>
        /// <returns></returns>
        [HttpGet("ValidarBcCostoCero/{folio:int}/{domiCodigo:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ValidarBcCostoCero(int folio, int domiCodigo)
        {
            try
            {
                var result = await _productoService.ValidaBcCostoCero(folio,domiCodigo);
                
                return new OkObjectResult(result);
            }
            catch (ArgumentException ar)
            {
                _logger.LogError(ar, ar.Message);
                return BadRequest();
            }
            catch (InvalidOperationException ioe)
            {
                _logger.LogError(ioe, ioe.Message);
                return Problem();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return NotFound();
            }
        }
    }
}
using System;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using ProductosBFF.Domain.Accidentes;
using ProductosBFF.Domain.Causales;
using ProductosBFF.Domain.Parameters;
using ProductosBFF.Domain.Productos;
using ProductosBFF.Exceptions;
using ProductosBFF.Interfaces;
using ProductosBFF.Models.Accidentes;
using ProductosBFF.Models.BCCesantia;
using ProductosBFF.Models.Causales;
using ProductosBFF.Models.Productos;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ProductosBFF.Utils;

namespace ProductosBFF.Services
{
    /// <summary>
    /// Clase
    /// </summary>
    public class ProductoService : IProductoService
    {
        private readonly IProductoInfrastructure _productoInfrastructure;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ProductoService> _logger;
        private const string MensajeError = "Sin Datos";

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductoService(IProductoInfrastructure productoInfrastructure, IMapper mapper,
            IConfiguration configuration, ILogger<ProductoService> logger)
        {
            _productoInfrastructure = productoInfrastructure;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// GetProductos
        /// </summary>
        /// <param name="rut"></param>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public async Task<List<ProductDto>> GetProductos(BodyProducto rut)
        {
            var actual = DateTime.Now.AddHours(-4).TimeOfDay;
            var inicio = new TimeSpan(0, 0, 0);
            var termino = new TimeSpan(0, 45, 0);
            var dtoList = new List<ProductDto>();

            try
            {
                var result = await _productoInfrastructure.GetProductsAsync(rut);
                if (result != null && result.Any())
                {
                    var mappedProducts = _mapper.Map<List<Producto>, List<ProductDto>>(result);
                    dtoList.AddRange(mappedProducts);
                }
                else
                {
                    _logger.LogInformation("No se encontraron productos para RUT {Rut}", rut.PIN_RUT);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos para RUT {Rut}", rut.PIN_RUT);
            }

            try
            {
                var costoCeroList = await _productoInfrastructure.GetProductosCostoCero(rut.PIN_RUT);
                if (costoCeroList != null && costoCeroList.Any())
                {
                    var costoCerodto = _mapper.Map<List<ProductosCostoCero>, List<ProductDto>>(costoCeroList);
                    dtoList.AddRange(costoCerodto);
                    _logger.LogInformation("Se obtuvieron {Count} productos costo cero para RUT {Rut}",
                        costoCeroList.Count, rut.PIN_RUT);
                }
                else
                {
                    _logger.LogInformation("No se encontraron productos costo cero para RUT {Rut}", rut.PIN_RUT);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener productos costo cero para RUT {Rut}", rut.PIN_RUT);
            }

            foreach (var dto in dtoList)
            {
                dto.FlagMantencion = actual >= inicio && actual < termino;
            }

            return dtoList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="domiCodigo"></param>
        /// <returns></returns>
        public async Task<bool> ValidaBcCostoCero(int folio, int domiCodigo)
        {
            try
            {
                return await _productoInfrastructure.ValidaBcCostoCero(folio, domiCodigo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// GetProductosLeyCorta
        /// </summary>
        /// <param name="rut"></param>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public async Task<List<ProductLeyCortaDto>> GetProductosLeyCorta(decimal rut)
        {
            var result = await _productoInfrastructure.GetProductosLeyCorta(rut) ??
                         throw new InvalidOperationException(MensajeError);

            return _mapper.Map<List<ProductoLeyCorta>, List<ProductLeyCortaDto>>(result);
        }

        /// <summary>
        /// Servicio que cancela bc costo cero
        /// </summary>
        /// <param name="bodyCancelarBcParam"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<CancelaBcCostoCeroResponse> CancelaBcCostoCero(BodyCancelarParamDto bodyCancelarBcParam)
        {
            try
            {
                var response = await _productoInfrastructure.ObtenerAuditoria(int.Parse(bodyCancelarBcParam.pin_firma));

                bodyCancelarBcParam.pi_dFecha_Autor = DateTime.Now;
                bodyCancelarBcParam.pi_vIp_Cliente = response.Ip;

                var idLog = await _productoInfrastructure.CancelarBcCostoCero(bodyCancelarBcParam);

                var obtenerComprobanteCostoCero = await _productoInfrastructure.ObtenerComprobante(
                    new ObtenerComprobanteParam
                    {
                        Folio = bodyCancelarBcParam.pin_folio.ToString(),
                        Canal = "SUDI",
                        Codigo = bodyCancelarBcParam.pin_codigo_bc,
                        LogTermino = idLog.ToString()
                    });

                return new CancelaBcCostoCeroResponse
                {
                    nroSolicitud = idLog,
                    documentoBase64 = obtenerComprobanteCostoCero.documentoBase64,
                    disponibleHasta = obtenerComprobanteCostoCero.fechaTerminoBeneficio
                };
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error al ingresar datos: " + e.Message);
            }
        }


        /// <summary> Get Base 64 BC </summary>
        /// <param name="codeBc"></param>
        /// <returns></returns>
        public async Task<DocumentoBCDto> GetDocumentsBase64(ProductoBC codeBc)
        {
            var result = await _productoInfrastructure.GetDocumentsBCBase64(codeBc);

            return result == null
                ? throw new InvalidOperationException(MensajeError)
                : new DocumentoBCDto()
                {
                    Base64 = result.DOCUMENTO,
                    Cabecera = "data:application/pdf;base64,",
                    Titulo = $"Beneficio Complementario N° {codeBc.BCCode}"
                };
        }


        /// <summary>
        /// Datos Cesantia
        /// </summary>
        /// <param name="solicitud"></param>
        /// <returns></returns>
        public async Task<ResponseBcAfilSini> EnviarDatosCesantia(BodySolicitudActivacion solicitud)
        {
            try
            {
                string idContentManager;
                ResponseValidaFun4 funEncontrado =
                    await _productoInfrastructure.ValidaFun4(new ObtieneFun4
                        { Folio = solicitud.FolioAfil.ToString() });

                var dtoAfil = _mapper.Map<DtoAfil>(solicitud);

                if (funEncontrado != null)
                {
                    //al ser "N" el caso solo se podra revisar por sucursal.
                    if (funEncontrado.SolicitudValida == "N")
                    {
                        throw new InvalidOperationException("Error al ingresar datos: La solicitud no es válida.");
                    }

                    //Si viene el ID content del fun es un FUN MANUAL y se le asigna un 0..
                    idContentManager = string.IsNullOrWhiteSpace(funEncontrado.ID_Content_Manager)
                        ? "0"
                        : funEncontrado.ID_Content_Manager;
                    dtoAfil.PinCodFun4 = funEncontrado.Folio;
                }
                else
                {
                    //Crea un fun 4 nuevo para el afiliado...
                    try
                    {
                        Fun4 funCreado = await _productoInfrastructure.EnviarFun4(_mapper.Map<DtoFun4>(solicitud));

                        idContentManager = funCreado.Content;
                        dtoAfil.PinCodFun4 = funCreado.Fun;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex,
                            "Error al generar el FUN 4 Clase: ProductoService Metodo: EnviarDatosCesantia Error: {Message}.",
                            ex.Message);
                        //Si falla al generar el FUN 4 por cualquier motivo, no se creara la solicitud - TP-293931
                        throw new InvalidOperationException(
                            "Error al generar el FUN 4 - Error al generar la solicitud" + ex.Message);
                    }
                }

                var resultAfil = await _productoInfrastructure.EnviarDatosAfilBCCesantia(dtoAfil);

                try
                {
                    solicitud.Imagen = solicitud.Imagen.Replace("Servicios Content Manager Rest", "");
                    var index = solicitud.FileBase64.IndexOf("ExtraDataToIncreaseSize", StringComparison.Ordinal);

                    if (index != -1)
                    {
                        solicitud.FileBase64 = solicitud.FileBase64.Replace("ExtraDataToIncreaseSize", "");
                    }

                    _ = await _productoInfrastructure.CargaArchivosShira(new BodyCargarArchivoShira
                    {
                        PIN_Rut = solicitud.RutAfil,
                        PIV_Archivo = solicitud.FileBase64,
                        PIV_NombreArchivo = resultAfil.IdSiniestro + solicitud.Imagen,
                        PIV_RutaArchivo = _configuration.GetValue<string>($"PRODUCTO:RUTA_SHIRA_CESANTIA")
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "Error al generar el FUN 4 Clase: ProductoService, Metodo: EnviarDatosCesantia, Error: {Message}",
                        ex.Message);
                }

                //Guarda finiquito en Content manager
                DocCesantia docCm =
                    await _productoInfrastructure.EnviarDatosDocBCCesantia(_mapper.Map<DtoDocCm>(solicitud));

                //Guarda finiquito en la tabla de archivos
                _ = await EnvioArchivoSini(resultAfil.IdSiniestro, docCm.Result.Idcontent, solicitud.Imagen,
                    "Finiquito",
                    5);

                //Guarda el FUN 4 en la tabla de archivos
                _ = await EnvioArchivoSini(resultAfil.IdSiniestro, idContentManager, $"FUN_{dtoAfil.PinCodFun4}.pdf",
                    "Fun 4", 6);

                return resultAfil;
            }
            catch (Exception e)
            {
                _logger.LogError("Error interno" + "Clase: " + "ProductoService " + "Metodo: " +
                                 "EnviarDatosCesantia " + "Error: {Mensaje}", e.Message);
                throw new InvalidOperationException("Error al ingresar datos " + e.Message);
            }
        }

        /// <summary>
        /// /
        /// </summary>
        /// <param name="bodyHistorialCabecera"></param>
        /// <returns></returns>
        public async Task<List<DtoDetalleSolicitudVC>> DetalleSolicitudVC(
            BodyHistorialCabecera bodyHistorialCabecera)
        {
            var result = await _productoInfrastructure.DetalleSolicitudVC(bodyHistorialCabecera);
            List<DtoDetalleSolicitudVC> detalleDto = _mapper.Map<List<DtoDetalleSolicitudVC>>(result);

            foreach (var dto in detalleDto)
            {
                dto.beneficiario = StringMethods.Capitalize(dto.beneficiario);
            }

            return detalleDto;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bodyHistorialCabecera"></param>
        /// <returns></returns>
        public async Task<List<DtoDetalleSolicitudCesantia>> DetalleSolicitudCesantia(
            BodyHistorialCabecera bodyHistorialCabecera)
        {
            var result = await _productoInfrastructure.DetalleSolicitudCesantia(bodyHistorialCabecera);

            var detalleDto = _mapper.Map<List<DtoDetalleSolicitudCesantia>>(result);

            foreach (var dto in detalleDto)
            {
                dto.beneficiario = StringMethods.Capitalize(dto.beneficiario);
            }

            return detalleDto;
        }

        /// <summary>
        /// Metodo que trae el historial de solicitudes del afiliado
        /// </summary>
        /// <param name="bodyHistorialSolicitudes"></param>
        /// <returns></returns>
        public async Task<List<DtoHistorialSolicitudes>> HistorialSolicitudes(
            BodyHistorialSolicitudes bodyHistorialSolicitudes)
        {
            List<ResponseHistorialSolicitudes> historialList =
                await _productoInfrastructure.HistorialSolicitudes(bodyHistorialSolicitudes);
            var historialSolicitudesList = _mapper.Map<List<DtoHistorialSolicitudes>>(historialList);
            return historialSolicitudesList;
        }


        /// <summary>
        /// Trae causales de despido
        /// </summary>
        /// <returns></returns>
        public async Task<List<CausalesDto>> GetCausales()
        {
            List<Causales> causales = await _productoInfrastructure.GetCausalesAsync();
            return _mapper.Map<List<Causales>, List<CausalesDto>>(causales);
        }

        /// <summary>
        /// Trae Motivos Cancelacion
        /// </summary>
        /// <returns></returns>
        public async Task<List<MotivosCancelacionDto>> GetMotivos()
        {
            List<MotivosCancelacion> motivos = await _productoInfrastructure.GetMotivosCancelacion();
            return _mapper.Map<List<MotivosCancelacion>, List<MotivosCancelacionDto>>(motivos);
        }

        /// <summary>
        /// Metodo para enviar archivo sini
        /// </summary>
        /// <param name="idSiniestro"></param>
        /// <param name="idContent"></param>
        /// <param name="detalleContent"></param>
        /// <param name="tipoDoc"></param>
        /// <param name="idParametro"></param>
        private async Task<ResponseEnviarArchivo> EnvioArchivoSini(string idSiniestro, string idContent,
            string detalleContent, string tipoDoc,
            int idParametro)
        {
            return await _productoInfrastructure.EnviarArchivosSini(new BodyBcArchivo
            {
                PinIdSiniestro = idSiniestro,
                PivTipoDoc = tipoDoc,
                PinIdContent = idContent,
                PivDetalleContent = detalleContent,
                PinIdParametro = idParametro
            });
        }

        /// <summary>
        /// GetContinuidadProductoService
        /// </summary>
        /// <param name="rut"></param>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public async Task<bool> GetContinuidadProductoService(ContinuidadProducto rut)
        {
            return await _productoInfrastructure.ContinuidadProducto(rut);
        }

        /// <summary>
        /// Registra una nueva solicitud de continuidad de BC de cesantía
        /// </summary>
        /// <param name="solicitud"></param>
        /// <returns>El Id del siniestro en caso de haber sido ejecutado correctamente</returns>
        public async Task<int> RegistraSolicitudContinuidadCesantia(BodySolicitudActivacion solicitud)
        {
            DocCesantia cargaContentResult;
            try
            {
                cargaContentResult =
                    await _productoInfrastructure.EnviarDatosDocBCCesantia(_mapper.Map<DtoDocCm>(solicitud));
            }
            catch (Exception e)
            {
                throw new ContentManagerException("Error al cargar archivo: " + e.Message);
            }

            try
            {
                var idSiniestro = await _productoInfrastructure.RegistraContinuidadCesantia(solicitud.RutAfil);
                if (idSiniestro == 0) throw new ArgumentException("No se pudo registrar el siniestro");

                await EnvioArchivoSini(idSiniestro.ToString(), cargaContentResult.Result.Idcontent, solicitud.Imagen,
                    "Certificado de cotizaciones", 7);
                return idSiniestro;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Error al ingresar datos: " + e.Message);
            }
        }

        /// <summary>
        /// Datos Cesantia
        /// </summary>
        /// <param name="envioCorreo"></param>
        /// <returns></returns>
        public async Task<ResponseEnvioCorreo> EnviarCorreo(EnvioCorreo envioCorreo)
        {
            try
            {
                //Flujo para enviar datos para el correo de rechazo
                var enviocorreo = await _productoInfrastructure.EnviarCorreo(_mapper.Map<DtoEnvioCorreo>(envioCorreo));

                return enviocorreo;
            }

            catch (Exception e)
            {
                throw new InvalidOperationException("Error al ingresar datos " + e.Message);
            }
        }

        /// <summary>
        /// Envia Datos para api Afil para id content fun4
        /// </summary>
        /// <param name="datosFun4"></param>
        /// <returns></returns>
        public async Task<Fun4> EnviarFun4(DtoFun4 datosFun4)
        {
            try
            {
                string hostName = Dns.GetHostName();
                IPAddress ip =
                    (await Dns.GetHostEntryAsync(hostName)).AddressList.FirstOrDefault(x =>
                        x.AddressFamily == AddressFamily.InterNetwork);
                datosFun4.Ip = ip!.ToString();
                //Flujo para enviar datos para el correo de rechazo
                var generafun4 =
                    await _productoInfrastructure.EnviarFun4(_mapper.Map<DtoFun4>(datosFun4));

                return generafun4;
            }

            catch (Exception e)
            {
                throw new InvalidOperationException("Error al ingresar datos " + e.Message);
            }
        }

        /// <summary>
        /// Obtener tipo accidente
        /// </summary>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public async Task<List<TipoAccidenteDto>> ObtenerTipoAccidente()
        {
            var result = await _productoInfrastructure.ObtenerTipoAccidente() ??
                         throw new InvalidOperationException(MensajeError);

            return _mapper.Map<List<TipoAccidente>, List<TipoAccidenteDto>>(result);
        }

        /// <summary>
        /// Obtener causal accidente
        ///  <param name="tipoAccidente"></param>
        /// </summary>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public async Task<List<CausalAccidenteDto>> ObtenerCausalAccidente(ParamTipoAccidente tipoAccidente)
        {
            var result = await _productoInfrastructure.ObtenerCausalAccidente(tipoAccidente) ??
                         throw new InvalidOperationException("Sin datos");

            return _mapper.Map<List<CausalAccidente>, List<CausalAccidenteDto>>(result);
        }

        /// <summary>
        /// Obtener documentos accidente
        ///  <param name="documentosAccidente"></param>
        /// </summary>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public async Task<List<DocumentosAccidenteDto>> ObtenerDocumentosAccidente(
            BodyDocumentosAccidente documentosAccidente)
        {
            var result = await _productoInfrastructure.ObtenerDocumentosAccidente(documentosAccidente) ??
                         throw new InvalidOperationException("Sin datos");

            var listaDoc = result
                .Select(s => !string.IsNullOrEmpty(s.LISTA) ? s.LISTA.Split(';').ToList() : new List<string>())
                .ToList();

            var objeto = _mapper.Map<List<DocumentosAccidente>, List<DocumentosAccidenteDto>>(result);

            return objeto.Zip(listaDoc, (accidenteDto, lista) =>
            {
                accidenteDto.Lista = lista.ToList();
                return accidenteDto;
            }).ToList();
        }

        /// <summary>
        /// Ingresar archivo accidente
        ///  <param name="ingresarArchivoAccidente"></param>
        /// </summary>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public async Task<IngresarArchivoAccidenteDto> IngresarArchivoAccidente(
            BodyIngresarArchivoAccidente ingresarArchivoAccidente)
        {
            ingresarArchivoAccidente.RutaArchivo = _configuration.GetValue<string>($"PRODUCTO:RUTA_SHIRA");
            var result = await _productoInfrastructure.IngresarArchivoAccidente(ingresarArchivoAccidente) ??
                         throw new InvalidOperationException("Sin datos");

            return _mapper.Map<IngresarArchivoAccidenteDto>(result);
        }

        /// <summary>
        /// Ingresar accidente
        ///  <param name="ingresarAccidente"></param>
        /// </summary>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public async Task<IngresarAccidenteDto> IngresarAccidente(BodyIngresarAccidente ingresarAccidente)
        {
            var result = await _productoInfrastructure.IngresarAccidente(ingresarAccidente) ??
                         throw new InvalidOperationException("Sin datos");

            return _mapper.Map<IngresarAccidenteDto>(result);
        }

        /// <summary>
        /// cargarArchivoShira
        ///  <param name="cargarArchivoShira"></param>
        /// </summary>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public async Task<CargarArchivoShiraDto> CargaArchivosShira(BodyCargarArchivoShira cargarArchivoShira)
        {
            cargarArchivoShira.PIV_RutaArchivo = _configuration.GetValue<string>("PRODUCTO:RUTA_SHIRA");
            var result = await _productoInfrastructure.CargaArchivosShira(cargarArchivoShira) ??
                         throw new InvalidOperationException("Sin datos");

            return _mapper.Map<CargarArchivoShiraDto>(result);
        }

        /// <summary>
        /// Carga archivo a content
        /// </summary>
        /// <param name="cargarArchivoContent"></param>
        /// <returns>El Id del content</returns>
        public async Task<CargarArchivoContentDto> CargaArchivosContent(BodyCargarArchivoContent cargarArchivoContent)
        {
            try
            {
                var cargaContentResult = await _productoInfrastructure.CargaArchivosContent(cargarArchivoContent);

                return _mapper.Map<CargarArchivoContentDto>(cargaContentResult);
            }
            catch (Exception e)
            {
                throw new ContentManagerException("Error al cargar archivo: " + e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bodyTrazaPadreParam"></param>
        /// <returns></returns>
        public async Task<DtoResponseTrazaPadre> SolicitudesTrazaPadre(TrazaPadreParam bodyTrazaPadreParam)
        {
            var result = await _productoInfrastructure.SolicitudesTrazaPadre(bodyTrazaPadreParam);

            // Transformar result2 a la estructura requerida
            var traza = result
                .GroupBy(c => c.ETAPA_FRONT)
                .Select(group =>
                {
                    var status = group.Any(g => g.STATUS.Equals("No resuelto", StringComparison.OrdinalIgnoreCase))
                        ? "No resuelto"
                        : group.First().STATUS;

                    var fechaEtapa = group.First().FEC_ING_REG;
                    var fechaEtapaString = fechaEtapa == DateTime.MinValue
                        ? string.Empty
                        : fechaEtapa.ToString("dd/MM/yyyy");

                    return new DtoResponseInfoPadre
                    {
                        idSiniestro = group.First().ID_SINIESTRO.ToString(CultureInfo.InvariantCulture),
                        estado = group.Key,
                        status = status,
                        fechaEtapa = fechaEtapaString,
                        informacion = group
                            .OrderBy(i => i.ID_ORDEN)
                            .Select(i =>
                            {
                                var fechaDescripcion = i.FEC_ING_REG == DateTime.MinValue
                                    ? string.Empty
                                    : i.FEC_ING_REG.ToString("dd/MM/yyyy");
                                return new DtoInformacionTraza
                                {
                                    descripcion =
                                        i.DESCRIPCION_INFO.Equals("SIN TEXTO", StringComparison.OrdinalIgnoreCase)
                                            ? string.Empty
                                            : i.DESCRIPCION_INFO,
                                    titulo = i.TITULO_INFO.Equals("SIN TEXTO", StringComparison.OrdinalIgnoreCase)
                                        ? string.Empty
                                        : i.TITULO_INFO,
                                    fechaDescripcion = fechaDescripcion,
                                    idOrden = i.ID_ORDEN,
                                    estiloBoton = i.COLOR
                                };
                            }).Where(w => !string.IsNullOrEmpty(w.fechaDescripcion))
                            .ToList()
                    };
                })
                .ToList();


            return result.Select(s => new DtoResponseTrazaPadre
            {
                estado = s.ESTADO,
                color = s.COLOR,
                informacionPadre = traza
            }).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obtenerDiagnostico"></param>
        /// <returns></returns>
        /// <exception cref="ContentManagerException"></exception>
        public async Task<List<DtoResponseDiagnostico>> ObtenerDiagnosticoVc(BodyObtenerDiagnostico obtenerDiagnostico)
        {
            try
            {
                var result = await _productoInfrastructure.ObtenerDiagnosticoVc(obtenerDiagnostico);
                var dto = _mapper.Map<List<DtoResponseDiagnostico>>(result);

                return dto;
            }
            catch (Exception e)
            {
                throw new ContentManagerException("Error al cargar archivo: " + e.Message);
            }
        }
    }
}
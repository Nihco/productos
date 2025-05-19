using ProductosBFF.Domain.Accidentes;
using ProductosBFF.Domain.Causales;
using ProductosBFF.Domain.Parameters;
using ProductosBFF.Domain.Productos;
using ProductosBFF.Interfaces;
using ProductosBFF.Models.BCCesantia;
using ProductosBFF.Models.Productos;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ProductosBFF.Domain.SegundaClave;

namespace ProductosBFF.Infrastructure
{
    /// <summary>
    /// Clase
    /// </summary>
    public class ProductoInfrastructure : IProductoInfrastructure
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _url;

        /// <summary>
        /// Constructor
        /// </summary>
        public ProductoInfrastructure(IHttpClientService httpClientService,
            IConfiguration comfig)
        {
            _httpClientService = httpClientService;
            _url = comfig.GetValue<string>("PRODUCTO:PRODUCTO_URL");
        }

        /// <summary>
        /// GetProductsAsync
        /// </summary>
        /// <param name="rut"></param>
        /// <returns></returns>
        public async Task<List<Producto>> GetProductsAsync(BodyProducto rut)
        {
            return await _httpClientService.PostAsync<List<Producto>>(_url + "Products/Productos", rut);
        }

        /// <summary>
        /// GetProductosLeyCorta
        /// </summary>
        /// <param name="rut"></param>
        /// <returns></returns>
        public async Task<List<ProductoLeyCorta>> GetProductosLeyCorta(decimal rut)
        {
            return await _httpClientService.GetAsync<List<ProductoLeyCorta>>(_url +
                                                                             $"Products/ProductosLeyCorta/{rut}");
        }

        /// <summary>
        /// GetProductosLeyCorta
        /// </summary>
        /// <param name="rut"></param>
        /// <returns></returns>
        public async Task<List<ProductosCostoCero>> GetProductosCostoCero(decimal rut)
        {
            return await _httpClientService.GetAsync<List<ProductosCostoCero>>(_url +
                                                                               $"Products/ProductosCostoCero/{rut}");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="domiCodigo"></param>
        /// <returns></returns>
        public async Task<bool> ValidaBcCostoCero(int folio, int domiCodigo)
        {
            return await _httpClientService.GetAsync<bool>(_url + $"Products/ValidaBcCostoCero/{folio}/{domiCodigo}");
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bbodyCancelarParam"></param>
        /// <returns></returns>
        public async Task<decimal> CancelarBcCostoCero(BodyCancelarParamDto bbodyCancelarParam)
        {
            return await _httpClientService.PostAsync<decimal>(
                _url + "Products/CancelaBcCostoCero", bbodyCancelarParam);
        }

        /// <summary>
        /// Metoodo que trae el comprobante
        /// </summary>
        /// <param name="obtenerComprobanteParam"></param>
        /// <returns></returns>
        public async Task<CancelaBcCostoCeroResponse> ObtenerComprobante(
            ObtenerComprobanteParam obtenerComprobanteParam)
        {
            return await _httpClientService.PostAsync<CancelaBcCostoCeroResponse>(
                _url + "Products/ObtenerComprobante", obtenerComprobanteParam);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idAuditoria"></param>
        /// <returns></returns>
        public async Task<ResponseAuditoria> ObtenerAuditoria(int idAuditoria)
        {
            return await _httpClientService.GetAsync<ResponseAuditoria>(_url + "SegundaClave/ObtenerAuditoria/" +
                                                                        idAuditoria);
        }

        /// <summary> Descarga BC </summary>
        /// <param name="CodeBC"></param>
        /// <returns></returns>
        public Task<DocumentoBC> GetDocumentsBCBase64(ProductoBC CodeBC)
        {
            return _httpClientService.GetAsync<DocumentoBC>(_url + $"DescargaBC/{CodeBC.BCCode}");
        }


        /// <summary>
        /// envia datos del afiliado a guardar a la bd
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseBcAfilSini> EnviarDatosAfilBCCesantia(DtoAfil bodyBcCesantiaAfil)
        {
            return await _httpClientService.PostAsync<ResponseBcAfilSini>(_url + "BCCesantia/EnviarAfilSini",
                bodyBcCesantiaAfil);
        }

        /// <summary>
        /// Envia Datos de archivo a la bd
        /// </summary>
        /// <param name="bodyBcArchivo"></param>
        /// <returns></returns>
        public async Task<ResponseEnviarArchivo> EnviarArchivosSini(BodyBcArchivo bodyBcArchivo)
        {
            return await _httpClientService.PostAsync<ResponseEnviarArchivo>(_url + "BCCesantia/EnviarArchivo",
                bodyBcArchivo);
        }

        /// <summary>
        /// Envia documento a guardar en content manager
        /// </summary>
        /// <param name="docCmCesantia"></param>
        /// <returns></returns>
        public async Task<DocCesantia> EnviarDatosDocBCCesantia(DtoDocCm docCmCesantia)
        {
            return await _httpClientService.PutAsync<DocCesantia>(_url + "BCCesantia/EnviarDatosDocBCCesantia",
                docCmCesantia);
        }

        /// <summary>
        /// Envia Datos para api Afil para id content fun4
        /// </summary>
        /// <param name="bodyDatosFun4"></param>
        /// <returns></returns>
        public async Task<Fun4> EnviarFun4(DtoFun4 bodyDatosFun4)
        {
            return await _httpClientService.PostAsync<Fun4>(_url + "BCCesantia/Traefun4", bodyDatosFun4);
        }

        /// <summary>
        /// Obtiene las Causales de despido
        /// </summary>
        /// <returns></returns>
        public async Task<List<Causales>> GetCausalesAsync()
        {
            return await _httpClientService.GetAsync<List<Causales>>(_url + "Causales");
        }

        /// <summary>
        /// Servicio que trae Motivos Cancelacion
        /// </summary>
        /// <returns></returns>
        public async Task<List<MotivosCancelacion>> GetMotivosCancelacion()
        {
            return await _httpClientService.GetAsync<List<MotivosCancelacion>>(_url + "Causales/GetMotivosCancelacion");
        }

        /// <summary>
        /// Obtiene la continuidad del Producto
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ContinuidadProducto(ContinuidadProducto rut)
        {
            return await _httpClientService.GetAsync<bool>(_url + "ContinuidadProducto", rut);
        }

        /// <summary>
        /// Registra la continuidad de un BC de cesantía
        /// </summary>
        /// <param name="rut"></param>
        /// <returns>El Id del siniestro en caso de haber sido ejecutado correctamente</returns>
        public async Task<int> RegistraContinuidadCesantia(int rut)
        {
            return await _httpClientService.PutAsync<int>(_url +
                                                          $"ContinuidadProducto/RegistraContinuidadCesantia?rut={rut}");
        }


        /// <summary>
        /// envia los datos para el correo de rechazo
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseEnvioCorreo> EnviarCorreo(DtoEnvioCorreo envioCorreo)
        {
            return await _httpClientService.PostAsync<ResponseEnvioCorreo>(_url + "BCCesantia/EnvioCorreo",
                envioCorreo);
        }

        /// <summary>
        /// Realiza la validación de la función 4 con los datos proporcionados.
        /// </summary>
        /// <param name="obtieneFun4">Datos de entrada para la validación de la función 4.</param>
        /// <returns>Una tarea asincrónica que representa la operación, devolviendo la respuesta de validación de la función 4.</returns>
        public async Task<ResponseValidaFun4> ValidaFun4(ObtieneFun4 obtieneFun4)
        {
            return await _httpClientService.PostAsync<ResponseValidaFun4>(_url + "BCCesantia/ValidaFun4", obtieneFun4);
        }

        /// <summary>
        /// ObtenerTipoAccidente
        /// </summary>        
        /// <returns></returns>
        public async Task<List<TipoAccidente>> ObtenerTipoAccidente()
        {
            return await _httpClientService.GetAsync<List<TipoAccidente>>(_url + $"Accidentes/TipoAccidentes");
        }

        /// <summary>
        /// ObtenerCausaAccidente
        /// </summary>   
        ///  /// <param name="tipoAccidente"></param>
        /// <returns></returns>
        public async Task<List<CausalAccidente>> ObtenerCausalAccidente(ParamTipoAccidente tipoAccidente)
        {
            return await _httpClientService.GetAsync<List<CausalAccidente>>(_url +
                                                                            $"Accidentes/CausalAccidentes/{tipoAccidente.IdTipoAccidente}");
        }

        /// <summary>
        /// ObtenerDocumentosAccidente
        /// </summary>   
        ///  /// <param name="documentosAccidente"></param>
        /// <returns></returns>
        public async Task<List<DocumentosAccidente>> ObtenerDocumentosAccidente(
            BodyDocumentosAccidente documentosAccidente)
        {
            return await _httpClientService.PostAsync<List<DocumentosAccidente>>(
                _url + "Accidentes/DocumentosAccidentes", documentosAccidente);
        }

        /// <summary>
        /// IngresarArchivoAccidente
        /// </summary>   
        ///  /// <param name="ingresarArchivoAccidente"></param>
        /// <returns></returns>
        public async Task<IngresarArchivoAccidente> IngresarArchivoAccidente(
            BodyIngresarArchivoAccidente ingresarArchivoAccidente)
        {
            return await _httpClientService.PostAsync<IngresarArchivoAccidente>(
                _url + "Accidentes/IngresarArchivoAccidente", ingresarArchivoAccidente);
        }

        /// <summary>
        /// IngresarAccidente
        /// </summary>   
        ///  /// <param name="ingresarAccidente"></param>
        /// <returns></returns>
        public async Task<IngresarAccidente> IngresarAccidente(BodyIngresarAccidente ingresarAccidente)
        {
            return await _httpClientService.PostAsync<IngresarAccidente>(_url + "Accidentes/IngresarAccidentes",
                ingresarAccidente);
        }

        /// <summary>
        /// cargarArchivoShira
        /// </summary>   
        ///  /// <param name="cargarArchivoShira"></param>
        /// <returns></returns>
        public async Task<CargarArchivoShira> CargaArchivosShira(BodyCargarArchivoShira cargarArchivoShira)
        {
            return await _httpClientService.PostAsync<CargarArchivoShira>(_url + "Accidentes/CargaArchivosShira",
                cargarArchivoShira);
        }

        /// <summary>
        /// Envia documento a guardar en content manager
        /// </summary>
        /// <param name="cargarArchivoContent"></param>
        /// <returns></returns>
        public async Task<DocCesantia> CargaArchivosContent(BodyCargarArchivoContent cargarArchivoContent)
        {
            return await _httpClientService.PutAsync<DocCesantia>(_url + "BCCesantia/EnviarDatosDocBCCesantia",
                cargarArchivoContent);
        }

        /// <summary>
        /// Historial Solicitudes Cabecera
        /// </summary>
        /// <param name="bodyHistorialCabecera"></param>
        /// <returns></returns>
        public async Task<List<ResponseDetalleSolicitudVC>> DetalleSolicitudVC(
            BodyHistorialCabecera bodyHistorialCabecera)
        {
            return await _httpClientService.PostAsync<List<ResponseDetalleSolicitudVC>>(
                _url + "BCCesantia/DetalleSolicitudVC", bodyHistorialCabecera);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bodyHistorialCabecera"></param>
        /// <returns></returns>
        public async Task<List<ResponseDetalleSolicitudCesantia>> DetalleSolicitudCesantia(
            BodyHistorialCabecera bodyHistorialCabecera)
        {
            return await _httpClientService.PostAsync<List<ResponseDetalleSolicitudCesantia>>(
                _url + "BCCesantia/DetalleSolicitudCesantia", bodyHistorialCabecera);
        }

        /// <summary>
        /// Trae el historial de solicitudes del afiliado
        /// </summary>
        /// <param name="bodyHistorialSolicitudes"></param>
        /// <returns></returns>
        public async Task<List<ResponseHistorialSolicitudes>> HistorialSolicitudes(
            BodyHistorialSolicitudes bodyHistorialSolicitudes)
        {
            return await _httpClientService.PostAsync<List<ResponseHistorialSolicitudes>>(
                _url + "BCCesantia/HistorialSolicitudes", bodyHistorialSolicitudes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bodyTrazaPadreParam"></param>
        /// <returns></returns>
        public async Task<List<ResponseTrazaPadre>> SolicitudesTrazaPadre(TrazaPadreParam bodyTrazaPadreParam)
        {
            return await _httpClientService.PostAsync<List<ResponseTrazaPadre>>(
                _url + "BCCesantia/SolicitudesTrazaPadre", bodyTrazaPadreParam);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obtenerDiagnostico"></param>
        /// <returns></returns>
        public async Task<List<ResponseObtenerDiagnostico>> ObtenerDiagnosticoVc(
            BodyObtenerDiagnostico obtenerDiagnostico)
        {
            return await _httpClientService.PostAsync<List<ResponseObtenerDiagnostico>>(
                _url + "BCCesantia/ObtenerDiagnosticoVc", obtenerDiagnostico);
        }
    }
}
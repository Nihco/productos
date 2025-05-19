using ProductosBFF.Domain.Accidentes;
using ProductosBFF.Domain.Causales;
using ProductosBFF.Domain.Parameters;
using ProductosBFF.Domain.Productos;
using ProductosBFF.Models.BCCesantia;
using ProductosBFF.Models.Productos;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductosBFF.Domain.SegundaClave;

namespace ProductosBFF.Interfaces
{
    /// <summary>
    /// Interfaz
    /// </summary>
    public interface IProductoInfrastructure
    {
        /// <summary>
        /// GetProducts
        /// </summary>
        /// <param name="rut"></param>
        /// <returns></returns>
        public Task<List<Producto>> GetProductsAsync(BodyProducto rut);

        /// <summary>
        /// GetProducts
        /// </summary>
        /// <param name="rut"></param>
        /// <returns></returns>
        public Task<List<ProductoLeyCorta>> GetProductosLeyCorta(decimal rut);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rut"></param>
        /// <returns></returns>
        public Task<List<ProductosCostoCero>> GetProductosCostoCero(decimal rut);

        /// <summary>
        /// Servicio que cancela bc costo cero
        /// </summary>
        /// <param name="bbodyCancelarParam"></param>
        /// <returns></returns>
        public Task<decimal> CancelarBcCostoCero(BodyCancelarParamDto bbodyCancelarParam);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="domiCodigo"></param>
        /// <returns></returns>
        public Task<bool> ValidaBcCostoCero(int folio, int domiCodigo);

        /// <summary>
        /// Obtener Comprobante
        /// </summary>
        /// <param name="obtenerComprobanteParam"></param>
        /// <returns></returns>
        public Task<CancelaBcCostoCeroResponse> ObtenerComprobante(ObtenerComprobanteParam obtenerComprobanteParam);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idAuditoria"></param>
        /// <returns></returns>
        public Task<ResponseAuditoria> ObtenerAuditoria(int idAuditoria);

        /// <summary>
        /// GetProducts
        /// </summary>
        /// <param name="CodeBC"></param>
        /// <returns></returns>
        public Task<DocumentoBC> GetDocumentsBCBase64(ProductoBC CodeBC);


        /// <summary>
        /// GetCausalesAsync
        /// </summary>
        /// <returns></returns>
        public Task<List<Causales>> GetCausalesAsync();

        /// <summary>
        /// Get Motivos Cancelacion
        /// </summary>
        /// <returns></returns>
        public Task<List<MotivosCancelacion>> GetMotivosCancelacion();

		/// <summary>
		/// EnviarDatosAfilBCCesantia
		/// </summary>
		/// <param name="bodyBcCesantiaAfil"></param>
		/// <returns></returns>
		public Task<ResponseBcAfilSini> EnviarDatosAfilBCCesantia(DtoAfil bodyBcCesantiaAfil);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="docCmCesantia"></param>
        /// <returns></returns>
        public Task<DocCesantia> EnviarDatosDocBCCesantia(DtoDocCm docCmCesantia);

        /// <summary>
        /// Envia parametros para traer fun4
        /// </summary>
        /// <param name="bodyDatosFun4"></param>
        /// <returns></returns>
        public Task<Fun4> EnviarFun4(DtoFun4 bodyDatosFun4);

        /// <summary>
        /// Envia datos Bc Archivo
        /// </summary>
        /// <param name="bodyBcArchivo"></param>
        /// <returns></returns>
        public Task<ResponseEnviarArchivo> EnviarArchivosSini(BodyBcArchivo bodyBcArchivo);

        /// <summary>
        /// Servicio que consulta la continuidad del producto
        /// </summary>
        /// <param name="rut"></param>
        /// <returns></returns>
        public Task<bool> ContinuidadProducto(ContinuidadProducto rut);

        /// <summary>
        /// Registra la continuidad de un BC de cesantía
        /// </summary>
        /// <param name="rut"></param>
        /// <returns>El Id del siniestro en caso de haber sido ejecutado correctamente</returns>
        public Task<int> RegistraContinuidadCesantia(int rut);

        /// <summary>
        /// Envia datos para correo de rechazo
        /// </summary>
        /// <param name="envioCorreo"></param>
        /// <returns></returns>
        public Task<ResponseEnvioCorreo> EnviarCorreo(DtoEnvioCorreo envioCorreo);

        /// <summary>
        /// Realiza la validación de la función 4 con los datos proporcionados.
        /// </summary>
        /// <param name="obtieneFun4">Datos de entrada para la validación de la función 4.</param>
        /// <returns>Una tarea asincrónica que representa la operación, devolviendo la respuesta de validación de la función 4.</returns>
        public Task<ResponseValidaFun4> ValidaFun4(ObtieneFun4 obtieneFun4);


        /// <summary>
        /// Obtiene tipos de aacidente
        /// </summary>
        /// <returns></returns>
        public Task<List<TipoAccidente>> ObtenerTipoAccidente();

        /// <summary>
        /// ObtenerCausaAccidente
        /// </summary>   
        ///  /// <param name="tipoAccidente"></param>
        /// <returns></returns>
        public Task<List<CausalAccidente>> ObtenerCausalAccidente(ParamTipoAccidente tipoAccidente);

        /// <summary>
        /// ObtenerDocumentosAccidente
        /// </summary>   
        ///  /// <param name="documentosAccidente"></param>
        /// <returns></returns>
        public Task<List<DocumentosAccidente>> ObtenerDocumentosAccidente(BodyDocumentosAccidente documentosAccidente);

        /// <summary>
        /// IngresarArchivoAccidente
        /// </summary>   
        ///  /// <param name="ingresarArchivoAccidente"></param>
        /// <returns></returns>
        public Task<IngresarArchivoAccidente> IngresarArchivoAccidente(
            BodyIngresarArchivoAccidente ingresarArchivoAccidente);

        /// <summary>
        /// IngresarAccidente
        /// </summary>   
        ///  /// <param name="ingresarAccidente"></param>
        /// <returns></returns>
        public Task<IngresarAccidente> IngresarAccidente(BodyIngresarAccidente ingresarAccidente);

        /// <summary>
        /// cargarArchivoShira
        /// </summary>   
        ///  /// <param name="cargarArchivoShira"></param>
        /// <returns></returns>
        public Task<CargarArchivoShira> CargaArchivosShira(BodyCargarArchivoShira cargarArchivoShira);


        /// <summary>
        /// Carga documento content
        /// </summary>
        /// <param name="cargarArchivoContent"></param>
        /// <returns></returns>
        public Task<DocCesantia> CargaArchivosContent(BodyCargarArchivoContent cargarArchivoContent);

        /// <summary>
        /// Historial solicitudes cabecera
        /// </summary>
        /// <param name="bodyHistorialCabecera"></param>
        /// <returns></returns>
        public Task<List<ResponseDetalleSolicitudVC>> DetalleSolicitudVC(
            BodyHistorialCabecera bodyHistorialCabecera);

        /// <summary>
        /// DetalleSolicitudCesantia
        /// </summary>
        /// <param name="bodyHistorialCabecera"></param>
        /// <returns></returns>
        public Task<List<ResponseDetalleSolicitudCesantia>> DetalleSolicitudCesantia(
            BodyHistorialCabecera bodyHistorialCabecera);

        /// <summary>
        /// Trae el historial de solicitudes del afiliado
        /// </summary>
        /// <param name="bodyHistorialSolicitudes"></param>
        /// <returns></returns>
        public Task<List<ResponseHistorialSolicitudes>> HistorialSolicitudes(
            BodyHistorialSolicitudes bodyHistorialSolicitudes);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bodyTrazaPadreParam"></param>
        /// <returns></returns>
        public Task<List<ResponseTrazaPadre>> SolicitudesTrazaPadre(TrazaPadreParam bodyTrazaPadreParam);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obtenerDiagnostico"></param>
        /// <returns></returns>
        public Task<List<ResponseObtenerDiagnostico>> ObtenerDiagnosticoVc(BodyObtenerDiagnostico obtenerDiagnostico);
    }
}
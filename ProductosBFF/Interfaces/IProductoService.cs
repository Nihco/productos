using ProductosBFF.Domain.Parameters;
using ProductosBFF.Models.Accidentes;
using ProductosBFF.Models.BCCesantia;
using ProductosBFF.Models.Causales;
using ProductosBFF.Models.Productos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductosBFF.Interfaces
{
    /// <summary>
    /// Interface
    /// </summary>
    public interface IProductoService
    {
        /// <summary>
        /// GetProductos
        /// </summary>
        /// <param name="rut"></param>
        /// <returns></returns>
        public Task<List<ProductDto>> GetProductos(BodyProducto rut);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folio"></param>
        /// <param name="domiCodigo"></param>
        /// <returns></returns>
        public Task<bool> ValidaBcCostoCero(int folio, int domiCodigo);

        /// <summary>
        /// GetProductosLeyCorta
        /// </summary>
        /// <param name="rut"></param>
        /// <returns></returns>
        public Task<List<ProductLeyCortaDto>> GetProductosLeyCorta(decimal rut);

        /// <summary>
        /// Servicio que cancela bc costo cero
        /// </summary>
        /// <param name="bodyCancelarBcParam"></param>
        /// <returns></returns>
        public Task<CancelaBcCostoCeroResponse> CancelaBcCostoCero(BodyCancelarParamDto bodyCancelarBcParam);

        /// <summary> Get Base 64 BC </summary>
        /// <param name="codeBc"></param>
        /// <returns></returns>
        public Task<DocumentoBCDto> GetDocumentsBase64(ProductoBC codeBc);

        /// <summary>
        /// Obtiene las Causales de despido
        /// </summary>
        /// <returns></returns>
        public Task<List<CausalesDto>> GetCausales();

        /// <summary>
        /// Trae Motivos Cancelacion
        /// </summary>
        /// <returns></returns>
        public Task<List<MotivosCancelacionDto>> GetMotivos();


		/// <summary>
		/// Servicio que envia datos del afiliado y doc content manager
		/// </summary>
		/// <param name="solicitud"></param>
		/// <returns></returns>
		public Task<ResponseBcAfilSini> EnviarDatosCesantia(BodySolicitudActivacion solicitud);

        /// <summary>
        /// Servicio que consulta la continuidad del producto
        /// </summary>
        /// <param name="rut"></param>
        /// <returns></returns>
        public Task<bool> GetContinuidadProductoService(ContinuidadProducto rut);

        /// <summary>
        /// Registra una nueva solicitud de continuidad de BC de cesantía
        /// </summary>
        /// <param name="solicitud"></param>
        /// <returns>El Id del siniestro en caso de haber sido ejecutado correctamente</returns>
        public Task<int> RegistraSolicitudContinuidadCesantia(BodySolicitudActivacion solicitud);

        /// <summary>
        /// Servicio que envia los datos para crear el correo de rechazo de la solicitud.
        /// </summary>
        /// <param name="envioCorreo"></param>
        /// <returns></returns>
        public Task<ResponseEnvioCorreo> EnviarCorreo(EnvioCorreo envioCorreo);

        /// <summary>
        /// Servicio que genera el documento fun 4
        /// </summary>
        /// <param name="datosFun4"></param>
        /// <returns></returns>
        public Task<Fun4> EnviarFun4(DtoFun4 datosFun4);

        /// <summary>
        /// Obtiene tipos de aacidente
        /// </summary>
        /// <returns></returns>
        public Task<List<TipoAccidenteDto>> ObtenerTipoAccidente();

        /// <summary>
        /// ObtenerCausaAccidente
        /// </summary>   
        ///  /// <param name="tipoAccidente"></param>
        /// <returns></returns>
        public Task<List<CausalAccidenteDto>> ObtenerCausalAccidente(ParamTipoAccidente tipoAccidente);

        /// <summary>
        /// ObtenerDocumentosAccidente
        /// </summary>   
        ///  /// <param name="documentosAccidente"></param>
        /// <returns></returns>
        public Task<List<DocumentosAccidenteDto>> ObtenerDocumentosAccidente(BodyDocumentosAccidente documentosAccidente);

        /// <summary>
        /// IngresarArchivoAccidente
        /// </summary>   
        ///  /// <param name="ingresarArchivoAccidente"></param>
        /// <returns></returns>
        public Task<IngresarArchivoAccidenteDto> IngresarArchivoAccidente(BodyIngresarArchivoAccidente ingresarArchivoAccidente);

        /// <summary>
        /// IngresarAccidente
        /// </summary>   
        ///  /// <param name="ingresarAccidente"></param>
        /// <returns></returns>
        public Task<IngresarAccidenteDto> IngresarAccidente(BodyIngresarAccidente ingresarAccidente);


        /// <summary>
        /// cargarArchivoShira
        /// </summary>   
        ///  /// <param name="cargarArchivoShira"></param>
        /// <returns></returns>
        public Task<CargarArchivoShiraDto> CargaArchivosShira(BodyCargarArchivoShira cargarArchivoShira);

        /// <summary>
        /// Carga documento content
        /// </summary>
        /// <param name="cargarArchivoContent"></param>
        /// <returns></returns>
        public Task<CargarArchivoContentDto> CargaArchivosContent(BodyCargarArchivoContent cargarArchivoContent);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bodyHistorialCabecera"></param>
        /// <returns></returns>
        public Task<List<DtoDetalleSolicitudVC>> DetalleSolicitudVC(
            BodyHistorialCabecera bodyHistorialCabecera);

        /// <summary>
        /// DetalleSolicitudCesantia
        /// </summary>
        /// <param name="bodyHistorialCabecera"></param>
        /// <returns></returns>
        public Task<List<DtoDetalleSolicitudCesantia>> DetalleSolicitudCesantia(
            BodyHistorialCabecera bodyHistorialCabecera);

        /// <summary>
        /// Metodo que trae el historial de solicitudes del afiliado
        /// </summary>
        /// <param name="bodyHistorialSolicitudes"></param>
        /// <returns></returns>
        public Task<List<DtoHistorialSolicitudes>> HistorialSolicitudes(
            BodyHistorialSolicitudes bodyHistorialSolicitudes);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bodyTrazaPadreParam"></param>
        /// <returns></returns>
        public Task<DtoResponseTrazaPadre> SolicitudesTrazaPadre(TrazaPadreParam bodyTrazaPadreParam);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obtenerDiagnostico"></param>
        /// <returns></returns>
        public Task<List<DtoResponseDiagnostico>> ObtenerDiagnosticoVc(BodyObtenerDiagnostico obtenerDiagnostico);
    }
}

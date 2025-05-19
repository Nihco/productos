namespace ProductosBFF.Models.BCCesantia
{
    /// <summary>
    /// Datos documentos guardados en Sini Archivo
    /// </summary>
    public class DtoDocSini
    {
        /// <summary>
        /// Id Solicitud
        /// </summary>
        public string PinIdSiniestro { get; set; }

        /// <summary>
        /// ID parametro documento
        /// </summary>
        /// Quitar
        public int PinIdParametro { get; set; }

        /// <summary>
        /// Tipo Documento
        /// </summary>
        public string PivTipoDoc { get; set; }

        /// <summary>
        /// Detalle Contenido
        /// </summary>
        public string PivDetalleContent { get; set; }

        /// <summary>
        /// Id Contenido
        /// </summary>
        public string PinIdContent { get; set; }
    }
}
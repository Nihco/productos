namespace ProductosBFF.Domain.Parameters
{
    /// <summary>
    /// class
    /// </summary>
    public class BodyIngresarArchivoAccidente
    {
        /// <summary>
        /// IdSiniestro
        /// </summary>
        public decimal IdSiniestro { get; set; }
        /// <summary>
        /// IdContent
        /// </summary>
        public string IdContent { get; set; }
        /// <summary>
        /// DetalleContent
        /// </summary>
        public string DetalleContent { get; set; }
        /// <summary>
        /// IdArchReq
        /// </summary>
        public decimal IdArchReq { get; set; }
        /// <summary>
        /// TipoDoc
        /// </summary>
        public string TipoDoc { get; set; }
        /// <summary>
        /// RutaArchivo
        /// </summary>
        public string RutaArchivo { get; set; }
        /// <summary>
        /// Declaracion
        /// </summary>
        public int Declaracion { get; set; }
    }
}




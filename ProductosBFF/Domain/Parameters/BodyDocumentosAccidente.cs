namespace ProductosBFF.Domain.Parameters
{
    /// <summary>
    /// Clase
    /// </summary>
    public class BodyDocumentosAccidente
    {
        /// <summary>
        /// IdBc
        /// </summary>
        public decimal IdBc { get; set; }
        /// <summary>
        /// TipoAccidente
        /// </summary>
        public decimal TipoAccidente { get; set; }
        /// <summary>
        /// TipoAtencion
        /// </summary>
        public decimal TipoAtencion { get; set; }

        /// <summary>
        /// MarcaUrgencia
        /// </summary>
        public string MarcaUrgencia { get; set; }
        /// <summary>
        /// MarcaIntervencion
        /// </summary>
        public string MarcaIntervencion { get; set; }
        /// <summary>
        /// MarcaSeguro
        /// </summary>
        public string RcaSeguro { get; set; }
        /// <summary>
        /// Continuidad
        /// </summary>
        public string Continuidad { get; set; }
    }
}

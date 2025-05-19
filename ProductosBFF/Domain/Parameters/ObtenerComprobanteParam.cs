namespace ProductosBFF.Domain.Parameters
{
    /// <summary>
    /// Clase
    /// </summary>
    public class ObtenerComprobanteParam
    {
        /// <summary>
        /// Folio del afiliado
        /// </summary>
        public string Folio { get; set; }
        
        /// <summary>
        /// Codigo Bc
        /// </summary>
        public string Codigo { get; set; }
        
        /// <summary>
        /// Registro de log de termino
        /// </summary>
        public string LogTermino { get; set; }
        
        /// <summary>
        /// Sistema
        /// </summary>
        public string Canal { get; set; }
    }
}
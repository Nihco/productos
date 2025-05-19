namespace ProductosBFF.Models.SegundaClave
{
    /// <summary>
    /// 
    /// </summary>
    public class VerificarSegundaClaveDto
    {
        /// <summary>
        /// DescripcionTransaccion
        /// </summary>
        public string DescripcionTransaccion { get; set; }

        /// <summary>
        /// Folio
        /// </summary>
        public long Folio { get; set; }

        /// <summary>
        /// Codigo
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Ip
        /// </summary>
        public string Ip { get; set; }
        
        
        /// <summary>
        /// Rut
        /// </summary>
        public long Rut { get; set; }

        /// <summary>
        /// Dv
        /// </summary>
        public string Dv { get; set; }
    }
}

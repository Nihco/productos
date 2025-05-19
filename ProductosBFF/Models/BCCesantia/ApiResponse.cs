namespace ProductosBFF.Models.BCCesantia
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Codigo error
        /// </summary>
        public string CodResult { get; set; }
        /// <summary>
        /// Descripcion error
        /// </summary>
        public string MessageResult { get; set; }
        /// <summary>
        /// Descripcion error
        /// </summary>
        public string IdSiniestro { get; set; }
    }
}
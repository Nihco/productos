namespace ProductosBFF.Models.BCCesantia
{
    /// <summary>
    /// Clase que recibe siniestro
    /// </summary>
    public class ResponseBcAfilSini
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
        /// Id Siniestro 
        /// </summary>
        public string IdSiniestro { get; set; }
    }
}
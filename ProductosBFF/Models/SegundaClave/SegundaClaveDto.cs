namespace ProductosBFF.Models.SegundaClave
{
    /// <summary>
    /// SegundaClaveDto
    /// </summary>
    public class SegundaClaveDto
    {
        /// <summary>
        /// CodigoEstado
        /// </summary>
        public string CodigoEstado { get; set; }

        /// <summary>
        /// Tiempo de vida en segundos
        /// </summary>
        public int TtlSegundos { get; set; }
    }
}

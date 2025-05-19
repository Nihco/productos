namespace ProductosBFF.Domain.SegundaClave
{
    /// <summary>
    /// SegundaClaveDto
    /// </summary>
    public class SegundaClaveResponse
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

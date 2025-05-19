namespace ProductosBFF.Domain.Parameters
{
    /// <summary>
    /// Clase para cancelar Bc Costo Cero
    /// </summary>
    public class BodyCancelarBcParam
    {
        /// <summary>
        /// Pin Folio AFiliada
        /// </summary>
        public int pin_folio { get; set; }

        /// <summary>
        ///  Codigo Bc 
        /// </summary>
        public string pin_codigo_bc { get; set; }

        /// <summary>
        /// Codigo Motivo de cancelar
        /// </summary>
        public string pin_codigo_motivo { get; set; }

    }
}
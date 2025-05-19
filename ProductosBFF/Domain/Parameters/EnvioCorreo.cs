namespace ProductosBFF.Domain.Parameters
{
    /// <summary>
    /// Clase que representa los datos necesarios para el envío de correo.
    /// </summary>
    public class EnvioCorreo
    {
        /// <summary>
        /// Obtiene o establece el tipo de rechazo de la solicitud desde la NSD.
        /// </summary>
        public string PinTipoRechazo { get; set; }

        /// <summary>
        /// Obtiene o establece el Rut del Afiliado.
        /// </summary>
        public int PinRutAfil { get; set; }

        /// <summary>
        /// Obtiene o establece el dígito verificador del Afiliado.
        /// </summary>
        public string PivDvAfil { get; set; }

        /// <summary>
        /// Obtiene o establece el correo electrónico del Afiliado.
        /// </summary>
        public string PivMailAfil { get; set; }

        /// <summary>
        /// Obtiene o establece los nombres del Afiliado.
        /// </summary>
        public string PivNombres { get; set; }
    }
}

namespace ProductosBFF.Models.BCCesantia
{
    /// <summary>
    /// Respuesta a envio de correo
    /// </summary>
    public class ResponseEnvioCorreo
    {
        /// <summary>
        /// Codigo Error
        /// </summary>
        public string Error { get; set; }
        /// <summary>
        /// Mensaje Error
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}

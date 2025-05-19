namespace ProductosBFF.Domain.Productos
{
    /// <summary>
    /// Clase
    /// </summary>
    public class DocCesantia
    {
        /// <summary>
        /// Objeto resultado content manager
        /// </summary>
        public Result Result { get; set; }

        /// <summary>
        /// Mensage de estado de la operacion
        /// </summary>
        public bool SuccessfulOperation { get; set; }
    }

    /// <summary>
    /// Clase
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Id Content recibido de Content Manager
        /// </summary>
        public string Idcontent { get; set; }

        /// <summary>
        /// Estado de carga
        /// </summary>
        public string EstadoCarga { get; set; }

        /// <summary>
        /// Mensaje de error en caso de fallar
        /// </summary>
        public string MensajeError { get; set; }
    }
}

namespace ProductosBFF.Domain.Parameters
{
    /// <summary>
    /// Clase
    /// </summary>
    public class BodyCargarArchivoShira
    {

        /// <summary>
        /// PIV_ARCHIVO
        /// </summary>
        public string PIV_Archivo { get; set; }

        /// <summary>
        /// PIV_NOMBRE_ARCHIVO
        /// </summary>
        public string PIV_NombreArchivo { get; set; }

        /// <summary>
        /// Rut
        /// </summary>
        public decimal PIN_Rut { get; set; }

        /// <summary>
        /// Ruta de carga Shira
        /// </summary>
        public string PIV_RutaArchivo { get; set; }
    }
}

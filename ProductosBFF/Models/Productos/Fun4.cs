namespace ProductosBFF.Models.Productos
{
    /// <summary>
    /// Clase para recibir la respuesta de la API Afil para Fun4.
    /// </summary>
    public class Fun4
    {
        /// <summary>
        /// Obtiene o establece el mensaje de la respuesta.
        /// </summary>
        public string Mensaje { get; set; }

        /// <summary>
        /// Obtiene o establece el código de la respuesta.
        /// </summary>
        public int Codigo { get; set; }

        /// <summary>
        /// Obtiene o establece el número de Fun.
        /// </summary>
        public int Fun { get; set; }

        /// <summary>
        /// Obtiene o establece el contenido de la respuesta.
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Obtiene o establece el código PIN de Fun4.
        /// </summary>
        public int PinCodFun4 { get; set; }
    }
}
namespace ProductosBFF.Models.Productos
{
    /// <summary>
    /// DocumentoBCDto
    /// </summary>
    public class DocumentoBCDto
    {
        /// <summary> Archivo Base 64 </summary>
        public string Base64 { get; set; }

        /// <summary> Extensión </summary>
        public string Cabecera { get; set; }

        /// <summary> Archivo </summary>
        public string Titulo { get; set; }
    }
}


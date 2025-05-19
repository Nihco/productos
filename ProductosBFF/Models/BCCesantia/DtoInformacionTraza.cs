namespace ProductosBFF.Models.BCCesantia
{
    /// <summary>
    /// Informacion Traza
    /// </summary>
    public class DtoInformacionTraza
    {
        /// <summary>
        /// Titulo
        /// </summary>
        public string titulo { get; set; }

        /// <summary>
        /// Descripcion
        /// </summary>
        public string descripcion { get; set; }

        /// <summary>
        /// Fecha Descripcion
        /// </summary>
        public string fechaDescripcion { get; set; }

        /// <summary>
        /// Enlace
        /// </summary>
        public string enlace { get; set; }

        /// <summary>
        /// Boton
        /// </summary>
        public string boton { get; set; }

        /// <summary>
        /// Enlace Boton
        /// </summary>
        public string enlaceBoton { get; set; }

        /// <summary>
        /// Estilo boton
        /// </summary>
        public string estiloBoton { get; set; }

        /// <summary>
        /// Id Orden
        /// </summary>
        public decimal idOrden { get; set; }


    }
}
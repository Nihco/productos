namespace ProductosBFF.Models.BCCesantia
{
    /// <summary>
    /// Clase que representa la respuesta de validación de la función 4.
    /// </summary>
    public class ResponseValidaFun4
    {
        /// <summary>
        /// Obtiene o establece el identificador del Content Manager.
        /// </summary>
        public string ID_Content_Manager { get; set; }

        /// <summary>
        /// Obtiene o establece el folio para la validación de la función 4.
        /// </summary>
        public int Folio { get; set; }

        /// <summary>
        /// Solciitud si es valida de crear
        /// </summary>
        public string SolicitudValida { get; set; }
    }

}
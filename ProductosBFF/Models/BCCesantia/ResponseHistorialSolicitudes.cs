using System;

namespace ProductosBFF.Models.BCCesantia
{
    /// <summary>
    /// Clase que recipe desde la APi Productos el historial de solicitudes
    /// </summary>
    public class ResponseHistorialSolicitudes
    {
        /// <summary>
        /// Tipo Solicitud
        /// </summary>
        public string tipo_solicitud { get; set; }

        /// <summary>
        /// BC
        /// </summary>
        public string producto_adicional { get; set; }

        /// <summary>
        /// Fecha Solicitud
        /// </summary>
        public DateTime fecha_solicitud { get; set; }

        /// <summary>
        /// Id del siniestro
        /// </summary>
        public int Id_Solicitud { get; set; }

        /// <summary>
        /// Descripcion de la solicitud
        /// </summary>
        public string Descripcion_tipo_solicitud { get; set; }

        /// <summary>
        /// Color que va a mostrar
        /// </summary>
        public string Tipo_Sol_Color { get; set; }

        /// <summary>
        /// Id Relacionado
        /// </summary>
        public int IdRelacionado { get; set; }

        /// <summary>
        /// Tipo de siniestro
        /// </summary>
        public string TIPO_SINIESTRO { get; set; }
    }
}
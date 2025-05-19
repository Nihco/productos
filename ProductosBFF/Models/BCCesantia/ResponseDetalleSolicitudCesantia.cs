using System;

namespace ProductosBFF.Models.BCCesantia
{
    /// <summary>
    /// 
    /// </summary>
    public class ResponseDetalleSolicitudCesantia
    {
        /// <summary>
        /// Nombre del afiliado
        /// </summary>
        public string NOMBRE_TITULAR { get; set; }
        /// <summary>
        /// Apellido del afiliado
        /// </summary>
        public string APELLIDOS_TITULAR { get; set; }
        /// <summary>
        /// Causal de despido
        /// </summary>
        public string CAUSAL_DESPIDO { get; set; }
        /// <summary>
        /// Nombre beneficiario
        /// </summary>
        public string NOMBRE_BENEFICIARIO { get; set; }
        /// <summary>
        /// Tipo Solicitud
        /// </summary>
        public string TIPO_SOLICITUD { get; set; }
        /// <summary>
        /// BC
        /// </summary>
        public string PRODUCTO_ADICIONAL { get; set; }
        /// <summary>
        /// Fecha de la solicitud
        /// </summary>
        public DateTime FECHA_SOLICITUD { get; set; }
        /// <summary>
        /// Id Solicitud Original
        /// </summary>
        public decimal ID_SOLICITUD_ORIGINAL { get; set; }
        /// <summary>
        /// Descripcion Tipo Solicitud
        /// </summary>
        public string DESCRIPCION_TIPO_SOLICITUD { get; set; }
        /// <summary>
        /// Color Solicitud
        /// </summary>
        public string TIPO_SOL_COLOR { get; set; }
        /// <summary>
        /// Id Relacionado
        /// </summary>
        public decimal IDRELACIONADO { get; set; }
    }
}
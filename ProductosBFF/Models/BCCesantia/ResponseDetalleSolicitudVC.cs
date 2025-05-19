using System;

namespace ProductosBFF.Models.BCCesantia
{
    /// <summary>
    /// Clase Historial Cabecera
    /// </summary>
    public class ResponseDetalleSolicitudVC
    {
        /// <summary>
        /// Nombre Titular
        /// </summary>
        public string NOMBRE_TITULAR { get; set; }
        
        /// <summary>
        /// Apellidos Titular
        /// </summary>
        public string APELLIDOS_TITULAR { get; set; }
        
        /// <summary>
        /// Diagnostico
        /// </summary>
        public string DIAGNOSTICO { get; set; }
        
        /// <summary>
        /// Nombre Beneficiario
        /// </summary>
        public string NOMBRE_BENEFICIARIO { get; set; }
        
        /// <summary>
        /// Tipo Solicitud
        /// </summary>
        public string TIPO_SOLICITUD { get; set; }
        
        /// <summary>
        /// Producto Adicional
        /// </summary>
        public string PRODUCTO_ADICIONAL { get; set; }
        
        /// <summary>
        /// Fecha Solicitud
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
        /// Color Tipo Solicitud
        /// </summary>
        public string TIPO_SOL_COLOR { get; set; }
        
        /// <summary>
        /// Id Relacionado
        /// </summary>
        public decimal IDRELACIONADO { get; set; }
    }
}
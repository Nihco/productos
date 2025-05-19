using System;

namespace ProductosBFF.Models.Productos
{
    /// <summary>
    /// Clase
    /// </summary>
    public class PactadoDto
    {
        /// <summary>
        /// CODIGO_SUCURSAL_ISAPRE
        /// </summary>
        public int CODIGO_SUCURSAL_ISAPRE { get; set; }
        /// <summary>
        /// RAZON_SOCIAL
        /// </summary>
        public string RAZON_SOCIAL { get; set; }
        /// <summary>
        /// COAF_FOLIO_SUSCRIPCION
        /// </summary>
        public int COAF_FOLIO_SUSCRIPCION { get; set; }
        /// <summary>
        /// COAF_ORGA_CODIGO_ISAPRE
        /// </summary>
        public int COAF_ORGA_CODIGO_ISAPRE { get; set; }
        /// <summary>
        /// SECUENCIA
        /// </summary>
        public int SECUENCIA { get; set; }
        /// <summary>
        /// TIPO_TRABAJADOR
        /// </summary>
        public string TIPO_TRABAJADOR { get; set; }
        /// <summary>
        /// FECHA_INICIO_DESCUENTO_COTIZAC
        /// </summary>
        public DateTime? FECHA_INICIO_DESCUENTO_COTIZAC { get; set; }
        /// <summary>
        /// MODALIDAD_COTIZACION
        /// </summary>
        public string MODALIDAD_COTIZACION { get; set; }
        /// <summary>
        /// RENTA_IMPONIBLE
        /// </summary>
        public int RENTA_IMPONIBLE { get; set; }
        /// <summary>
        /// CECO_CODIGO
        /// </summary>
        public string CECO_CODIGO { get; set; }
        /// <summary>
        /// CECO_UNOR_CORRELATIVO
        /// </summary>
        public int CECO_UNOR_CORRELATIVO { get; set; }
        /// <summary>
        /// CECO_EMPR_RUT
        /// </summary>
        public int CECO_EMPR_RUT { get; set; }
        /// <summary>
        /// COTIZACION_TOTAL
        /// </summary>
        public int COTIZACION_TOTAL { get; set; }
        /// <summary>
        /// COLE_ADICIONAL
        /// </summary>
        public int COLE_ADICIONAL { get; set; }
        /// <summary>
        /// MONTO_PACTADO_UF
        /// </summary>
        public decimal MONTO_PACTADO_UF { get; set; }
        /// <summary>
        /// MONTO_PACTADO_PESO
        /// </summary>
        public int MONTO_PACTADO_PESO { get; set; }
        /// <summary>
        /// MONTO_PACTADO_SIETE
        /// </summary>
        public int MONTO_PACTADO_SIETE { get; set; }
        /// <summary>
        /// ADICIONAL_RN
        /// </summary>
        public int ADICIONAL_RN { get; set; }
        /// <summary>
        /// MONTO_COTIZACION
        /// </summary>
        public int MONTO_COTIZACION { get; set; }
        /// <summary>
        /// PORCENT_COTIZACION_LEY_18566
        /// </summary>
        public int PORCENT_COTIZACION_LEY_18566 { get; set; }
        /// <summary>
        /// RENTA_BRUTA
        /// </summary>
        public int RENTA_BRUTA { get; set; }
        /// <summary>
        /// MONTO_A_ENTERAR
        /// </summary>
        public int MONTO_A_ENTERAR { get; set; }
    }
}
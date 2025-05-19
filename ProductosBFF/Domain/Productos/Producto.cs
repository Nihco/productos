using System;

namespace ProductosBFF.Domain.Productos
{
    /// <summary>
    /// Clase
    /// </summary>
    public class Producto
    {
       /// <summary>
        /// Codigo
        /// </summary>
        public string CODIGO { get; set; }

        /// <summary>
        /// Nombre
        /// </summary>
        public string NOMBRE { get; set; }

        /// <summary>
        /// Fecha Inicio
        /// </summary>
        public DateTime FECHA_INICIO { get; set; }

        /// <summary>
        /// Cobertura
        /// </summary>
        public Single COBERTURA { get; set; }

        /// <summary>
        /// Monto UF
        /// </summary>
        public double MONTO_UF { get; set; }

        /// <summary>
        /// Monto pesos
        /// </summary>
        public decimal MONTO_PESOS { get; set; }

        /// <summary>
        /// Tope
        /// </summary>
        public double TOPE { get; set; }

        /// <summary>
        /// Modalidad de cotizacion
        /// </summary>
        public string MODALIDAD_COTIZACION { get; set; }

        /// <summary>
        /// Incluido plan
        /// </summary>
        public string INCLUIDO_PLAN { get; set; }

        /// <summary>
        /// Disponible
        /// </summary>
        public string DISPONIBLE { get; set; }

        /// <summary>
        /// Contrato
        /// </summary>
        public string CONTRATO { get; set; }

        /// <summary>
        /// Usar desde
        /// </summary>
        public DateTime USAR_DESDE { get; set; }

        /// <summary>
        /// Codigo plan
        /// </summary>
        public string CODIGO_PLAN { get; set; }

        /// <summary>
        /// URL del contrato
        /// </summary>
        public string URL_CONTRATO { get; set; }

        /// <summary>
        /// Tope
        /// </summary>
        public decimal PLAZO_USO { get; set; }

        /// <summary>
        /// TEXTO_ETIQUETA
        /// </summary>
        public string TEXTO_ETIQUETA { get; set; }

        /// <summary>
        /// COLOR_ETIQUETA
        /// </summary>
        public string COLOR_ETIQUETA { get; set; }

        /// <summary>
        /// ICONO
        /// </summary>
        public string ICONO { get; set; }
        
        /// <summary>
        /// MULTICOTIZANTE
        /// </summary>
        public bool MULTICOTIZANTE { get; set; }
        
        /// <summary>
        /// PLAZO_VALIDO
        /// </summary>
        public decimal PLAZO_VALIDO { get; set; }

        /// <summary>
        /// TEXTO_SOLICITUD
        /// </summary>
        public string TEXTO_SOLICITUD { get; set; }

        /// <summary>
        /// FAMILIA
        /// </summary>
        public string FAMILIA { get; set; }

        /// <summary>
        /// ES_MULTICOTIZANTE
        /// </summary>
        public decimal ES_MULTICOTIZANTE { get; set; }

        /// <summary>
        /// ES_COLECTIVO 
        /// </summary>
        public decimal ES_COLECTIVO { get; set; }
        
        /// <summary>
        /// Tipo0 de activacion
        /// </summary>
        public string TIPO_ACTIVACION { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ES_VIDA_CAMARA { get; set; }
        
        /// <summary>
        /// Es Caec
        /// </summary>
        public bool ES_CAEC { get; set; }
        
        /// <summary>
        /// Fecha que termina el bc costo cero
        /// </summary>
        public DateTime FECHA_CANCELADO { get; set; }
       
        /// <summary>
        /// Fecha que termina de estar en costo 0 el BC
        /// </summary>
        public DateTime USAR_HASTA { get; set; }
    }
}
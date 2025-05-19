using System;

namespace ProductosBFF.Domain.Productos
{
    /// <summary>
    /// Clase
    /// </summary>
    public class ProductoLeyCorta
    {
        /// <summary>
        /// Folio afiliado
        /// </summary>
        public long folio_suscripcion { get; set; }

        /// <summary>
        /// codigo bc
        /// </summary>
        public string codigo { get; set; }

        /// <summary>
        /// nombre del afiliado
        /// </summary>
        public string nombre { get; set; }

        /// <summary>
        /// fecha inicio
        /// </summary>
        public DateTime fecha_inicio { get; set; }

        /// <summary>
        /// flag si incluye plan
        /// </summary>
        public string incluido_plan { get; set; }

        /// <summary>
        /// fecha usar desde producto
        /// </summary>
        public DateTime usar_desde { get; set; }

        /// <summary>
        /// fecha usar hasta producto
        /// </summary>
        public DateTime usar_hasta { get; set; }

        /// <summary>
        /// plazo de uso
        /// </summary>
        public decimal plazo_valido { get; set; }

        /// <summary>
        /// Codigo plan
        /// </summary>
        public string codigo_plan { get; set; }

        /// <summary>
        /// Plazo Uso
        /// </summary>
        public decimal plazo_uso { get; set; }

        /// <summary>
        /// Texto etiqueta
        /// </summary>
        public string texto_etiqueta { get; set; }

        /// <summary>
        /// Color etiqueta front
        /// </summary>
        public string color_etiqueta { get; set; }

        /// <summary>
        /// Icono
        /// </summary>
        public string icono { get; set; }

        /// <summary>
        /// Texto Solicitud
        /// </summary>
        public string texto_solicitud { get; set; }

        /// <summary>
        /// Familia del bc
        /// </summary>
        public string familia { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string url_resumen { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string url_condiciones { get; set; }

        /// <summary>
        /// Monto en pesos
        /// </summary>
        public decimal valueAprox { get; set; }

    }
}
using System;

namespace ProductosBFF.Domain.Productos
{
    /// <summary>
    /// ProductoCosto Cero
    /// </summary>
    public class ProductosCostoCero : Producto
    {
        /// <summary>
        /// Fecha de suscripcion
        /// </summary>
        public string fecha_suscripcion { get; set; }

        /// <summary>
        /// Fecha inicio beneficio
        /// </summary>
        public string fecha_inicio_beneficio { get; set; }

        /// <summary>
        /// fecha termino gratuidad
        /// </summary>
        public string fecha_termino_gratuidad { get; set; }

        /// <summary>
        /// fecha termino beneficio
        /// </summary>
        public string fecha_termino_beneficio { get; set; }

        /// <summary>
        /// estado bc
        /// </summary>
        public string estado { get; set; }

        /// <summary>
        /// Fecha que se cancelo el bc
        /// </summary>
        public DateTime fecha_cancelado { get; set; }

        /// <summary>
        /// duracion bc gratis
        /// </summary>
        public decimal duracion_bc_gratis { get; set; }
    }
}
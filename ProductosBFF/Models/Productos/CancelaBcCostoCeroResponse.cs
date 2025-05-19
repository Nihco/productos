using System;

namespace ProductosBFF.Models.Productos
{
    /// <summary>
    /// Respuesta del servicio cancela bc costo cero
    /// </summary>
    public class CancelaBcCostoCeroResponse
    {
        /// <summary>
        /// Codigo respuesta API
        /// </summary>
        public decimal nroSolicitud { get; set; }
        
        /// <summary>
        /// /Mensaje retorno API
        /// </summary>
        public DateTime disponibleHasta { get; set; }
        
        /// <summary>
        /// Documento en Base64
        /// </summary>
        public string documentoBase64 { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime fechaTerminoBeneficio { get; set; }
    }
}
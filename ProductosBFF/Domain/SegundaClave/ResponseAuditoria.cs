using System;

namespace ProductosBFF.Domain.SegundaClave
{
    /// <summary>
    /// 
    /// </summary>
    public class ResponseAuditoria
    {
        /// <summary>
        /// 
        /// </summary>
        public string Ip { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime FechaAutorizacion {get; set;}
        
        /// <summary>
        /// 
        /// </summary>
        public int IdOperacion { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string CodigoOperacion { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int Rut { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Dv { get; set; }
    }
}
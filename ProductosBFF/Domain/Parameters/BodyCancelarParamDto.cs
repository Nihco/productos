using System;

namespace ProductosBFF.Domain.Parameters
{
    /// <summary>
    /// 
    /// </summary>
    public class BodyCancelarParamDto:BodyCancelarBcParam
    {
        /// <summary>
        /// id_persona como Firma
        /// </summary>
        public string pin_firma { get; set; }
        
        
        /// <summary>
        /// 
        /// </summary>
        public DateTime pi_dFecha_Autor { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pi_vIp_Cliente { get; set; }
    }
}
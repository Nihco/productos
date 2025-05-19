using System;

namespace ProductosBFF.Models.BCCesantia
{
    /// <summary>
    /// Clase
    /// </summary>
    public class ResponseTrazaPadre
    {
        /// <summary>
        /// Id Solicitud
        /// </summary>
        public decimal ID_SINIESTRO { get; set; }
        
        /// <summary>
        /// Id Orden
        /// </summary>
        public decimal ID_ORDEN { get; set; }
        
        /// <summary>
        /// Folio Afil
        /// </summary>
        public decimal FOLIO_AFIL { get; set; }
        
        /// <summary>
        /// Rut Afil
        /// </summary>
        public decimal RUT_AFIL { get; set; }
        
        /// <summary>
        /// Dv Afil
        /// </summary>
        public string DV_AFIL { get; set; }
        
        /// <summary>
        /// Etapa Back
        /// </summary>
        public string ETAPA_BACK { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        public string ESTADO { get; set; }
        
        /// <summary>
        /// Color
        /// </summary>
        public string COLOR { get; set; }
        
        /// <summary>
        /// Etapa Front
        /// </summary>
        public string ETAPA_FRONT { get; set; }
        
        /// <summary>
        /// Estado
        /// </summary>
        public string STATUS { get; set; }
        
        /// <summary>
        /// Fecha ingreso registro
        /// </summary>
        public DateTime FEC_ING_REG { get; set; }
        
        /// <summary>
        /// Titulo Info
        /// </summary>
        public string TITULO_INFO { get; set; }
        
        /// <summary>
        /// Descripcion Info
        /// </summary>
        public string DESCRIPCION_INFO { get; set; }
    }
}
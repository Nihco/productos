using System.Collections.Generic;

namespace ProductosBFF.Models.BCCesantia
{
    /// <summary>
    /// Clase
    /// </summary>
    public class DtoResponseInfoPadre
    {
        /// <summary>
        /// Estado
        /// </summary>
        public string status { get; set; }
        
        /// <summary>
        /// Etapa
        /// </summary>
        public string etapa { get; set; }
        
        /// <summary>
        /// Fecha Etapa
        /// </summary>
        public string fechaEtapa { get; set; }
        
        /// <summary>
        /// Estado
        /// </summary>
        public string estado { get; set; }
        
        /// <summary>
        /// Id Estado
        /// </summary>
        public string idSiniestro { get; set; }
        
        /// <summary>
        /// Info
        /// </summary>
        public List<DtoInformacionTraza> informacion { get; set; }
    }
}
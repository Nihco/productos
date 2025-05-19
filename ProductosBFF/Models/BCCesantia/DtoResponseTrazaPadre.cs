using System.Collections.Generic;
using ProductosBFF.Mappings;

namespace ProductosBFF.Models.BCCesantia
{
    /// <summary>
    /// Clase
    /// </summary>
    public class DtoResponseTrazaPadre: IMapFrom<ResponseTrazaPadre>
    {
        /// <summary>
        /// Estado
        /// </summary>
        public string estado { get; set; }
        
        /// <summary>
        /// Color
        /// </summary>
        public string color { get; set; }
        
        /// <summary>
        /// Color
        /// </summary>
        public List<DtoResponseInfoPadre> informacionPadre { get; set; }
    }
}
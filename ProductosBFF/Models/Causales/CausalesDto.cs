using ProductosBFF.Mappings;

namespace ProductosBFF.Models.Causales
{
    /// <summary>
    /// Clase
    /// </summary>
    public class CausalesDto : IMapFrom<Domain.Causales.Causales>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id_Causales_Despido { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Causales_Despido { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Id_Tipo_Trabajador { get; set; }
    }
}
using ProductosBFF.Mappings;

namespace ProductosBFF.Models.BCCesantia
{
    /// <summary>
    /// Dtp Response Diagnostico
    /// </summary>
    public class DtoResponseDiagnostico : IMapFrom<ResponseObtenerDiagnostico>
    {
        /// <summary>
        /// Id Siniestro
        /// </summary>
        public decimal id_siniestro { get; set; }

        /// <summary>
        /// Descripcion Bc
        /// </summary>
        public string descripcion_bc { get; set; }

        /// <summary>
        /// Tipo Accidente
        /// </summary>
        public decimal tipo_accidente { get; set; }
    }
}
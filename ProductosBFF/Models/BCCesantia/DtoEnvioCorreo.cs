using ProductosBFF.Domain.Parameters;
using ProductosBFF.Mappings;

namespace ProductosBFF.Models.BCCesantia
{
    /// <summary>
    /// Clase
    /// </summary>
    /// 
    public class DtoEnvioCorreo : IMapFrom<EnvioCorreo>
    {
        /// <summary>
        /// Tipo de rechazo de la solicitud desde la NSD
        /// </summary>
        public string PinTipoRechazo { get; set; }

        /// <summary>
        /// Rut Afiliado
        /// </summary>
        public int PinRutAfil { get; set; }

        /// <summary>
        /// Digito verificador Afil
        /// </summary>
        public string PivDvAfil { get; set; }

        /// <summary>
        /// Email Afiliado
        /// </summary>
        public string PivMailAfil { get; set; }

        /// <summary>
        /// Nombres
        /// </summary>
        public string PivNombres { get; set; }
    }
}

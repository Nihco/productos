using AutoMapper;
using ProductosBFF.Mappings;

namespace ProductosBFF.Models.BCCesantia
{
    /// <summary>
    /// Clase
    /// </summary>
    public class DtoDetalleSolicitudVC :IMapFrom<ResponseDetalleSolicitudVC>
    {
        /// <summary>
        /// Diagnostico
        /// </summary>
        public string diagnostico { get; set; }
        
        /// <summary>
        /// Nombre Beneficiario
        /// </summary>
        public string beneficiario { get; set; }
        
        /// <summary>
        /// Tipo Solicitud
        /// </summary>
        public string tipoSolicitud { get; set; }
        
        /// <summary>
        /// Producto Adicional
        /// </summary>
        public string productoAdicional { get; set; }
        
        /// <summary>
        /// Fecha Solicitud
        /// </summary>
        public string fechaSolicitud { get; set; }
        
        /// <summary>
        /// Id Solicitud Original
        /// </summary>
        public decimal nroSolicitud { get; set; }
        
        /// <summary>
        /// Descripcion Tipo Solicitud
        /// </summary>
        public string montoReembolsado { get; set; }
        
        /// <summary>
        /// Color Tipo Solicitud
        /// </summary>
        public string color { get; set; }
        
        /// <summary>
        /// Id Relacionado
        /// </summary>
        public decimal IDRELACIONADO { get; set; }

        /// <summary>
        /// Mapeo
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ResponseDetalleSolicitudVC, DtoDetalleSolicitudVC>()
                .ForMember(dest => dest.beneficiario, opt => opt.MapFrom(src => src.NOMBRE_BENEFICIARIO))
                .ForMember(dest => dest.diagnostico, opt => opt.MapFrom(src => src.DIAGNOSTICO))
                .ForMember(dest => dest.productoAdicional, opt => opt.MapFrom(src => src.PRODUCTO_ADICIONAL))
                .ForMember(dest => dest.fechaSolicitud,
                    opt => opt.MapFrom(src => src.FECHA_SOLICITUD.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.tipoSolicitud, opt => opt.MapFrom(src => src.TIPO_SOLICITUD))
                .ForMember(dest => dest.montoReembolsado, opt => opt.MapFrom(src => src.DESCRIPCION_TIPO_SOLICITUD))
                .ForMember(dest => dest.color, opt => opt.MapFrom(src => src.TIPO_SOL_COLOR))
                .ForMember(dest => dest.nroSolicitud, opt => opt.MapFrom(src => src.ID_SOLICITUD_ORIGINAL));
        }
    }
}
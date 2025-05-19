using AutoMapper;
using ProductosBFF.Mappings;

namespace ProductosBFF.Models.BCCesantia
{
    /// <summary>
    /// Clase de respuesta para el historial de solicitudes
    /// </summary>
    public class DtoHistorialSolicitudes : IMapFrom<ResponseHistorialSolicitudes>
    {
        /// <summary>
        /// Tipo Solicitud
        /// </summary>
        public string tipoSolicitud { get; set; }
        
        /// <summary>
        /// BC
        /// </summary>
        public string productoAdicional { get; set; }
        
        /// <summary>
        /// Fecha de la solicitud
        /// </summary>
        public string fechaSolicitud { get; set; }
        
        /// <summary>
        /// Id de la solicitud
        /// </summary>
        public int id { get; set; }
        
        /// <summary>
        /// Estado de la solicitud
        /// </summary>
        public string estado { get; set; }
        
        /// <summary>
        /// Color que va retornar
        /// </summary>
        public string color { get; set; }
        
        /// <summary>
        /// Id Padre
        /// </summary>
        public int idRelacionado { get; set; }
        
        /// <summary>
        /// Tipo de la solicitud
        /// </summary>
        public string TipoSiniestro { get; set; }

        /// <summary>
        /// Metodo de mapeo
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ResponseHistorialSolicitudes, DtoHistorialSolicitudes>()
                .ForMember(dest => dest.tipoSolicitud, opt => opt.MapFrom(src => src.tipo_solicitud))
                .ForMember(dest => dest.productoAdicional, opt => opt.MapFrom(src => src.producto_adicional))
                .ForMember(dest => dest.fechaSolicitud,
                    opt => opt.MapFrom(src => src.fecha_solicitud.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id_Solicitud))
                .ForMember(dest => dest.estado, opt => opt.MapFrom(src => src.Descripcion_tipo_solicitud))
                .ForMember(dest => dest.color, opt => opt.MapFrom(src => src.Tipo_Sol_Color))
                .ForMember(dest => dest.idRelacionado, opt => opt.MapFrom(src => src.IdRelacionado))
                .ForMember(dest => dest.TipoSiniestro, opt => opt.MapFrom(src => src.TIPO_SINIESTRO));
        }
    }
}
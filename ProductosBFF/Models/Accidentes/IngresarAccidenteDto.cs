using AutoMapper;
using ProductosBFF.Domain.Accidentes;
using ProductosBFF.Mappings;

namespace ProductosBFF.Models.Accidentes
{
    /// <summary>
    /// class
    /// </summary>
    public class IngresarAccidenteDto : IMapFrom<IngresarAccidente>
    {
        /// <summary>
        /// CodigoError
        /// </summary>
        public decimal CodigoError { get; set; }

        /// <summary>
        /// PonIdSiniestroVc
        /// </summary>
        public decimal IdSiniestro { get; set; }

        /// <summary>
        /// Mapper
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<IngresarAccidente, IngresarAccidenteDto>()
                .ForMember(dto => dto.CodigoError, dom => dom.MapFrom(d => d.pon_error))
                .ForMember(dto => dto.IdSiniestro, dom => dom.MapFrom(d => d.pon_id_siniestro_vc));
        }
    }
}

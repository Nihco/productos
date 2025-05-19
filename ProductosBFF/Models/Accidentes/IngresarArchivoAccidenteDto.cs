using AutoMapper;
using ProductosBFF.Domain.Accidentes;
using ProductosBFF.Mappings;

namespace ProductosBFF.Models.Accidentes
{
    /// <summary>
    /// class
    /// </summary>
    public class IngresarArchivoAccidenteDto : IMapFrom<IngresarArchivoAccidente>
    {
        /// <summary>
        /// CodigoError
        /// </summary>
        public decimal CodigoError { get; set; }

        /// <summary>
        /// Mapper
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<IngresarArchivoAccidente, IngresarArchivoAccidenteDto>()
                .ForMember(dto => dto.CodigoError, dom => dom.MapFrom(d => d.pon_error));
        }
    }
}

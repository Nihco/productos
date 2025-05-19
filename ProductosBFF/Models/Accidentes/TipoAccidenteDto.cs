using AutoMapper;
using ProductosBFF.Domain.Accidentes;
using ProductosBFF.Mappings;

namespace ProductosBFF.Models.Accidentes
{
    /// <summary>
    /// Clase
    /// </summary>
    public class TipoAccidenteDto : IMapFrom<TipoAccidente>
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public string Descripcion { get; set; }

        /// <summary>
        /// Mapper
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<TipoAccidente, TipoAccidenteDto>()
                .ForMember(dto => dto.Id, dom => dom.MapFrom(d => d.ID))
                .ForMember(dto => dto.Descripcion, dom => dom.MapFrom(d => d.DESCRIPCION));
        }
    }
}

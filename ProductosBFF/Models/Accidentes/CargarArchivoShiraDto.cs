using AutoMapper;
using ProductosBFF.Domain.Accidentes;
using ProductosBFF.Mappings;

namespace ProductosBFF.Models.Accidentes
{
    /// <summary>
    /// CargarArchivoShiraDto
    /// </summary>
    public class CargarArchivoShiraDto : IMapFrom<CargarArchivoShira>
    {
        /// <summary>
        /// Resultado carga a Shira
        /// </summary>
        public string ResultadoCargaShira { get; set; }

        /// <summary>
        /// EstadoCarga
        /// </summary>
        public bool EstadoCarga { get; set; }

        /// <summary>
        /// Mapper
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CargarArchivoShira, CargarArchivoShiraDto>()
                .ForMember(dto => dto.ResultadoCargaShira, dom => dom.MapFrom(d => d.ResultadoCargaShira))
                .ForMember(dto => dto.EstadoCarga, dom => dom.MapFrom(d => d.EstadoCarga));
        }
    }
}

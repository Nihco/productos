using System.Collections.Generic;
using AutoMapper;
using ProductosBFF.Domain.Productos;
using ProductosBFF.Mappings;

namespace ProductosBFF.Models.Productos
{
    /// <summary>
    /// Clase
    /// </summary>
    public class PrestadoresDto : IMapFrom<PrestadoresLeyCorta>
    {
       
        /// <summary>
        /// Lisa Clase
        /// </summary>
        public List<PrestadorDto> ListaClase { get; set; }


        /// <summary>
        /// Mapper
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<PrestadoresLeyCorta, PrestadoresDto>()
                .ForMember(dto => dto.ListaClase, dom => dom.MapFrom(d => d.ListaClase));

            profile.CreateMap<Prestador, PrestadorDto>()
                .ForMember(dto => dto.codigo_bc, dom => dom.MapFrom(d => int.Parse(d.CODIGO_BC)))
                .ForMember(dto => dto.nombre, dom => dom.MapFrom(d => d.NOMBRE));
        }
    }
}
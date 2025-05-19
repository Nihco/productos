using AutoMapper;
using ProductosBFF.Domain.Accidentes;
using ProductosBFF.Mappings;
using System.Collections.Generic;
using System.Linq;
using ReverseMarkdown;

namespace ProductosBFF.Models.Accidentes
{
    /// <summary>
    /// Clase
    /// </summary>
    public class DocumentosAccidenteDto : IMapFrom<DocumentosAccidente>
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nombre
        /// </summary>
        public string Nombre { get; set; }
        /// <summary>
        /// Glosa
        /// </summary>
        public string Glosa { get; set; }

        /// <summary>
        /// Obligatorio
        /// </summary>
        public string Obligatorio { get; set; }

        /// <summary>
        /// CheckBox
        /// </summary>
        public string CheckBox { get; set; }

        /// <summary>
        /// Name 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// TituloModal
        /// </summary>
        public string TituloModal { get; set; }
        /// <summary>
        /// SubtituloModal
        /// </summary>
        public string SubtituloModal { get; set; }
        /// <summary>
        /// Lista
        /// </summary>
        public List<string> Lista { get; set; }
        /// <summary>
        /// Imagen 
        /// </summary>
        public string Imagen { get; set; }
        /// <summary>
        /// Formulario
        /// </summary>
        public decimal Formulario { get; set; }
        /// <summary>
        /// Texto alerta modal
        /// </summary>
        public string TextoAlertaModal { get; set; }

        /// <summary>
        /// Mapper
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            Converter converter = new Converter();

            profile.CreateMap<DocumentosAccidente, DocumentosAccidenteDto>()
                .ForMember(dto => dto.Id, dom => dom.MapFrom(d => d.ID))
                .ForMember(dto => dto.Nombre, dom => dom.MapFrom(d => d.NOMBRE))
                .ForMember(dto => dto.Glosa, dom => dom.MapFrom(d => converter.Convert(d.GLOSA)))
                .ForMember(dto => dto.Obligatorio, dom => dom.MapFrom(d => d.OBLIGATORIO))
                .ForMember(dto => dto.CheckBox, dom => dom.MapFrom(d => d.CHECKBOX))
                .ForMember(dto => dto.Name, dom => dom.MapFrom(d => d.NAME))
                .ForMember(dto => dto.TituloModal, dom => dom.MapFrom(d => d.TITULO_MODAL))
                .ForMember(dto => dto.SubtituloModal, dom => dom.MapFrom(d => d.SUBTITULO_MODAL))
                .ForMember(dto => dto.Imagen, dom => dom.MapFrom(d => d.IMAGEN))
                .ForMember(dto => dto.Formulario, dom => dom.MapFrom(d => d.FORMULARIO))
                .ForMember(dto => dto.TextoAlertaModal, dom => dom.MapFrom(d => d.TextoAlertaModal))
                .ForMember(dto => dto.Lista, m => m.MapFrom(src => src.LISTA.Split(";", System.StringSplitOptions.None).ToList()));


        }
    }
}

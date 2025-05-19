using AutoMapper;
using ProductosBFF.Domain.Productos;
using ProductosBFF.Mappings;
using System.Globalization;

namespace ProductosBFF.Models.Productos
{
    /// <summary>
    /// Clase
    /// </summary>
    public class ProductLeyCortaDto : IMapFrom<Producto>
    {
        /// <summary>
        /// Folio afiliado
        /// </summary>
        public long folio_suscripcion { get; set; }

        /// <summary>
        /// codigo bc
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// nombre del bc
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// fecha inicio
        /// </summary>
        public string fecha_inicio { get; set; }

        /// <summary>
        /// flag si incluye plan
        /// </summary>
        public string incluido_plan { get; set; }

        /// <summary>
        /// fecha usar desde producto
        /// </summary>
        public string usar_desde { get; set; }
        
        /// <summary>
        /// fecha usar hasta producto
        /// </summary>
        public string usar_hasta { get; set; }

        /// <summary>
        /// plazo de uso
        /// </summary>
        public decimal plazo_valido { get; set; }

        /// <summary>
        /// Codigo plan
        /// </summary>
        public string codigo_plan { get; set; }

        /// <summary>
        /// Plazo Uso
        /// </summary>
        public decimal plazo_uso { get; set; }

        /// <summary>
        /// Texto etiqueta
        /// </summary>
        public string texto_etiqueta { get; set; }

        /// <summary>
        /// Color etiqueta front
        /// </summary>
        public string color_etiqueta { get; set; }

        /// <summary>
        /// Icono
        /// </summary>
        public string icono { get; set; }

        /// <summary>
        /// Texto Solicitud
        /// </summary>
        public string texto_solicitud { get; set; }

        /// <summary>
        /// Familia del bc
        /// </summary>
        public string familia { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string url_resumen { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string url_condiciones { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string valueAprox { get; set; }


        /// <summary>
        /// Mapper
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            CultureInfo culture = new("es-ES");

            profile.CreateMap<ProductoLeyCorta, ProductLeyCortaDto>()
                .ForMember(dto => dto.folio_suscripcion, dom => dom.MapFrom(d => d.folio_suscripcion))
                .ForMember(dto => dto.id, dom => dom.MapFrom(d => d.codigo))
                .ForMember(dto => dto.name, dom => dom.MapFrom(d => d.nombre))
                .ForMember(dto => dto.fecha_inicio, opt => opt.MapFrom(src => src.fecha_inicio.ToString("dd/MM/yyyy", culture)))
                .ForMember(dto => dto.incluido_plan, dom => dom.MapFrom(d => d.incluido_plan))
                .ForMember(dto => dto.usar_desde, opt => opt.MapFrom(src => src.usar_desde.ToString("dd/MM/yyyy", culture)))
                .ForMember(dto => dto.usar_hasta, opt => opt.MapFrom(src => src.usar_hasta.ToString("dd/MM/yyyy", culture)))
                .ForMember(dto => dto.plazo_valido, dom => dom.MapFrom(d => d.plazo_valido))
                .ForMember(dto => dto.codigo_plan, dom => dom.MapFrom(d => d.codigo_plan))
                .ForMember(dto => dto.plazo_uso, dom => dom.MapFrom(d => d.plazo_uso))
                .ForMember(dto => dto.texto_etiqueta, dom => dom.MapFrom(d => d.texto_etiqueta))
                .ForMember(dto => dto.color_etiqueta, dom => dom.MapFrom(d => d.color_etiqueta))
                .ForMember(dto => dto.icono, dom => dom.MapFrom(d => d.icono))
                .ForMember(dto => dto.texto_solicitud, dom => dom.MapFrom(d => d.texto_solicitud))
                .ForMember(dto => dto.familia, dom => dom.MapFrom(d => d.familia))
                .ForMember(dto => dto.url_resumen, dom => dom.MapFrom(d => d.url_resumen))
                .ForMember(dto => dto.url_condiciones, dom => dom.MapFrom(d => d.url_condiciones))
                .ForMember(dto => dto.valueAprox,
                    dom => dom.MapFrom(d =>
                        $"${0.ToString("#,##0", new NumberFormatInfo { NumberDecimalSeparator = ",", NumberGroupSeparator = "." })}"))
                ;
        }
    }
}
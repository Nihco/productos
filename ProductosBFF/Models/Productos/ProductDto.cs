using System;
using AutoMapper;
using ProductosBFF.Domain.Productos;
using ProductosBFF.Mappings;
using ProductosBFF.Utils;
using System.Globalization;

namespace ProductosBFF.Models.Productos
{
    /// <summary>
    /// Clase
    /// </summary>
    public class ProductDto : IMapFrom<Producto>
    {
        /// <summary>
        /// Id
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Nombre
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Monto
        /// </summary>
        public string amountUf { get; set; }

        /// <summary>
        /// Valor
        /// </summary>
        public string valueAprox { get; set; }

        /// <summary>
        /// Fecha de Contrato de Plan
        /// </summary>
        public string fecha_inicio { get; set; }

        /// <summary>
        /// Fecha disponibilidad del plan
        /// </summary>
        public string usar_desde { get; set; }

        /// <summary>
        /// Codigo compuesto del Plan
        /// </summary>
        public string codigo_plan { get; set; }

        /// <summary>
        /// URL de contrato 
        /// </summary>
        public string url_contrato { get; set; }

        /// <summary>
        /// mese de carencia
        /// </summary>
        public string plazo_uso { get; set; }


        /// <summary>
        /// Texto de la etiqueta
        /// </summary>
        public string texto_etiqueta { get; set; }

        /// <summary>
        /// Color de la etiqueta
        /// </summary>
        public string color_etiqueta { get; set; }

        /// <summary>
        /// ICONO
        /// </summary>
        public string icono { get; set; }

        /// <summary>
        /// plazo valido
        /// </summary>
        public bool plazo_valido { get; set; }

        /// <summary>
        /// texto solicitud
        /// </summary>
        public string texto_solicitud { get; set; }

        /// <summary>
        /// familia
        /// </summary>
        public string familia { get; set; }

        /// <summary>
        /// es_multicotizante
        /// </summary>
        public bool es_multicotizante { get; set; }

        /// <summary>
        /// es_colectivo
        /// </summary>
        public bool es_colectivo { get; set; }

        /// <summary>
        /// tipo de activacion
        /// </summary>
        public string tipoActivacion { get; set; }

        /// <summary>
        /// Es Cesantia
        /// </summary>
        public bool es_cesantia { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool es_urgencia { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public bool es_vida_camara { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public bool es_caec { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool es_familia_protegida { get; set; }

        /// <summary>
        /// Fecha que mostrara la franja cuando el sistema esté en mantencion
        /// </summary>
        public bool FlagMantencion { get; set; }

        // Campos Costo Cero
        /// <summary>
        /// Campo que indica si es costo Cero
        /// </summary>
        public bool EsCostoCero { get; set; }

        /// <summary>
        /// Fecha cuando se cancela el BC
        /// </summary>
        public DateTime? FechaCancelado { get; set; }

        /// <summary>
        /// Fecha Hasta cuando esta con costo cero
        /// </summary>
        public DateTime? DisponibleHasta { get; set; }

        /// <summary>
        /// Duracion de dias que esta con costo cero
        /// </summary>
        public int DuracionBCGratis { get; set; }

        /// <summary>
        /// Indica si se esta cobrando el bc
        /// </summary>
        public bool SeEstaCobrandoBc { get; set; }

        /// <summary>
        /// Indica si se puede cancelar el BC
        /// </summary>
        public bool SePuedeCancelar { get; set; }


        /// <summary>
        /// Mapper
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            CultureInfo culture = new("es-ES");

            profile.CreateMap<Producto, ProductDto>()
                .ForMember(dto => dto.id, dom => dom.MapFrom(d => d.CODIGO))
                .ForMember(dto => dto.name, dom => dom.MapFrom(d => StringMethods.Decode(d.NOMBRE.Replace("Â", ""))))
                .ForMember(dto => dto.amountUf,
                    dom => dom.MapFrom(d =>
                        $"UF {d.MONTO_UF.ToString("#,##0.000", new NumberFormatInfo { NumberDecimalSeparator = ",", NumberGroupSeparator = "." })}/mes"))
                .ForMember(dto => dto.valueAprox,
                    dom => dom.MapFrom(d =>
                        $"${d.MONTO_PESOS.ToString("#,##0", new NumberFormatInfo { NumberDecimalSeparator = ",", NumberGroupSeparator = "." })}"))
                .ForMember(dto => dto.usar_desde,
                    opt => opt.MapFrom(src => src.USAR_DESDE.ToString("dd/MM/yyyy", culture)))
                .ForMember(dto => dto.fecha_inicio,
                    opt => opt.MapFrom(src => src.FECHA_INICIO.ToString("dd/MM/yyyy", culture)))
                .ForMember(dto => dto.plazo_valido, opt => opt.MapFrom(src => src.PLAZO_VALIDO == 1))
                .ForMember(dto => dto.es_multicotizante, opt => opt.MapFrom(src => src.ES_MULTICOTIZANTE == 1))
                .ForMember(dto => dto.es_colectivo, opt => opt.MapFrom(src => src.ES_COLECTIVO == 1))
                .ForMember(dto => dto.tipoActivacion, opt => opt.MapFrom(src => src.TIPO_ACTIVACION))
                .ForMember(dto => dto.es_cesantia,
                    opt => opt.MapFrom(src => src.TIPO_ACTIVACION == Enums.EnumTipoActivacion.CESANTIA.ToString()))
                .ForMember(dto => dto.es_vida_camara,
                    opt => opt.MapFrom(src => src.TIPO_ACTIVACION == Enums.EnumTipoActivacion.VIDA_CAMARA.ToString()))
                .ForMember(dto => dto.es_urgencia,
                    opt => opt.MapFrom(src => src.TIPO_ACTIVACION == Enums.EnumTipoActivacion.URGENCIA.ToString()))
                .ForMember(dto => dto.es_familia_protegida,
                    opt => opt.MapFrom(src =>
                        src.TIPO_ACTIVACION == Enums.EnumTipoActivacion.FAMILIA_PROTEGIDA.ToString()))
                .ForMember(dto => dto.es_caec,
                    opt => opt.MapFrom(src =>
                        src.TIPO_ACTIVACION == Enums.EnumTipoActivacion.CAEC.ToString()))
                .ForMember(dto => dto.EsCostoCero, opt => opt.Ignore())
                .ForMember(dto => dto.FechaCancelado, opt => opt.Ignore())
                .ForMember(dto => dto.DisponibleHasta, opt => opt.MapFrom(src => src.USAR_HASTA))
                .ForMember(dto => dto.DuracionBCGratis, opt => opt.Ignore())
                .ForMember(dto => dto.SeEstaCobrandoBc, opt => opt.Ignore())
                .ForMember(dto => dto.SePuedeCancelar,
                    opt => opt.Ignore());

            profile.CreateMap<ProductosCostoCero, ProductDto>()
                .ForMember(dto => dto.id, dom => dom.MapFrom(src => src.CODIGO))
                .ForMember(dto => dto.name, dom => dom.MapFrom(src => src.NOMBRE))
                .ForMember(dto => dto.usar_desde, dom => dom.MapFrom(src => src.FECHA_INICIO))
                .ForMember(dto => dto.fecha_inicio, dom => dom.MapFrom(src => src.fecha_suscripcion))
                .ForMember(dto => dto.DisponibleHasta, dom => dom.MapFrom(src =>
                    ParseDate(src.fecha_termino_beneficio) ?? ParseDate(src.fecha_termino_gratuidad)
                ))
                .ForMember(dto => dto.amountUf, dom => dom.MapFrom(d =>
                    $"UF {d.MONTO_UF.ToString("#,##0.000", new NumberFormatInfo { NumberDecimalSeparator = ",", NumberGroupSeparator = "." })}/mes"))
                .ForMember(dto => dto.valueAprox,
                    dom => dom.MapFrom(d =>
                        $"${d.MONTO_PESOS.ToString("#,##0", new NumberFormatInfo { NumberDecimalSeparator = ",", NumberGroupSeparator = "." })}"))
                .ForMember(dto => dto.FechaCancelado,
                    dom => dom.MapFrom(src =>
                        src.fecha_cancelado == DateTime.MinValue ? (DateTime?)null : src.fecha_cancelado))
                .ForMember(dto => dto.tipoActivacion, opt => opt.Ignore())
                .ForMember(dto => dto.EsCostoCero, opt => opt.MapFrom(src => true))
                .ForMember(dto => dto.DuracionBCGratis,
                    opt => opt.MapFrom(src => src.duracion_bc_gratis))
                .ForMember(dto => dto.SeEstaCobrandoBc,
                    opt => opt.MapFrom(src => src.fecha_cancelado == DateTime.MinValue))
            .ForMember(dto => dto.SePuedeCancelar,
                opt => opt.MapFrom(src => src.estado.Equals("Vigente")));
        }

        private static DateTime? ParseDate(string dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString) || dateString == "0001-01-01T00:00:00")
                return null;

            try
            {
                return DateTime.ParseExact(dateString.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                return null;
            }
        }
    }
}
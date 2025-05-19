using System;
using AutoMapper;
using ProductosBFF.Domain.Parameters;
using ProductosBFF.Mappings;

namespace ProductosBFF.Models.BCCesantia
{
    /// <summary>
    /// Clase Doc
    /// </summary>
    public class DtoDocCm : IMapFrom<BodySolicitudActivacion>
    {
        /// <summary>
        /// Id Sistema
        /// </summary>
        public string IdSistema { get; set; }

        /// <summary>
        /// Id Folio
        /// </summary>
        public string IdFolio { get; set; }

        /// <summary>
        /// Imagen
        /// </summary>
        public string Imagen { get; set; }

        /// <summary>
        /// Depto 
        /// </summary>
        public string Depto { get; set; }

        /// <summary>
        /// Apellido Paterno
        /// </summary>
        public string ApPat { get; set; }

        /// <summary>
        /// Apellido Materno
        /// </summary>
        public string ApMat { get; set; }

        /// <summary>
        /// Nombres
        /// </summary>
        public string Nombres { get; set; }

        /// <summary>
        /// Rut
        /// </summary>
        public string Rut { get; set; }

        /// <summary>
        /// Dig
        /// </summary>
        public string Dig { get; set; }

        /// <summary>
        /// Fecha  
        /// </summary>
        public string FecVisa { get; set; }

        /// <summary>
        /// Codigo Usuario 
        /// </summary>
        public string CodUsuario { get; set; }

        /// <summary>
        /// Codigo Area 
        /// </summary>
        public string CodArea { get; set; }

        /// <summary>
        /// Codigo Sistema  
        /// </summary>
        public string CodSistMat { get; set; }

        /// <summary>
        /// Codigo Tipo
        /// </summary>
        public string CodTipo { get; set; }

        /// <summary>
        /// Etapa 
        /// </summary>
        public string Etapa { get; set; }

        /// <summary>
        /// String Codigo Accion 
        /// </summary>
        public string StrCodAccion { get; set; }

        /// <summary>
        /// Archivo Base 64 
        /// </summary>
        public string FileBase64 { get; set; }

        /// <summary>
        /// Mapeo de clase
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<BodySolicitudActivacion, DtoDocCm>()
                .ForMember(dest => dest.IdSistema, opt => opt.MapFrom(src => "725"))
                .ForMember(dest => dest.Depto, opt => opt.MapFrom(src => "Backoffice"))
                .ForMember(dest => dest.CodArea, opt => opt.MapFrom(src => "BENCO"))
                .ForMember(dest => dest.Etapa, opt => opt.MapFrom(src => "ETA1"))
                .ForMember(dest => dest.CodSistMat, opt => opt.MapFrom(src => "G02-BENE"))
                .ForMember(dest => dest.CodUsuario, opt => opt.MapFrom(src => "SINIESTRO.NSD"))
                .ForMember(dest => dest.CodTipo, opt => opt.MapFrom(src => "S"))
                .ForMember(dest => dest.StrCodAccion, opt => opt.MapFrom(src => "INGDOCTO"))
                .ForMember(dest => dest.IdFolio, opt => opt.MapFrom(src => src.FolioAfil.ToString()))
                .ForMember(dest => dest.Imagen, opt => opt.MapFrom(src => src.Imagen))
                .ForMember(dest => dest.ApMat, opt => opt.MapFrom(src =>src.Apellidos.Split(new char[]{' '},2)[1]))
                .ForMember(dest => dest.ApPat, opt => opt.MapFrom(src =>src.Apellidos.Split(new char[]{' '},2)[0]))
                .ForMember(dest => dest.Nombres, opt => opt.MapFrom(src => src.Nombres))
                .ForMember(dest => dest.Rut, opt => opt.MapFrom(src => src.RutAfil.ToString()))
                .ForMember(dest => dest.Dig, opt => opt.MapFrom(src => src.DvAfil.ToString()))
                .ForMember(dest => dest.FecVisa, opt => opt.MapFrom(src => DateTime.Today.ToString("dd-MM-yyyy")))
                .ForMember(dest => dest.FileBase64, opt => opt.MapFrom(src => src.FileBase64));
        }
    }
}
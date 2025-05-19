using AutoMapper;
using ProductosBFF.Domain.Productos;
using ProductosBFF.Mappings;

namespace ProductosBFF.Models.Accidentes
{
    /// <summary>
    /// Clase
    /// </summary>
    public class CargarArchivoContentDto : IMapFrom<DocCesantia>
    {
        /// <summary>
        /// Id Content recibido de Content Manager
        /// </summary>
        public string Idcontent { get; set; }      

        /// <summary>
        /// Mensaje de error en caso de fallar
        /// </summary>
        public string MensajeError { get; set; }

        /// <summary>
        /// Mapper
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<DocCesantia, CargarArchivoContentDto>()
                .ForMember(dto => dto.Idcontent, dom => dom.MapFrom(d => d.Result.Idcontent))
                .ForMember(dto => dto.MensajeError, dom => dom.MapFrom(d => d.Result.MensajeError));
        }
    }
}

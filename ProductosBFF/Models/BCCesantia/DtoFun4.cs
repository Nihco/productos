using AutoMapper;
using ProductosBFF.Domain.Parameters;
using ProductosBFF.Mappings;
using System;
using System.Net;
using System.Net.Sockets;

namespace ProductosBFF.Models.BCCesantia
{
    /// <summary>
    /// Clase Fun 4
    /// </summary>
    public class DtoFun4 : IMapFrom<BodySolicitudActivacion>
    {
        /// <summary>
        /// Folio Suscripcion del usuario
        /// </summary>
        public int Folio { get; set; }

        /// <summary>
        /// Token de sesion del usuario
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Id de usuario
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Fecha de Autorizacion del usuario 
        /// </summary>
        public DateTime Fecha { get; set; }

        /// <summary>
        /// Ip de cliente
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// Mapeo
        /// </summary>
        /// <param name="profile"></param>
        public void Mapping(Profile profile)
        {
            profile.CreateMap<BodySolicitudActivacion, DtoFun4>()
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => DateTime.Today))
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token))
                .ForMember(dest => dest.Ip, opt => opt.MapFrom(src => GetServerIp()))
                .ForMember(dest => dest.Folio, opt => opt.MapFrom(src => src.FolioAfil))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }

        /// <summary>
        /// Metodo que trae la ip del servidor
        /// </summary>
        /// <returns></returns>
        private static string GetServerIp()
        {
            string hostName = Dns.GetHostName();
            IPAddress[] addresses = Dns.GetHostEntry(hostName).AddressList;
            IPAddress ip = Array.Find(addresses, x => x.AddressFamily == AddressFamily.InterNetwork);
            return ip.ToString();
        }
    }
}

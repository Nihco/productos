using System.Diagnostics.CodeAnalysis;

namespace ProductosBFF.Domain.Parameters
{

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class IngresoUniversal
    {
        /// <summary>
        /// TipoSolicitud
        /// </summary>
        public decimal TipoSolicitud { get; set; }
        /// <summary>
        /// RutAfil
        /// </summary>
        public decimal RutAfil { get; set; }
        /// <summary>
        /// FolioAfil
        /// </summary>
        public decimal FolioAfil { get; set; }
        /// <summary>
        /// DvAfil
        /// </summary>
        public string DvAfil { get; set; }
        /// <summary>
        /// Nombres
        /// </summary>
        public string Nombres { get; set; }
        /// <summary>
        /// Apellidos
        /// </summary>
        public string Apellidos { get; set; }
        /// <summary>
        /// MailAfil
        /// </summary>
        public string MailAfil { get; set; }
        /// <summary>
        /// FonoAfil
        /// </summary>
        public decimal FonoAfil { get; set; }
        /// <summary>
        /// NumBcAfil
        /// </summary>
        public decimal NumBcAfil { get; set; }
        /// <summary>
        /// Diagnostico
        /// </summary>
        public string Diagnostico { get; set; }
        /// <summary>
        /// DescripcionBc
        /// </summary>
        public string DescripcionBc { get; set; }
        /// <summary>
        /// Detalle
        /// </summary>
        public string Detalle { get; set; }
        /// <summary>
        /// EsExtencion
        /// </summary>
        public string EsExtencion { get; set; }
        /// <summary>
        /// IdSinRelacionado
        /// </summary>
        public decimal IdSinRelacionado { get; set; }
        /// <summary>
        /// TipoTrabajador
        /// </summary>
        public string TipoTrabajador { get; set; }
        /// <summary>
        /// CausalDespido
        /// </summary>
        public string CausalDespido { get; set; }
        /// <summary>
        /// FechaFiniquito
        /// </summary>
        public string FechaFiniquito { get; set; }
        /// <summary>
        /// FechaDespido
        /// </summary>
        public string FechaDespido { get; set; }
        /// <summary>
        /// RutEmpleador
        /// </summary>
        public decimal RutEmpleador { get; set; }
        /// <summary>
        /// DvEmpleador
        /// </summary>
        public string DvEmpleador { get; set; }
        /// <summary>
        /// FechaInicioVigencia
        /// </summary>
        public string FechaInicioVigencia { get; set; }

        /// <summary>
        /// NombreBenef
        /// </summary>
        public string NombreBenef { get; set; }

        /// <summary>
        /// RutBeneficiario
        /// </summary>
        public decimal RutBeneficiario { get; set; }
        /// <summary>
        /// DvBeneficiario
        /// </summary>
        public string DvBeneficiario { get; set; }

        /// <summary>
        /// TipoAccidente
        /// </summary>
        public decimal TipoAccidente { get; set; }
        /// <summary>
        /// FechaAccidente
        /// </summary>
        public string FechaAccidente { get; set; }
        /// <summary>
        /// HoraAccidente
        /// </summary>
        public string HoraAccidente { get; set; }
        /// <summary>
        /// LugarAccidente
        /// </summary>
        public string LugarAccidente { get; set; }
        /// <summary>
        /// CausaAccidente
        /// </summary>
        public decimal CausaAccidente { get; set; }
        /// <summary>
        /// DescripcionAccidente
        /// </summary>
        public string DescripcionAccidente { get; set; }
        /// <summary>
        /// TipoAtencion
        /// </summary>
        public decimal TipoAtencion { get; set; }
        /// <summary>
        /// FueUrgencia
        /// </summary>
        public string FueUrgencia { get; set; }
        /// <summary>
        /// HuboInterv
        /// </summary>
        public string HuboInterv { get; set; }
        /// <summary>   
        /// OtroSeguro
        /// </summary>
        public string OtroSeguro { get; set; }
        /// <summary>
        /// MontoTotal
        /// </summary>
        public string MontoTotal { get; set; }

        /// <summary>
        /// CodFUN4 
        /// </summary>
        public decimal CodFUN4 { get; set; }
    }
}
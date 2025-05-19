using System;
using ProductosBFF.Domain.Parameters;
using ProductosBFF.Mappings;

namespace ProductosBFF.Models.BCCesantia
{
    /// <summary>
    /// Clase
    /// </summary>
    public class DtoAfil : IMapFrom<BodySolicitudActivacion>

    {
        /// <summary>
        /// Rut Afiliado
        /// </summary>
        public int PinRutAfil { get; set; }

        /// <summary>
        /// Folio Suscripcion Afil
        /// </summary>
        public int PinFolioAfil { get; set; }

        /// <summary>
        /// Digito verificador Afil
        /// </summary>
        public string PivDvAfil { get; set; }

        /// <summary>
        /// Nombres
        /// </summary>
        public string PivNombres { get; set; }

        /// <summary>
        /// Apellidos
        /// </summary>
        public string PivApellidos { get; set; }

        /// <summary>
        /// Email Afiliado
        /// </summary>
        public string PivMailAfil { get; set; }

        /// <summary>
        /// Fono Afiliado
        /// </summary>
        public int PinFonoAfil { get; set; }

        /// <summary>
        /// Numero BC
        /// </summary>
        public int PinNumBcAfil { get; set; }

        /// <summary>
        /// Descripcion BC
        /// </summary>
        public string PivDescripcionBc { get; set; }

        /// <summary>
        /// Fecha Vigencia Bc
        /// </summary>
        public DateTime PivFechaInicioVigencia { get; set; }

        /// <summary>
        /// Fecha Despido
        /// </summary>
        public DateTime PivFechaDespido { get; set; }

        /// <summary>
        /// Fecha Finiquito
        /// </summary>
        public DateTime PivFechaFiniquito { get; set; }

        /// <summary>
        /// Causal Despido
        /// </summary>
        public string PivCausalDespido { get; set; }

        /// <summary>
        /// Tipo Trabajador
        /// </summary>
        public string PivTipoTrabajador { get; set; }

        /// <summary>
        /// ID Parametro Documento
        /// </summary>
        public int PinIdParametro { get; set; }

        /// <summary>
        /// Tipo Familia
        /// </summary>
        public string PivTipoFamilia { get; set; }


        /// <summary>
        /// Codigo Fun 4
        /// </summary>
        public int PinCodFun4 { get; set; }
    }
}
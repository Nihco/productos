using System;

namespace ProductosBFF.Domain.Parameters
{
    /// <summary>
    /// Clase que toma los parametros 
    /// </summary>
    public class BodySolicitudActivacion
    {
        /// <summary>
        /// Rut Afiliado
        /// </summary>
        public int RutAfil { get; set; }

        /// <summary>
        /// Folio Suscripcion Afil
        /// </summary>
        public int FolioAfil { get; set; }

        /// <summary>
        /// Digito verificador Afil
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
        /// Email Afiliado
        /// </summary>
        public string MailAfil { get; set; }

        /// <summary>
        /// Fono Afiliado
        /// </summary>
        public int FonoAfil { get; set; }

        /// <summary>
        /// Numero BC
        /// </summary>
        public int NumBcAfil { get; set; }

        /// <summary>
        /// Descripcion BC
        /// </summary>
        public string DescripcionBc { get; set; }

        /// <summary>
        /// Fecha Vigencia Bc
        /// </summary>
        public DateTime FechaInicioVigencia { get; set; }

        /// <summary>
        /// Fecha Despido
        /// </summary>
        public DateTime FechaDespido { get; set; }

        /// <summary>
        /// Fecha Finiquito
        /// </summary>
        public DateTime FechaFiniquito { get; set; }

        /// <summary>
        /// Causal Despido
        /// </summary>
        public string CausalDespido { get; set; }

        /// <summary>
        /// Tipo Trabajador
        /// </summary>
        public string TipoTrabajador { get; set; }

        /// <summary>
        /// Tipo Familia
        /// </summary>
        public string TipoFamilia { get; set; }

        /// <summary>
        /// Imagen
        /// </summary>
        public string Imagen { get; set; }

        /// <summary>
        /// Archivo Base 64 
        /// </summary>
        public string FileBase64 { get; set; }

        /// <summary>
        /// Token de sesion del usuario
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// ID Usuario
        /// </summary>
        public string Id { get; set; }
    }
}
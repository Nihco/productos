namespace ProductosBFF.Domain.Parameters
{
    /// <summary>
    /// Clase
    /// </summary>
    public class BodyCargarArchivoContent
    {
        /// <summary>
        /// ID Sistema
        /// </summary>
        public string IdSistema { get; set; }
        /// <summary>
        ///  Id Tipo Documento
        /// </summary>
        public string IdTipoDocto_CM { get; set; }
        /// <summary>
        /// Folio
        /// </summary>
        public string IdFolio { get; set; }
        /// <summary>
        /// Codigo contenido
        /// </summary>
        public string CodContent { get; set; }
        /// <summary>
        /// imagen
        /// </summary>
        public string Imagen { get; set; }
        /// <summary>
        /// Departamento
        /// </summary>
        public string Depto { get; set; }
        /// <summary>
        /// Apellido paterno
        /// </summary>
        public string ApPat { get; set; }
        /// <summary>
        /// Apellido materno
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
        /// Digito Verificador
        /// </summary>
        public string Dig { get; set; }
        /// <summary>
        /// Fecha Visa
        /// </summary>
        public string FecVisa { get; set; }
        /// <summary>
        /// Codigo Ususario
        /// </summary>
        public string CodUsuario { get; set; }
        /// <summary>
        /// Cod Area
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
        /// Codigo Accion
        /// </summary>
        public string StrCodAccion { get; set; }
        /// <summary>
        /// Archivo
        /// </summary>
        public string FileBase64 { get; set; }
    }
}

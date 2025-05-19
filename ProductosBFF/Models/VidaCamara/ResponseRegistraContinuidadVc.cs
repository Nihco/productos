namespace ProductosBFF.Models.VidaCamara
{

    /// <summary>
    /// Clase
    /// </summary>
    public class ResponseRegistraContinuidadVc
    {
        /// <summary>
        /// Siniestro Generado
        /// </summary>
        public decimal pou_id_siniestroNEW { get; set; }

        /// <summary>
        /// Codigo estado
        /// </summary>
        public decimal pou_cod_estado { get; set; }

        /// <summary>
        /// Descripcion error
        /// </summary>
        public string pou_desc_err { get; set; }
    }
}
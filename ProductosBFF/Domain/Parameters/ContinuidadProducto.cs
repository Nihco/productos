namespace ProductosBFF.Domain.Parameters
{
    /// <summary>
    /// Representa rut para consultar su continuidad del producto
    /// </summary>
    public class ContinuidadProducto
    {
        /// <summary>
        /// correlativo unico del producto del pack
        /// </summary>
        public int rut { get; set; }
    }
}

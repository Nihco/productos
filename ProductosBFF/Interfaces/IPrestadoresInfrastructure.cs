using ProductosBFF.Domain.Parameters;
using ProductosBFF.Domain.Productos;
using System.Threading.Tasks;

namespace ProductosBFF.Interfaces
{
    /// <summary>
    /// Interfaz
    /// </summary>
    public interface IPrestadoresInfrastructure
    {
        /// <summary>
        /// GetPrestadoresAsync
        /// </summary>
        /// <param name="bc"></param>
        /// <returns></returns>
        Task<PrestadoresLeyCorta> GetPrestadoresAsync(BodyPrestadores bc);
    }
}
using ProductosBFF.Domain.Parameters;
using ProductosBFF.Models.Productos;
using System.Threading.Tasks;

namespace ProductosBFF.Interfaces
{
    /// <summary>
    /// Interface
    /// </summary>
    public interface IPrestadoresService
    {
        /// <summary>
        /// GetPrestadores
        /// </summary>
        /// <param name="bc"></param>
        /// <returns></returns>
        public Task<PrestadoresDto> GetPrestadores(BodyPrestadores bc);

    }
}
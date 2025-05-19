using ProductosBFF.Domain.Parameters;
using ProductosBFF.Domain.Universal;
using System.Threading.Tasks;

namespace ProductosBFF.Interfaces.Universal
{/// <summary>
 /// Interface
 /// </summary>
    public interface IUniversalInteractor
    {
        /// <summary>
        /// Ingreso Universal   
        /// </summary>
        /// <param name="ingresoUniversal"></param>
        /// <returns></returns>       
        Task<IngresoUniversalNSD> IngresoUniversal(IngresoUniversal ingresoUniversal);

    }
}

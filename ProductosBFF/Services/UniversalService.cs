using ProductosBFF.Domain.Parameters;
using ProductosBFF.Domain.Universal;
using ProductosBFF.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProductosBFF.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class UniversalService : IUniversalService
    {
        private readonly IUniversalInfrastructure _universalInfrastructure;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="universalInfrastructure"></param>
        public UniversalService(IUniversalInfrastructure universalInfrastructure)
        {
            _universalInfrastructure = universalInfrastructure;
        }

        /// <summary>
        /// GetProduct
        /// </summary>
        /// <param name="ingresoUniversal"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<IngresoUniversalNSD> IngresoUniversal(IngresoUniversal ingresoUniversal)
        {
            try
            {

                return await _universalInfrastructure.IngresoUniversal(ingresoUniversal);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al consultar a la base de datos " + ex);
            }
        }

    }
}

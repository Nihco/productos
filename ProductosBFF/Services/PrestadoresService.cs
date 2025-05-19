using System;
using AutoMapper;
using ProductosBFF.Domain.Parameters;
using ProductosBFF.Domain.Productos;
using ProductosBFF.Interfaces;
using ProductosBFF.Models.Productos;
using System.Threading.Tasks;

namespace ProductosBFF.Services
{
    /// <summary>
    /// Clase
    /// </summary>
    public class PrestadoresService : IPrestadoresService
    {
        private readonly IPrestadoresInfrastructure _prestadoresInfrastructure;
        private readonly IMapper _mapper;
     
        private const string MensajeError = "Sin Datos";

        /// <summary>
        /// Constructor
        /// </summary>
        public PrestadoresService(IPrestadoresInfrastructure prestadoresInfrastructure, IMapper mapper)
        {
            _prestadoresInfrastructure = prestadoresInfrastructure;
            _mapper = mapper;
        }

        /// <summary>
        /// Get PRestadores
        /// </summary>
        /// <param name="bc"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<PrestadoresDto> GetPrestadores(BodyPrestadores bc)
        {
            var result = await _prestadoresInfrastructure.GetPrestadoresAsync(bc) ??
                         throw new InvalidOperationException(MensajeError);

            return _mapper.Map<PrestadoresLeyCorta, PrestadoresDto>(result);
        }

    }
}
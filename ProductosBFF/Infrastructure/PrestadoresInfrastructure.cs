using ProductosBFF.Domain.Parameters;
using ProductosBFF.Domain.Productos;
using ProductosBFF.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ProductosBFF.Infrastructure
{
    /// <summary>
    /// Clase
    /// </summary>
    public class PrestadoresInfrastructure : IPrestadoresInfrastructure
    {
        private readonly IHttpClientService _httpClientServicePrestadores;
        private readonly string _urlLeyCorta;

        /// <summary>
        /// Constructor
        /// </summary>
        public PrestadoresInfrastructure(IHttpClientService httpClientService,
            IConfiguration comfig)
        {
            _httpClientServicePrestadores = httpClientService;
            _urlLeyCorta = comfig.GetValue<string>("PRODUCTO:PRODUCTO_URL");
        }

        /// <summary>
        /// Trae Prestadores
        /// </summary>
        /// <param name="bc"></param>
        /// <returns></returns>
        public async Task<PrestadoresLeyCorta> GetPrestadoresAsync(BodyPrestadores bc)
        {
            return await _httpClientServicePrestadores.GetAsync<PrestadoresLeyCorta>(_urlLeyCorta + "Prestadores/BC/" + bc.PIN_BC);
        }

    }
}
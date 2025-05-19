using Microsoft.Extensions.Configuration;
using ProductosBFF.Domain.Universal;
using ProductosBFF.Interfaces;
using System.Threading.Tasks;

namespace ProductosBFF.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class UniversalInfrastructure : IUniversalInfrastructure
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _url;

        /// <summary>
        /// Constructor
        /// </summary>
        public UniversalInfrastructure(IHttpClientService httpClientService,
            IConfiguration comfig)
        {
            _httpClientService = httpClientService;
            _url = comfig.GetValue<string>("PRODUCTO:PRODUCTO_URL");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ingresoUniversal"></param>
        /// <returns></returns>
        public async Task<IngresoUniversalNSD> IngresoUniversal(Domain.Parameters.IngresoUniversal ingresoUniversal)
        {
            return await _httpClientService.PostAsync<IngresoUniversalNSD>(_url + "Universal/IngresoUniversal",
                ingresoUniversal);
        }
    }
}
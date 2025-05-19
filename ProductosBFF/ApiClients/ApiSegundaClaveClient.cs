using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using ProductosBFF.Interfaces;
using ProductosBFF.Models.SegundaClave;

namespace ProductosBFF.ApiClients
{
    /// <summary>
    /// 
    /// </summary>
    public class ApiSegundaClaveClient : IApiSegundaClaveClient
    {
        private readonly ILogger<ApiSegundaClaveClient> _logger;
        private readonly string _url;
        private readonly IHttpClientService _httpClientService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="comfig"></param>
        /// <param name="httpClientService"></param>
        public ApiSegundaClaveClient(ILogger<ApiSegundaClaveClient> logger, IConfiguration comfig,
            IHttpClientService httpClientService)
        {
            _logger = logger;
            _httpClientService = httpClientService;
            _url = comfig["PRODUCTO:PRODUCTO_URL"];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="solicitud"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<SegundaClaveDto> SolicitarSegundaClaveAsync(SolicitudClaveDto solicitud)
        {
            try
            {
                var policy = CreatePolicy();
                var response =
                    await policy.ExecuteAsync(async () =>
                        await _httpClientService.PostAsync<SegundaClaveDto>(_url + "SegundaClave/SolicitarSegundaClave",
                            solicitud));

                if (response != null) return response;

                _logger.LogError("Respuesta  API:'{Uri}'", _url);
                return new SegundaClaveDto();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Error al consumir el servicio REST {_url}, Error: {e.Message}");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="verificacion"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<VerificadoDto> VerificarSegundaClaveAsync(VerificarSegundaClaveDto verificacion)
        {
            try
            {
                var policy = CreatePolicy();

                var response = await policy.ExecuteAsync(async () =>
                    await _httpClientService.PostAsync<VerificadoDto>(_url + "SegundaClave/VerificarSegundaClave",
                        verificacion));

                if (response != null) return response;

                _logger.LogError("Respuesta  API:'{Uri}'", _url);
                return new VerificadoDto();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Error al consumir el servicio REST {_url}, Error: {e.Message}");
            }
        }

        private AsyncRetryPolicy CreatePolicy()
        {
            var policy = Policy
                .Handle<Exception>()
                .RetryAsync(2,
                    (exception, reintento) =>
                    {
                        _logger.LogError("Intento {RetryCount} fallido. Error: {Message}", reintento,
                            exception.Message);
                    });
            return policy;
        }
    }
}
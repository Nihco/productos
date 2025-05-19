using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProductosBFF.Interfaces;

namespace ProductosBFF.Utils
{
    /// <summary>
    /// Clase
    /// </summary>
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        private readonly ILogger<HttpClientService> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpClientFactory"></param>
        /// <param name="config"></param>
        /// <param name="logger"></param>
        public HttpClientService(IHttpClientFactory httpClientFactory,IConfiguration config, ILogger<HttpClientService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
            _logger = logger;
        }

        private async Task<T> SendRequestAsync<T>(string url, HttpMethod method, object queryParams = null,
            object body = null, Dictionary<string, string> headers = null)
        {
            var client = _httpClientFactory.CreateClient();
            string requestUri = url;

            if (queryParams is not null)
            {
                var uriBuilder = new UriBuilder(url);
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                foreach (var param in ConvertToQueryParams(queryParams)
                             .Where(param => !string.IsNullOrEmpty(param.Value)))
                {
                    query[param.Key] = param.Value;
                }

                uriBuilder.Query = query.ToString() ?? string.Empty;
                requestUri = uriBuilder.ToString();
            }

            var request = new HttpRequestMessage(method, requestUri);

            AddHeaders(request, headers);

            if (body != null)
            {
                var json = JsonConvert.SerializeObject(body);
                request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            }

            try
            {
                using var response = await client.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.NotFound ||
                    response.StatusCode == HttpStatusCode.BadRequest ||
                    response.StatusCode == HttpStatusCode.PreconditionFailed ||
                    response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    _logger.LogError(
                        $"Respuesta  API:'{requestUri}', StatusCode: '{Convert.ToInt32(response.StatusCode)}'");
                    return default;
                }
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException($"Error en la solicitud HTTP: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error inesperado: {ex.Message}", ex);
            }
        }
        
        /// <summary>
        /// Método para agregar headers a una solicitud HTTP.
        /// </summary>
        /// <param name="request">La solicitud HTTP.</param>
        /// <param name="headers">Los headers a agregar.</param>
        private void AddHeaders(HttpRequestMessage request, Dictionary<string, string> headers)
        {
            request.Headers.Add(_config["PRODUCTO_HEADER_NAME"],_config["PRODUCTO_HEADER_VALUE"]);
            if (headers == null) return;
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }
        
        private static Dictionary<string, string> ConvertToQueryParams(object obj)
        {
            var queryParams = new Dictionary<string, string>();
            if (obj == null) return queryParams;

            var properties = obj.GetType().GetProperties();
            foreach (var prop in properties)
            {
                var value = prop.GetValue(obj)?.ToString();
                if (!string.IsNullOrEmpty(value))
                {
                    queryParams.Add(prop.Name, value);
                }
            }

            return queryParams;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="queryParams"></param>
        /// <param name="headers"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string url, object queryParams = null,
            Dictionary<string, string> headers = null)
        {
            return await SendRequestAsync<T>(url, HttpMethod.Get, queryParams, null, headers);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <param name="queryParams"></param>
        /// <param name="headers"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> PostAsync<T>(string url, object body = null, object queryParams = null,
            Dictionary<string, string> headers = null)
        {
            return SendRequestAsync<T>(url, HttpMethod.Post, queryParams, body, headers);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="body"></param>
        /// <param name="queryParams"></param>
        /// <param name="headers"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T> PutAsync<T>(string url, object body = null, object queryParams = null,
            Dictionary<string, string> headers = null)
        {
            return SendRequestAsync<T>(url, HttpMethod.Put, queryParams, body, headers);
        }
    }
}
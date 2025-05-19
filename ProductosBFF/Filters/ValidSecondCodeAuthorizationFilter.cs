using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using ProductosBFF.Domain.SegundaClave;
using ProductosBFF.Interfaces;
using ProductosBFF.Models.Json;
using ProductosBFF.Models.SegundaClave;

namespace ProductosBFF.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidSecondCodeAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly ILogger<ValidSecondCodeAuthorizationFilter> _logger;
        private readonly IApiSegundaClaveClient _client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="client"></param>
        public ValidSecondCodeAuthorizationFilter(ILogger<ValidSecondCodeAuthorizationFilter> logger,
            IApiSegundaClaveClient client)
        {
            _logger = logger;
            _client = client;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            try
            {
                #region CODIGO REAL COMIENZA DESDE ACÁ

                var token = GetToken(context.HttpContext);
                var data = GetClientData(DecodeTokenToJson(token));

                var valorCodigo = context.HttpContext.Request.Headers["X-CONSALUD-CLAVE2-VALOR"];
                if (valorCodigo.Count == 0)
                {
                    var solicitud = await _client.SolicitarSegundaClaveAsync(
                        new SolicitudClaveDto
                        {
                            DescripcionTransaccion = "CANCELA_BC_COSTO_CERO",
                            Rut = data.Rut,
                            Dv = data.Dv,
                            Folio = data.Folio
                        });
                    context.Result = new ObjectResult("PreconditionFailed")
                    {
                        StatusCode = (int)HttpStatusCode.PreconditionFailed,
                        Value = new SegundaClaveResponse
                        {
                            CodigoEstado = solicitud.CodigoEstado,
                            TtlSegundos = solicitud.TtlSegundos
                        }
                    };
                    return;
                }

                if (!int.TryParse(valorCodigo, out _))
                {
                    context.Result = new ObjectResult("PreconditionFailed")
                    {
                        StatusCode = (int)HttpStatusCode.PreconditionFailed,
                        Value = new
                        {
                            CodigoEstado = "VALORES-INVALIDOS"
                        }
                    };
                    return;
                }
                
                var verificado = await _client.VerificarSegundaClaveAsync(new VerificarSegundaClaveDto
                {
                    Codigo = valorCodigo,
                    DescripcionTransaccion = "CANCELA_BC_COSTO_CERO",
                    Rut = data.Rut,
                    Dv = data.Dv,
                    Folio = data.Folio,
                    Ip = context.HttpContext.Request.Headers["x-real-ip"]
                });


                if (verificado.IdAuditoria != 0)
                {
                    context.HttpContext.Items["IdAuditoria"] = verificado.IdAuditoria;
                    return;
                }

                context.Result = new ObjectResult("PreconditionFailed")
                {
                    StatusCode = (int)HttpStatusCode.PreconditionFailed,
                    Value = new
                    {
                        verificado.CodigoEstado
                    }
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }

            #endregion
        }

        private static string GetToken(HttpContext context)
        {
            var headerValue = context.Request.Headers["Authorization"];
            if (!headerValue.Any() || string.IsNullOrEmpty(headerValue[0]))
            {
                return null;
            }

            return headerValue[0].Replace("Bearer ", "");
        }

        private string DecodeTokenToJson(string token)
        {
            try
            {
                var json = JwtBuilder.Create()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .Decode(token);
                return json;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Token no pasa validación de JwtBuilder: '{message}'", ex.Message);
                throw;
            }
        }

        private static JsonData GetClientData(string json)
        {
            var jsonParsed = JObject.Parse(json);
            var datos = JObject.Parse(jsonParsed.Value<object>("Datos").ToString());
            return new JsonData
            {
                Rut = long.Parse(jsonParsed.Value<string>("sub")),
                Dv = datos.Value<string>("Digito"),
                Folio = long.Parse(datos.Value<string>("FolioSuscripcion"))
            };
        }
    }
}
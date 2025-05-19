using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Nancy.Json;
using ProductosBFF.Class;
using ProductosBFF.Domain.Authorization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace ProductosBFF.Filters
{
    /// <summary>
    /// Clase
    /// </summary>
    public class UserAuthorizationFilter : ActionFilterAttribute, IAuthorizationFilter
    {
        private static readonly List<string> RutNames = new()
        {
            "rut",
            "pivRutSolicita",
            "pinRutSolicita",
            "mantisa",
            "pin_RUT_SOLICITA"
        };

        /// <summary>
        /// Autorizacion
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            try
            {
                var token = GetToken(context.HttpContext);
                if (string.IsNullOrEmpty(token))
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
                var objAuthorization = new UserAuthorization()
                {
                    TOKEN = token,
                    RUT = GetRut(context.HttpContext)
                };
                GetAccess(objAuthorization, context);
            }
            catch (Exception)
            {
                context.Result = new BadRequestResult();
            }
        }

        private static string GetToken(HttpContext context)
        {
            string header = context.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(header))
            {
                return string.Empty;
            }
            return header.Replace("Bearer ", "");
        }

        private static string GetRut(HttpContext context)
        {
            return context.Request.Method switch
            {
                "GET" => GetRutFromHttpQuery(context),
                "POST" => GetRutFromHttpBody(context),
                "DELETE" => GetRutFromHttpQuery(context),
                _ => string.Empty,
            };
        }

        private static string GetRutFromHttpQuery(HttpContext context)
        {
            if (context.Request.QueryString.HasValue)
            {
                var rutKey = context.Request.Query.Keys.FirstOrDefault(y => RutNames.Contains(y));
                if (!string.IsNullOrEmpty(rutKey))
                {
                    return context.Request.Query[rutKey].ToString();
                }
            }
            var keys = context.Request.RouteValues.Keys.Select((value, index) => new { Index = index, Value = value });
            var indexRut = keys.FirstOrDefault(item => RutNames.Contains(item.Value));
            return indexRut is not null ? context.Request.RouteValues.Values.ToArray()[indexRut.Index].ToString() : string.Empty;
        }

        private static string GetRutFromHttpBody(HttpContext context)
        {
            var serializar = new JavaScriptSerializer();
            context.Request.EnableBuffering();
            string json = new StreamReader(context.Request.Body).ReadToEndAsync().Result;
            context.Request.Body.Position = 0;
            if (!string.IsNullOrEmpty(json))
            {
                dynamic itemJson = serializar.Deserialize<object>(json);
                foreach (var item in itemJson.Keys)
                {
                    var rutKey = RutNames.FirstOrDefault(x => x.Equals(item));
                    if (!string.IsNullOrEmpty(rutKey))
                    {
                        return itemJson[rutKey].ToString();
                    }
                }
            }
            return string.Empty;
        }

        private static void GetAccess(UserAuthorization objAuthorization, AuthorizationFilterContext context)
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");
            var _configuration = builder.Build();
            string _urlApi = _configuration.GetValue<string>("SEGURIDAD:SEGURIDAD_URL");

            Uri _rest_server_uri;
            _rest_server_uri = new Uri(_urlApi);
            using var _client = new HttpClient();
            HttpContent Contenido = SerializaJson.CreateHttpContent(objAuthorization);
            var result = _client.PostAsync(_rest_server_uri, Contenido).Result;
            int statusCode = (int)result.StatusCode;
            switch (statusCode)
            {
                case 401:
                    context.Result = new UnauthorizedResult();
                    return;
                case 403:
                    context.Result = new ObjectResult("Forbidden") { StatusCode = 403 };
                    return;
                case 500:
                    context.Result = new BadRequestResult();
                    return;
            }
        }
    }
}

using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ProductosBFF.Class
{
    /// <summary>
    /// Clase
    /// </summary>
    public static class SerializaJson
    {
        /// <summary>
        /// Método privados para envio de datos
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static HttpContent CreateHttpContent(object content)
        {
            try
            {
                HttpContent httpContent = null;
                if (content != null)
                {
                    var ms = new MemoryStream();
                    SerializeJsonIntoStream(content, ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    httpContent = new StreamContent(ms);
                    httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }
                return httpContent;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al ejecutar CreateHttpContent ", ex);
            }
        }

        /// <summary>
        /// Método privado para serialización de datos
        /// </summary>
        /// <param name="value"></param>
        /// <param name="stream"></param>
        public static void SerializeJsonIntoStream(object value, Stream stream)
        {
            try
            {
                using var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true);
                using var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None };
                var js = new JsonSerializer();
                js.Serialize(jtw, value);
                jtw.Flush();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error al ejecutar SerializeJsonIntoStream ", ex);
            }
        }
    }
}

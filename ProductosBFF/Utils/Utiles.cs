using Microsoft.Extensions.Configuration;
using System.IO;

namespace ProductosBFF.Utils
{
    /// <summary>
    /// Clase
    /// </summary>
    public static class Utiles
    {
        /// <summary>
        /// GetTimeOut
        /// </summary>
        /// <returns></returns>
        public static int GetTimeOut()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var _configuration = builder.Build();

            if (!int.TryParse(_configuration.GetValue<string>("Configs:ApiTimeout"), out var timeout)) timeout = 0;
            return (timeout == 0 ? 60 : timeout) * 1000;
        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NewRelic.LogEnrichers.Serilog;
using Serilog;
using System;

namespace ProductosBFF
{
    /// <summary>
    /// Program Class
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                // Para filtrar logs de health check
                .Filter.ByExcluding("RequestPath like '/health%'")
                .Enrich.WithNewRelicLogsInContext()
                .WriteTo.Console()
                .CreateLogger();
            try
            {
                Log.Information("Iniciando la aplicación");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Excepción en la aplicación");
            }
            finally
            {
                Log.Information("Saliendo de la aplicación");
                Log.CloseAndFlush();
            }
        }

        /// <summary>
        /// Create Host Builder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>().UseUrls("http://*:5000", "http://*:8000");
                });
    }
}

using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProductosBFF.Filters;
using ProductosBFF.Infrastructure;
using ProductosBFF.Interfaces;
using ProductosBFF.Services;
using ProductosBFF.Utils;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text.Json;
using ProductosBFF.ApiClients;


namespace ProductosBFF
{
    /// <summary>
    /// Clase startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Configure Services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddHttpClient();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            services.AddScoped<IProductoInfrastructure, ProductoInfrastructure>();
            services.AddScoped<IProductoService, ProductoService>();
            services.AddScoped<IPrestadoresInfrastructure, PrestadoresInfrastructure>();
            services.AddScoped<IPrestadoresService, PrestadoresService>();
            services.AddScoped<IHttpClientService, HttpClientService>();

            services.AddScoped<IApiSegundaClaveClient, ApiSegundaClaveClient>();


            // Disponibiliza health check por defecto para usan al momento de desplegar
            services.AddHealthChecks();

            services.AddControllers(options => { options.Filters.Add<ValidateModelStateAttribute>(); });

            services.AddControllers(options =>
            {
                options.Filters.Add<ValidateModelStateAttribute>();
            })

                .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase)
                .AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            AddSwagger(services);

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductosBFF v1");

                    c.SupportedSubmitMethods(new SubmitMethod[]
                    {
                        SubmitMethod.Delete, SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Patch
                    });

                    c.SupportedSubmitMethods(SubmitMethod.Delete, SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Patch);

                });
                app.UseCors(options =>
                {
                    options.WithOrigins("http://localhost:3000", "http://localhost:3001",
                        "https://t.sucursaldigital.consalud.cloud", "*.consalud.cl", "http://localhost:9000",
                        "http://localhost:9006");
                    options.AllowAnyMethod();
                    options.AllowAnyHeader();
                });
            }

            if (env.IsProduction())
            {
                app.UseCors(options =>
                {
                    options.WithOrigins("*.consalud.cl");
                    options.AllowAnyMethod();
                    options.AllowAnyHeader();
                });
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });

            NewRelic.Api.Agent.NewRelic.SetApplicationName($"ProductosBFF");
        }

        #region Swagger

        /// <summary>
        /// Agregar la configuración de swagger
        /// </summary>
        /// <param name="services"></param>
#pragma warning disable CA1822 // Mark members as static
        private void AddSwagger(IServiceCollection services)
#pragma warning restore CA1822 // Mark members as static
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";
                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"{Assembly.GetExecutingAssembly().GetName().Name} v1",
                    Version = groupName,
                    Description = $"{Assembly.GetExecutingAssembly().GetName().Name} BFF",
                    Contact = new OpenApiContact
                    {
                        Name = "Consalud"
                    }
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header usando  the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.",
                    Name = "X-CONSALUD-AUTHORIZATION",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                        },
                        new List<string>()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //si no se inculye el archvo XML de documentaci?n, caer? en este punto al ejecutar
                options.IncludeXmlComments(xmlPath);
            });
        }

        #endregion
    }
}
using AutoMapper;
using ProductosBFF.Domain.Parameters;
using ProductosBFF.Models.BCCesantia;
using System;
using System.Linq;
using System.Reflection;

namespace ProductosBFF.Mappings
{
    /// <summary>
    /// Clase para mapear perfiles
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Constructor que llama a metodos para ejecutar el mapping
        /// </summary>
        public MappingProfile()
        {
            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());

            ApplyCustomMappings();
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                                 ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }

        /// <summary>
        /// Aplicar mapeos customizables
        /// </summary>
        private void ApplyCustomMappings()
        {
            var mappingExpression = CreateMap<BodySolicitudActivacion, DtoAfil>();
            MapsUtils.ApplyPinPivMappings(mappingExpression);

        }
    }
}
using AutoMapper;
using System;
using System.Reflection;

namespace ProductosBFF.Mappings
{
    /// <summary>
    /// Mapeo customizado de clases a  campos piv y pin
    /// </summary>
    public abstract class MapsUtils
    {
        /// <summary>
        /// Mapeo de clases
        /// </summary>
        /// <param name="mapping"></param>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        public static void ApplyPinPivMappings<TSource, TDestination>(IMappingExpression<TSource, TDestination> mapping)
        {
            mapping.AfterMap((src, dest) =>
            {
                Type destType = dest.GetType();
                Type srcType = src.GetType();

                foreach (PropertyInfo destProperty in destType.GetProperties())
                {
                    if (!destProperty.Name.StartsWith("Piv") && !destProperty.Name.StartsWith("Pin")) continue;
                    string srcPropertyName = destProperty.Name[3..]; // Elimina los primeros 3 caracteres (Piv o Pin)
                    PropertyInfo srcProperty = srcType.GetProperty(srcPropertyName);

                    if (srcProperty == null || srcProperty.PropertyType != destProperty.PropertyType) continue;
                    object srcValue = srcProperty.GetValue(src);
                    destProperty.SetValue(dest, srcValue);
                }
            });
        }
    }
}
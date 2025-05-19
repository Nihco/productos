using ProductosBFF.Models.Productos;
using Xunit;

namespace ProductosBFFTests.Models.Productos
{
    public class ProductosLeyCortaDtoTest
    {
      [Fact]
        public void ProductLeyCortaDto_Properties_SetAndGetCorrectly()
        {
            var expectedFolioSuscripcion = 1234567890L;
            var expectedId = "BC123";
            var expectedName = "Nombre BC";
            var expectedFechaInicio = "01/01/2024";
            var expectedIncluidoPlan = "Sí";
            var expectedUsarDesde = "15/01/2024";
            var expectedPlazoValido = 12.5m;
            var expectedCodigoPlan = "Plan123";
            var expectedPlazoUso = 6.5m;
            var expectedTextoEtiqueta = "Etiqueta";
            var expectedColorEtiqueta = "Rojo";
            var expectedIcono = "icon.png";
            var expectedTextoSolicitud = "Solicitud";
            var expectedFamilia = "Familia1";
            var expectedUrlResumen = "http://example.com/resumen";
            var expectedUrlCondiciones = "http://example.com/condiciones";
            var expectedValueAprox = "$0,00";
            
            var dto = new ProductLeyCortaDto
            {
                folio_suscripcion = expectedFolioSuscripcion,
                id = expectedId,
                name = expectedName,
                fecha_inicio = expectedFechaInicio,
                incluido_plan = expectedIncluidoPlan,
                usar_desde = expectedUsarDesde,
                plazo_valido = expectedPlazoValido,
                codigo_plan = expectedCodigoPlan,
                plazo_uso = expectedPlazoUso,
                texto_etiqueta = expectedTextoEtiqueta,
                color_etiqueta = expectedColorEtiqueta,
                icono = expectedIcono,
                texto_solicitud = expectedTextoSolicitud,
                familia = expectedFamilia,
                url_resumen = expectedUrlResumen,
                url_condiciones = expectedUrlCondiciones,
                valueAprox = expectedValueAprox
            };
            
            Assert.Equal(expectedFolioSuscripcion, dto.folio_suscripcion);
            Assert.Equal(expectedId, dto.id);
            Assert.Equal(expectedName, dto.name);
            Assert.Equal(expectedFechaInicio, dto.fecha_inicio);
            Assert.Equal(expectedIncluidoPlan, dto.incluido_plan);
            Assert.Equal(expectedUsarDesde, dto.usar_desde);
            Assert.Equal(expectedPlazoValido, dto.plazo_valido);
            Assert.Equal(expectedCodigoPlan, dto.codigo_plan);
            Assert.Equal(expectedPlazoUso, dto.plazo_uso);
            Assert.Equal(expectedTextoEtiqueta, dto.texto_etiqueta);
            Assert.Equal(expectedColorEtiqueta, dto.color_etiqueta);
            Assert.Equal(expectedIcono, dto.icono);
            Assert.Equal(expectedTextoSolicitud, dto.texto_solicitud);
            Assert.Equal(expectedFamilia, dto.familia);
            Assert.Equal(expectedUrlResumen, dto.url_resumen);
            Assert.Equal(expectedUrlCondiciones, dto.url_condiciones);
            Assert.Equal(expectedValueAprox, dto.valueAprox);
        }
    }
}
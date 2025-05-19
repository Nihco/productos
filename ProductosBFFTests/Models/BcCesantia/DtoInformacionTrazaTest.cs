using ProductosBFF.Models.BCCesantia;
using Xunit;

namespace ProductosBFFTests.Models.BcCesantia
{
    public class DtoInformacionTrazaTest
    {
        [Fact]
        public void DtoInformacionTraza_ShouldSetAndGetPropertiesCorrectly()
        {
            const string expectedTitulo = "Titulo de Prueba";
            const string expectedDescripcion = "Descripcion de Prueba";
            const string expectedFechaDescripcion = "01/01/2024";
            const string expectedEnlace = "https://example.com";
            const string expectedBoton = "Boton de Prueba";
            const string expectedEnlaceBoton = "https://example.com/boton";
            const string expectedEstiloBoton = "Estilo1";
            const decimal expectedIdOrden = 12345;

            var dtoInformacionTraza = new DtoInformacionTraza
            {
                titulo = expectedTitulo,
                descripcion = expectedDescripcion,
                fechaDescripcion = expectedFechaDescripcion,
                enlace = expectedEnlace,
                boton = expectedBoton,
                enlaceBoton = expectedEnlaceBoton,
                estiloBoton = expectedEstiloBoton,
                idOrden = expectedIdOrden
            };

            Assert.Equal(expectedTitulo, dtoInformacionTraza.titulo);
            Assert.Equal(expectedDescripcion, dtoInformacionTraza.descripcion);
            Assert.Equal(expectedFechaDescripcion, dtoInformacionTraza.fechaDescripcion);
            Assert.Equal(expectedEnlace, dtoInformacionTraza.enlace);
            Assert.Equal(expectedBoton, dtoInformacionTraza.boton);
            Assert.Equal(expectedEnlaceBoton, dtoInformacionTraza.enlaceBoton);
            Assert.Equal(expectedEstiloBoton, dtoInformacionTraza.estiloBoton);
            Assert.Equal(expectedIdOrden, dtoInformacionTraza.idOrden);
        }
    }
}
using ProductosBFF.Models.Productos;
using Xunit;

namespace ProductosBFFTests.Models.Productos
{
    public class PrestadorDtoTest
    {
        [Fact]
        public void PrestadorDtoTest_Properties_SetAndGetCorrectly()
        {
            var expectedcodigo_BC = 750;
            var expectednombre = "Clínica Consalud";           

            var dto = new PrestadorDto
            {
                codigo_bc = expectedcodigo_BC,
                nombre = expectednombre
            };

            Assert.Equal(expectedcodigo_BC, dto.codigo_bc);
            Assert.Equal(expectednombre, dto.nombre);          
        }
    }
}
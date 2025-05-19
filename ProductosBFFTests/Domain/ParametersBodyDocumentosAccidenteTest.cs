using ProductosBFF.Domain.Parameters;
using Xunit;

namespace ProductosBFFTests.Domain
{
    public class ParametersBodyDocumentosAccidenteTest
    {
        [Fact]
        public void BodyDocumentosAccidente_Ok()
        {
            const decimal expectedIdBc = 123;
            const decimal expectedTipoAccidente = 123;
            const decimal expectedTipoAtencion = 456;
            const string expectedMarcaUrgencia = "Urgencia";
            const string expectedMarcaIntervencion = "Intervencion";
            const string expectedRcaSeguro = "Seguro";
            const string expectedContinuidad = "Continuidad";
            
            var bodyDocumentosAccidente = new BodyDocumentosAccidente
            {
                IdBc = expectedIdBc,
                TipoAccidente = expectedTipoAccidente,
                TipoAtencion = expectedTipoAtencion,
                MarcaUrgencia = expectedMarcaUrgencia,
                MarcaIntervencion = expectedMarcaIntervencion,
                RcaSeguro = expectedRcaSeguro,
                Continuidad = expectedContinuidad
            };
            
            Assert.Equal(expectedIdBc, bodyDocumentosAccidente.IdBc);
            Assert.Equal(expectedTipoAccidente, bodyDocumentosAccidente.TipoAccidente);
            Assert.Equal(expectedTipoAtencion, bodyDocumentosAccidente.TipoAtencion);
            Assert.Equal(expectedMarcaUrgencia, bodyDocumentosAccidente.MarcaUrgencia);
            Assert.Equal(expectedMarcaIntervencion, bodyDocumentosAccidente.MarcaIntervencion);
            Assert.Equal(expectedRcaSeguro, bodyDocumentosAccidente.RcaSeguro);
            Assert.Equal(expectedContinuidad, bodyDocumentosAccidente.Continuidad);
        }
    }
}
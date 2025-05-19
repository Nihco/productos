using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using ProductosBFF.Domain.Parameters;
using ProductosBFF.Domain.Productos;
using ProductosBFF.Interfaces;
using ProductosBFF.Models.Productos;
using ProductosBFF.Services;
using Xunit;

namespace ProductosBFFTests.Services
{
    public class PrestadoresServiceTest
    {
        private readonly Mock<IPrestadoresInfrastructure> _mockPrestadoresInfrastructure;
        private readonly Mock<IMapper> _mockMapper;
        private readonly PrestadoresService _service;

        public PrestadoresServiceTest()
        {
            _mockPrestadoresInfrastructure = new Mock<IPrestadoresInfrastructure>();
            _mockMapper = new Mock<IMapper>();
            _service = new PrestadoresService(_mockPrestadoresInfrastructure.Object, _mockMapper.Object);
        }
        
           [Fact]
        public async Task GetPrestadores_ConDatosValidos_DebeRetornarPrestadoresDtoMapeado()
        {
            var bodyPrestadores = new BodyPrestadores { PIN_BC = 12345 };

            var prestadorDominio = new Prestador { CODIGO_BC = "001", NOMBRE = "Clínica Central" };
            var prestadoresLeyCortaDesdeInfra = new PrestadoresLeyCorta
            {
                ListaClase = new List<Prestador> { prestadorDominio }
            };

            var prestadorDtoMapeado = new PrestadorDto { codigo_bc = 1, nombre = "Clínica Central" };
            var prestadoresDtoEsperado = new PrestadoresDto
            {
                ListaClase = new List<PrestadorDto> { prestadorDtoMapeado }
            };

            _mockPrestadoresInfrastructure
                .Setup(infra => infra.GetPrestadoresAsync(bodyPrestadores))
                .ReturnsAsync(prestadoresLeyCortaDesdeInfra);

            _mockMapper
                .Setup(mapper => mapper.Map<PrestadoresLeyCorta, PrestadoresDto>(prestadoresLeyCortaDesdeInfra))
                .Returns(prestadoresDtoEsperado);
            
            var resultado = await _service.GetPrestadores(bodyPrestadores);
            
            Assert.NotNull(resultado);
            Assert.IsType<PrestadoresDto>(resultado);
            Assert.NotNull(resultado.ListaClase);
            Assert.Single(resultado.ListaClase); 

            var primerPrestadorResultado = resultado.ListaClase[0];
            Assert.Equal(prestadoresDtoEsperado.ListaClase[0].codigo_bc, primerPrestadorResultado.codigo_bc);
            Assert.Equal(prestadoresDtoEsperado.ListaClase[0].nombre, primerPrestadorResultado.nombre);

            _mockPrestadoresInfrastructure.Verify(infra => infra.GetPrestadoresAsync(bodyPrestadores), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<PrestadoresLeyCorta, PrestadoresDto>(prestadoresLeyCortaDesdeInfra), Times.Once);
        }
        
        [Fact]
        public async Task GetPrestadores_ConListaVaciaDesdeInfra_DebeRetornarDtoConListaVacia()
        {
            var bodyPrestadores = new BodyPrestadores { PIN_BC = 67890 };

            var prestadoresLeyCortaDesdeInfra = new PrestadoresLeyCorta
            {
                ListaClase = new List<Prestador>() 
            };

            var prestadoresDtoEsperado = new PrestadoresDto
            {
                ListaClase = new List<PrestadorDto>() 
            };

            _mockPrestadoresInfrastructure
                .Setup(infra => infra.GetPrestadoresAsync(bodyPrestadores))
                .ReturnsAsync(prestadoresLeyCortaDesdeInfra);

            _mockMapper
                .Setup(mapper => mapper.Map<PrestadoresLeyCorta, PrestadoresDto>(prestadoresLeyCortaDesdeInfra))
                .Returns(prestadoresDtoEsperado);
            
            var resultado = await _service.GetPrestadores(bodyPrestadores);
            
            Assert.NotNull(resultado);
            Assert.IsType<PrestadoresDto>(resultado);
            Assert.NotNull(resultado.ListaClase);
            Assert.Empty(resultado.ListaClase);

            _mockPrestadoresInfrastructure.Verify(infra => infra.GetPrestadoresAsync(bodyPrestadores), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<PrestadoresLeyCorta, PrestadoresDto>(prestadoresLeyCortaDesdeInfra), Times.Once);
        }
        
        [Fact]
        public async Task GetPrestadores_CuandoInfraestructuraDevuelveNull_DebeLanzarInvalidOperationException()
        {
            var bodyPrestadores = new BodyPrestadores { PIN_BC = 11111 };

            _mockPrestadoresInfrastructure
                .Setup(infra => infra.GetPrestadoresAsync(bodyPrestadores))
                .ReturnsAsync((PrestadoresLeyCorta)null);
            
            var excepcion = await Assert.ThrowsAsync<InvalidOperationException>(() => _service.GetPrestadores(bodyPrestadores));
            Assert.Equal("Sin Datos", excepcion.Message);

            _mockPrestadoresInfrastructure.Verify(infra => infra.GetPrestadoresAsync(bodyPrestadores), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<PrestadoresLeyCorta, PrestadoresDto>(It.IsAny<PrestadoresLeyCorta>()), Times.Never);
        }
        
        [Fact]
        public async Task GetPrestadores_CuandoInfraestructuraLanzaExcepcion_DebePropagarExcepcion()
        {
            var bodyPrestadores = new BodyPrestadores { PIN_BC = 22222 };
            var excepcionDeInfra = new Exception("Error específico de la infraestructura");

            _mockPrestadoresInfrastructure
                .Setup(infra => infra.GetPrestadoresAsync(bodyPrestadores))
                .ThrowsAsync(excepcionDeInfra);
            
            var excepcionReal = await Assert.ThrowsAsync<Exception>(() => _service.GetPrestadores(bodyPrestadores));
            Assert.Same(excepcionDeInfra, excepcionReal);

            _mockPrestadoresInfrastructure.Verify(infra => infra.GetPrestadoresAsync(bodyPrestadores), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<PrestadoresLeyCorta, PrestadoresDto>(It.IsAny<PrestadoresLeyCorta>()), Times.Never);
        }
        
        [Fact]
        public async Task GetPrestadores_CuandoMapperDevuelveNull_DebeRetornarNull()
        {
            var bodyPrestadores = new BodyPrestadores { PIN_BC = 33333 };
            var prestadoresLeyCortaDesdeInfra = new PrestadoresLeyCorta
            {
                ListaClase = new List<Prestador> { new Prestador { CODIGO_BC = "002", NOMBRE = "Otro Prestador" } }
            };
            PrestadoresDto dtoMapeadoNulo = null;

            _mockPrestadoresInfrastructure
                .Setup(infra => infra.GetPrestadoresAsync(bodyPrestadores))
                .ReturnsAsync(prestadoresLeyCortaDesdeInfra);

            _mockMapper
                .Setup(mapper => mapper.Map<PrestadoresLeyCorta, PrestadoresDto>(prestadoresLeyCortaDesdeInfra))
                .Returns(dtoMapeadoNulo);
            
            var resultado = await _service.GetPrestadores(bodyPrestadores);
            
            Assert.Null(resultado);

            _mockPrestadoresInfrastructure.Verify(infra => infra.GetPrestadoresAsync(bodyPrestadores), Times.Once);
            _mockMapper.Verify(mapper => mapper.Map<PrestadoresLeyCorta, PrestadoresDto>(prestadoresLeyCortaDesdeInfra), Times.Once);
        }
    }
}
using System.Collections.Generic;
using AutoMapper;
using ProductosBFF.Domain.Productos;
using ProductosBFF.Models.Productos;
using Xunit;

namespace ProductosBFF.Tests
{
    public class PrestadoresDtoTests
    {
        private readonly IMapper _mapper;

        public PrestadoresDtoTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductosMappingProfile());
            });

            _mapper = config.CreateMapper();

            config.AssertConfigurationIsValid();
        }

        [Fact]
        public void PrestadoresDto_Mapping_ConfigurationIsValid()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductosMappingProfile());
            });

            config.AssertConfigurationIsValid();
        }

        [Fact]
        public void PrestadoresDto_ShouldMapPrestadoresLeyCortaToDto()
        {
            var prestadoresLeyCorta = new PrestadoresLeyCorta
            {
                ListaClase = new List<Prestador>
                {
                    new Prestador { CODIGO_BC = "123", NOMBRE = "Prestador 1" },
                    new Prestador { CODIGO_BC = "456", NOMBRE = "Prestador 2" }
                }
            };

            var result = _mapper.Map<PrestadoresDto>(prestadoresLeyCorta);

            Assert.NotNull(result);
            Assert.NotNull(result.ListaClase);
            Assert.Equal(2, result.ListaClase.Count);
            Assert.Equal(123, result.ListaClase[0].codigo_bc);
            Assert.Equal("Prestador 1", result.ListaClase[0].nombre);
            Assert.Equal(456, result.ListaClase[1].codigo_bc);
            Assert.Equal("Prestador 2", result.ListaClase[1].nombre);
        }
    }
        
    public class ProductosMappingProfile : Profile
    {
        public ProductosMappingProfile()
        {    
            new PrestadoresDto().Mapping(this);
        }
    }
}

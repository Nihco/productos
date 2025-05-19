using System;
using AutoMapper;
using ProductosBFF.Domain.Productos;
using ProductosBFF.Mappings;
using ProductosBFF.Models.Productos;
using Xunit;

namespace ProductosBFFTests.Models.Productos
{
    public class ProductoDtoTest
    {
        private readonly IMapper _mapper;

        public ProductoDtoTest()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void Mapping_Producto_To_ProductDto()
        {
            var producto = new Producto
            {
                CODIGO = "766",
                NOMBRE = "test",
                MONTO_UF = 123,
                MONTO_PESOS = 123456,
                USAR_DESDE = new DateTime(2024, 2, 17, 0, 0, 0, DateTimeKind.Utc),
                FECHA_INICIO = new DateTime(2023, 10, 26, 0, 0, 0, DateTimeKind.Utc),
                PLAZO_VALIDO = 1,
                ES_MULTICOTIZANTE = 0,
                ES_COLECTIVO = 1,
                TIPO_ACTIVACION = "VIDA_CAMARA",
                FECHA_CANCELADO = DateTime.MinValue,
                USAR_HASTA = new DateTime(2025, 2, 17, 0, 0, 0, DateTimeKind.Utc)
            };

            var productDto = _mapper.Map<ProductDto>(producto);

            Assert.NotNull(productDto);
            Assert.Equal(766, productDto.id);
            Assert.Equal("test", productDto.name);
            Assert.Equal("UF 123,000/mes", productDto.amountUf);
            Assert.Equal("$123.456", productDto.valueAprox);
            Assert.Equal("17/02/2024", productDto.usar_desde);
            Assert.Equal("26/10/2023", productDto.fecha_inicio);
            Assert.True(productDto.plazo_valido);
            Assert.False(productDto.es_multicotizante);
            Assert.True(productDto.es_colectivo);
            Assert.Equal("VIDA_CAMARA", productDto.tipoActivacion);
            Assert.True(productDto.es_vida_camara);
            Assert.False(productDto.es_cesantia);
            Assert.False(productDto.es_urgencia);
            Assert.False(productDto.es_familia_protegida);
            Assert.False(productDto.EsCostoCero);
            Assert.Null(productDto.FechaCancelado);
            Assert.Equal(new DateTime(2025, 2, 17, 0, 0, 0, DateTimeKind.Utc), productDto.DisponibleHasta);
            Assert.Equal(0, productDto.DuracionBCGratis);
            Assert.False(productDto.SeEstaCobrandoBc);
            Assert.False(productDto.SePuedeCancelar);
        }

        [Fact]
        public void Mapping_ProductosCostoCero_To_ProductDto()
        {
            var costoCero = new ProductosCostoCero
            {
                CODIGO = "766",  
                NOMBRE = "Costo Cero 1",
                fecha_suscripcion = "15/01/2024",
                FECHA_INICIO = new DateTime(2024,02,01),
                fecha_termino_gratuidad = "01/08/2024",
                fecha_termino_beneficio = "01/10/2024",
                MONTO_UF = 1.23,
                MONTO_PESOS = 45678m,
                estado = "Vigente",
                ICONO = "icon.png",
                TEXTO_ETIQUETA = "Etiqueta CC",
                COLOR_ETIQUETA = "Azul",
                URL_CONTRATO = "http://example.com",
                fecha_cancelado = DateTime.MinValue,
                duracion_bc_gratis = 6
            };
            
            var productDto = _mapper.Map<ProductDto>(costoCero);
            
            Assert.NotNull(productDto);
            Assert.Equal(766, productDto.id);
            Assert.Equal("Costo Cero 1", productDto.name);
            Assert.Equal("UF 1,230/mes", productDto.amountUf);
            Assert.Equal("$45.678", productDto.valueAprox);
            Assert.Equal("icon.png", productDto.icono);
            Assert.Equal("Etiqueta CC", productDto.texto_etiqueta);
            Assert.Equal("Azul", productDto.color_etiqueta);
            Assert.Equal("http://example.com", productDto.url_contrato);
            Assert.Null(productDto.FechaCancelado);
            Assert.True(productDto.EsCostoCero);
            Assert.Equal(6, productDto.DuracionBCGratis);
            Assert.True(productDto.SeEstaCobrandoBc);
            Assert.True(productDto.SePuedeCancelar);
            Assert.Null(productDto.tipoActivacion);
        }


        [Fact]
        public void Mapping_ProductosCostoCero_Null()
        {
            var costoCero = new ProductosCostoCero
            {
                CODIGO = "123",
                NOMBRE = "Costo Cero 1",
                fecha_suscripcion = "15/01/2024",
                fecha_inicio_beneficio = "01/02/2024",
                fecha_termino_gratuidad = "01/08/2024",
                fecha_termino_beneficio = null,
                MONTO_UF = 1.23,
                MONTO_PESOS = 45678m,
                estado = "Vigente",
                ICONO = "icon.png",
                TEXTO_ETIQUETA = "Etiqueta CC",
                COLOR_ETIQUETA = "Azul",
                URL_CONTRATO = "http://example.com",
                fecha_cancelado = DateTime.MinValue,
                duracion_bc_gratis = 6
            };
            
            var productDto = _mapper.Map<ProductDto>(costoCero);
            
            Assert.NotNull(productDto);
            Assert.Equal(123, productDto.id);
            Assert.Equal(6, productDto.DuracionBCGratis);
        }

        [Fact]
        public void Mapping_ProductosCostoCero_FechaCancelado()
        {
            var costoCero = new ProductosCostoCero
            {
                CODIGO = "766",
                NOMBRE = "Costo Cero 1",
                fecha_suscripcion = "15/01/2024",
                fecha_inicio_beneficio = "01/02/2024",
                fecha_termino_gratuidad = "01/08/2024",
                fecha_termino_beneficio = "01/10/2024",
                MONTO_UF = 1.23,
                MONTO_PESOS = 45678m,
                estado = "Vigente",
                ICONO = "icon.png",
                TEXTO_ETIQUETA = "Etiqueta CC",
                COLOR_ETIQUETA = "Azul",
                URL_CONTRATO = "http://url.com",
                fecha_cancelado = new DateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Utc)
            };

            var productDto = _mapper.Map<ProductDto>(costoCero);

            Assert.NotNull(productDto);
            Assert.Equal(766, productDto.id);
            Assert.Equal(new DateTime(2024, 6, 15, 0, 0, 0, DateTimeKind.Utc), productDto.FechaCancelado);
            Assert.False(productDto.SeEstaCobrandoBc);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using ProductosBFF.Domain.SegundaClave;
using ProductosBFF.Filters;
using ProductosBFF.Interfaces;
using ProductosBFF.Models.SegundaClave;
using Xunit;

namespace ProductosBFFTests.FilterTest
{
    public class ValidSecondCodeAuthorizationFilterTest
    {
        private readonly Mock<ILogger<ValidSecondCodeAuthorizationFilter>> _mockLogger;
        private readonly Mock<IApiSegundaClaveClient> _mockClient;
        private readonly ValidSecondCodeAuthorizationFilter _filter;
        
        private const long TestRut = 15809509;
        private const string TestDv = "2";
        private const long TestFolio = 9751336;
        private const string TestIp = "192.168.1.100";
        
        private static readonly string TestJwtPayload = JsonConvert.SerializeObject(new
        {
            sub = TestRut.ToString(),
            Datos = JsonConvert.SerializeObject(new
            {
                Digito = TestDv,
                FolioSuscripcion = TestFolio.ToString()
            })
        });
        
        private static readonly string TestHeaderJson = JsonConvert.SerializeObject(new { alg = "HS256", typ = "JWT" });
        
        private static string Base64UrlEncode(string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes)
                .Replace('+', '-')
                .Replace('/', '_')
                .TrimEnd('=');  
        }
        
        private readonly string _testToken = $"{Base64UrlEncode(TestHeaderJson)}.{Base64UrlEncode(TestJwtPayload)}.dummySignature";
        
        public ValidSecondCodeAuthorizationFilterTest()
        {
            _mockLogger = new Mock<ILogger<ValidSecondCodeAuthorizationFilter>>();
            _mockClient = new Mock<IApiSegundaClaveClient>();
            _filter = new ValidSecondCodeAuthorizationFilter(_mockLogger.Object, _mockClient.Object);
        }
        private AuthorizationFilterContext CreateTestContext(
            string authorizationHeader,
            string ipHeader,
            string secondKeyCodeHeader)
        {
            var httpContext = new DefaultHttpContext();

            if (!string.IsNullOrEmpty(authorizationHeader))
            {
                httpContext.Request.Headers["Authorization"] = authorizationHeader;
            }
            if (!string.IsNullOrEmpty(ipHeader))
            {
                httpContext.Request.Headers["x-real-ip"] = ipHeader;
            }
            if (secondKeyCodeHeader != null) 
            {
                httpContext.Request.Headers["X-CONSALUD-CLAVE2-VALOR"] = secondKeyCodeHeader;
            }
            
            var actionContext = new ActionContext(
                httpContext,
                new RouteData(),
                new ActionDescriptor()
            );

            return new AuthorizationFilterContext(actionContext, new List<IFilterMetadata>());
        }
        
        [Fact]
        public async Task OnAuthorizationAsync_NoSecondKeyCodeHeader_CallsSolicitarAndReturnsPreconditionFailed()
        {
            var context = CreateTestContext($"Bearer {_testToken}", TestIp, null); 
            var solicitudResponse = new SegundaClaveDto { CodigoEstado = "SOLICITUD-OK", TtlSegundos = 180 };
            var expectedMappedResponse = new SegundaClaveResponse { CodigoEstado = solicitudResponse.CodigoEstado, TtlSegundos = solicitudResponse.TtlSegundos };

            _mockClient.Setup(c => c.SolicitarSegundaClaveAsync(It.Is<SolicitudClaveDto>(s =>
                    s.Rut == TestRut && s.Dv == TestDv && s.Folio == TestFolio && s.DescripcionTransaccion == "CANCELA_BC_COSTO_CERO")))
                .ReturnsAsync(solicitudResponse);
            
            await _filter.OnAuthorizationAsync(context);
            
            Assert.NotNull(context.Result);
            var objectResult = Assert.IsType<ObjectResult>(context.Result);
            Assert.Equal((int)HttpStatusCode.PreconditionFailed, objectResult.StatusCode);

            var value = Assert.IsType<SegundaClaveResponse>(objectResult.Value);
            Assert.Equal(expectedMappedResponse.CodigoEstado, value.CodigoEstado);
            Assert.Equal(expectedMappedResponse.TtlSegundos, value.TtlSegundos);
        }
        
        [Fact]
        public async Task OnAuthorizationAsync_InvalidSecondKeyCodeHeader_ReturnsPreconditionFailedInvalidValues()
        {
            var context = CreateTestContext($"Bearer {_testToken}", TestIp, "ABCDE");
            
            await _filter.OnAuthorizationAsync(context);
            
            Assert.NotNull(context.Result);
            var objectResult = Assert.IsType<ObjectResult>(context.Result);
            Assert.Equal((int)HttpStatusCode.PreconditionFailed, objectResult.StatusCode);
            
            dynamic value = objectResult.Value;
            Assert.Equal("VALORES-INVALIDOS", value.GetType().GetProperty("CodigoEstado").GetValue(value, null));
        }
        
        [Fact]
        public async Task OnAuthorizationAsync_EmptySecondKeyCodeHeader_CallsSolicitarAndReturnsPreconditionFailed()
        {
             var context = CreateTestContext($"Bearer {_testToken}", TestIp, "");

             await _filter.OnAuthorizationAsync(context);
            
            Assert.NotNull(context.Result);
            var objectResult = Assert.IsType<ObjectResult>(context.Result);
            Assert.Equal((int)HttpStatusCode.PreconditionFailed, objectResult.StatusCode);
            dynamic value = objectResult.Value;
            Assert.Equal("VALORES-INVALIDOS", value.GetType().GetProperty("CodigoEstado").GetValue(value, null));
        }
        
        [Fact]
        public async Task OnAuthorizationAsync_ValidCode_VerificationSuccessful_AllowsRequestAndSetsIdAuditoria()
        {
            const string expectedValidCode = "123456";
            var context = CreateTestContext($"Bearer {_testToken}", TestIp, expectedValidCode);
            var verificationResponse = new VerificadoDto { CodigoEstado = "VALIDO", IdAuditoria = 987 };
            
            _mockClient.Setup(c => c.VerificarSegundaClaveAsync(It.IsAny<VerificarSegundaClaveDto>()))
                .ReturnsAsync(verificationResponse);
            
            await _filter.OnAuthorizationAsync(context);
            
            Assert.Null(context.Result); 
            Assert.True(context.HttpContext.Items.ContainsKey("IdAuditoria"));
            Assert.Equal(verificationResponse.IdAuditoria, context.HttpContext.Items["IdAuditoria"]);
            
        }
        
        [Fact]
        public async Task OnAuthorizationAsync_ValidCode_VerificationFailed_ReturnsPreconditionFailedWithCodigoEstado()
        {
            const string expectedValidCode = "123456";
            var context = CreateTestContext($"Bearer {_testToken}", TestIp, expectedValidCode);
            var verificationResponse = new VerificadoDto { CodigoEstado = "CODIGO-EXPIRADO", IdAuditoria = 0 }; 

            _mockClient.Setup(c => c.VerificarSegundaClaveAsync(It.Is<VerificarSegundaClaveDto>(v => v.Codigo == expectedValidCode)))
                .ReturnsAsync(verificationResponse);
            
            await _filter.OnAuthorizationAsync(context);
            
            Assert.NotNull(context.Result);
            var objectResult = Assert.IsType<ObjectResult>(context.Result);
            Assert.Equal((int)HttpStatusCode.PreconditionFailed, objectResult.StatusCode);
            
            dynamic value = objectResult.Value;
            Assert.Equal(verificationResponse.CodigoEstado, value.GetType().GetProperty("CodigoEstado").GetValue(value, null));
            Assert.False(context.HttpContext.Items.ContainsKey("IdAuditoria")); 
        }
        
        [Fact]
        public async Task OnAuthorizationAsync_MalformedToken_ThrowsAndLogsDuringDecoding()
        {
            var context = CreateTestContext("Bearer invalid.token.string", TestIp, "123456");
            
            await Assert.ThrowsAnyAsync<Exception>(() => _filter.OnAuthorizationAsync(context));
        }
        
        [Fact]
        public async Task OnAuthorizationAsync_TokenMissingRequiredFields_ThrowsDuringGetClientData()
        {
            var incompletePayloadString = JsonConvert.SerializeObject(new { WrongField = "abc", AnotherField = 123 });
            
            var incompleteToken = $"{Base64UrlEncode(TestHeaderJson)}.{Base64UrlEncode(incompletePayloadString)}.dummySignature";
            
            var context = CreateTestContext($"Bearer {incompleteToken}", TestIp, "123456");
            
            await Assert.ThrowsAnyAsync<Exception>(() => _filter.OnAuthorizationAsync(context));
            
        }
    }
}